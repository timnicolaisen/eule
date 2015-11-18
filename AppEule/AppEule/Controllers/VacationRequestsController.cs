using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using System.Web.Mvc;
using AppEule.Controllers;
using VacationManagement;
using DatabaseManagement;
using Microsoft.AspNet.Identity;

namespace GUIManagement
{
    /// <summary>
    /// The controller is responsible for managing the vacation requests (Urlaubsanträge).
    /// Includes the actions like creating VR, showing the list of VRs, editing and deleting the ones.
    /// </summary>
    [HandleError()]
    public class VacationRequestsController : BaseController
    {

        public ActionResult RoleView()
        {
            if (User.IsInRole("Bereichsleiter"))
            {
                return RedirectToAction("Index", "PendingVacationRequests");
            }
            else if (User.IsInRole("Administrator"))
            {
            
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return RedirectToAction("Index", "VacationRequests");
            }

        }


        // GET: Employee's owned VacationRequests
        // EmployeeVacationRequestController
        public ActionResult Index()
        {
            //DBQuery dbq = new DBQuery();
            //dbq.RestoreDemoDB();
                return View(GetVacationRequests());   
        }


        public ActionResult All()
        {
            return View(GetAllVacationRequests());
        }

        // GET: VacationRequests/Create
        public ActionResult Create()
        {
            
            return View();
        }
       
        // POST: VacationRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VacationManagement.VacationRequest vacationRequest)
        {
            String currentUserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                String statusMessage;

                bool result = false;

                if (vacationRequest.VacationEndDate >= vacationRequest.VacationStartDate)
                {
                    if (DateTime.Now.Year == vacationRequest.getVacationStartDate().Year && DateTime.Now.Year == vacationRequest.getVacationEndDate().Year)
                    {
                        //Constructor VacationRequest
                        VacationManagement.VacationRequest vr = new VacationManagement.VacationRequest(currentUserId, vacationRequest.VacationStartDate, vacationRequest.VacationEndDate);

                        //start validate then insert into db
                        result = vr.InsertIntoDB();

                        if (result)
                        {
                            TempData["success"] = "Der Urlaubsantrag wurde erfolgreich angelegt.";
                        }
                    }
                    else
                    {
                        ErrorState.ErrorStateInstance.setError(ErrorState.VACATION_REQUEST_NOT_IN_CURRENT_YEAR);
                    }
                }
                else
                {
                    ErrorState.ErrorStateInstance.setError(ErrorState.END_DATE_BEFORE_START_DATE);
                }
                
                int lastError = ErrorState.ErrorStateInstance.getLastError();          
                if (lastError != ErrorState.OK )
                {
                    TempData["error"] = "Leider konnte ihr Urlaubsantrag nicht erstellt werden. " + ErrorState.ErrorStateInstance.getErrorMessage(lastError);
                }
            }
            
            return RedirectToAction("Index", GetVacationRequests());
        }

        // POST: VacationRequests/Delete/5      
        public ActionResult Cancel(ulong id)
        {
            DBQuery dbq = new DBQuery();
               
            Boolean result = dbq.UpdateVacationRequestStatus(id, VacationRequestProcessingState.canceled, DateTime.Now);
            if (result)
            {
                VacationRequest vr = dbq.SelectVacationRequest(id);
                //Restore Net Vacation Days of VacationRequest to Employee's VacationEntitlement
                result = RestoreVacationDaysInEmployeeVacationEntitlement(vr);          
                if (result)
                {
                    TempData["success"] = "Der Urlaubsantrag wurde erfolgreich storniert.";                   
                }
                else
                {
                    ErrorState.ErrorStateInstance.setError(ErrorState.DATABASE_ERROR);
                    //TempData["error"] = "Die Gutschrift der Urlaubstage konnte nicht durchgeführt werden!";                                               
                }
                // set NetVacationDays of cancelled VacationRequest to zero
                result = ClearNetVacationDaysOfCanceledVacationRequest(vr.getVacationRequestID());
                if (!result)
                {
                    ErrorState.ErrorStateInstance.setError(ErrorState.DATABASE_ERROR);
                }
                // remove any VacationPeriodOverlapNotes in VacationRequests of ShiftPartner, if applicable
                if (vr.getVacationPeriodOverlapNote())
                {
                    result = RemoveVacationPeriodOverlapNote(vr);
                    if (!result)
                    {
                        ErrorState.ErrorStateInstance.setError(ErrorState.DATABASE_ERROR);
                    }
                }
            }
            else
            {
                ErrorState.ErrorStateInstance.setError(ErrorState.DATABASE_ERROR);
                //TempData["error"] = "Die Änderung konnte nicht in die Datenbank übernommen werden!"; 
            }

            int lastError = ErrorState.ErrorStateInstance.getLastError();
            if (lastError != ErrorState.OK)
            {
                TempData["error"] = "Der Urlaubsantrag konnte nicht storniert werden. Grund: " + ErrorState.ErrorStateInstance.getErrorMessage(lastError);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(ulong id)
        {
            DBQuery dbq = new DBQuery();

            VacationRequest vr = dbq.SelectVacationRequest(id);
            int netWorkingDays = vr.getNetVacationDays();
            int remainingVacationDays = dbq.SelectRemainingVacationDays(vr.getEmployeeID());
            Boolean update = dbq.UpdateRemainingVacationDays(vr.getEmployeeID(), netWorkingDays + remainingVacationDays);
            if (update)
            {
                Boolean result = dbq.DeleteVacationRequest(id);
                if (result)
                {
                    ErrorState.ErrorStateInstance.setError(ErrorState.DATABASE_ERROR);
                    TempData["success"] = "Der Urlaubsantrag wurde erfolgreich gelöscht";
                }
                else
                {
                    ErrorState.ErrorStateInstance.setError(ErrorState.DATABASE_ERROR);
                    //TempData["error"] = "Die Änderung konnte nicht in die Datenbank übernommen werden!";
                }
            }
            else
            {
                ErrorState.ErrorStateInstance.setError(ErrorState.DATABASE_ERROR);
                //TempData["error"] = "Die Änderung konnte nicht in die Datenbank übernommen werden!";        
            }

            int lastError = ErrorState.ErrorStateInstance.getLastError();
            if (lastError != ErrorState.OK)
            {
                TempData["error"] = "Der Urlaubsantrag konnte nicht gelöscht werden. Grund: " + ErrorState.ErrorStateInstance.getErrorMessage(lastError);
            }

            return RedirectToAction("All");
        }

        public List<AllVacationRequestViewItem> GetAllVacationRequests()
        {

            List<GUIManagement.AllVacationRequestViewItem> resultList = new List<GUIManagement.AllVacationRequestViewItem>();
            DBQuery dbq = new DBQuery();

            List<VacationManagement.VacationRequest> vacationRequestList = dbq.SelectAllVacationRequestsOfDivision(1);              // hier muss eine Funktionr ein der die Bereichsid übermittelt

            string requesterFullName = "-";

            foreach (VacationManagement.VacationRequest vr in vacationRequestList)
            {
                requesterFullName = dbq.SelectEmployeeFullName(vr.getEmployeeID());
                var employeeVacationRequest = new GUIManagement.AllVacationRequestViewItem(vr, requesterFullName);
                resultList.Add(employeeVacationRequest);
            }
            return resultList;
        }

        public List<EmployeeVacationRequestViewItem> GetVacationRequests()
        {
            String currentUserId = User.Identity.GetUserId();

            List<GUIManagement.EmployeeVacationRequestViewItem> resultList = new List<GUIManagement.EmployeeVacationRequestViewItem>();
            DBQuery dbq = new DBQuery();

            List<VacationManagement.VacationRequest> vacationRequestList = dbq.SelectAllVacationRequestsOfEmployee(currentUserId);

            String shiftPartnerID = dbq.SelectShiftPartner(currentUserId);
            String shiftParterName = "-";

            if (!shiftPartnerID.Equals("null"))
            {
                Employee shiftPartner = dbq.SelectDeputy(currentUserId);
                shiftParterName = dbq.SelectEmployeeFullName(shiftPartner.getEmployeeID());
            }

            foreach (VacationManagement.VacationRequest vr in vacationRequestList)
            {
                var employeeVacationRequest = new GUIManagement.EmployeeVacationRequestViewItem(vr, shiftParterName);
                resultList.Add(employeeVacationRequest);
            }
            return resultList;
        }

        private Boolean RestoreVacationDaysInEmployeeVacationEntitlement(VacationRequest vacationRequest)
        {
            Boolean result = false;
            DBQuery dbq = new DBQuery();
            int netWorkingDays = vacationRequest.getNetVacationDays();
            int remainingVacationDays = dbq.SelectRemainingVacationDays(vacationRequest.getEmployeeID());
            result = dbq.UpdateRemainingVacationDays(vacationRequest.getEmployeeID(), netWorkingDays + remainingVacationDays);
            return result;
        }

        private Boolean ClearNetVacationDaysOfCanceledVacationRequest(ulong vacationRequestID)
        {
            Boolean result = false;
            DBQuery dbq = new DBQuery();
            result = dbq.UpdateNetVacationDays(vacationRequestID, 0);
            return result;
        }

        /// <summary>
        /// Removes the TRUE-flag in all vacation requests of the shift partner, if the given vacation request is cancelled by its requester.
        /// Use requester ID of vacation request to identity the shiftpartner and startdate and enddate to retrieve vacation requests of the deputy
        /// </summary>
        /// <param name="vacationRequest">use requesterID to find deputy, use time period to specify overlap time period </param>
        /// <returns>TRUE if successful, else FALSE</returns>
        private Boolean RemoveVacationPeriodOverlapNote(VacationRequest vacationRequest)
        {
            Boolean result = true;
            DBQuery dbq = new DBQuery();

            String deputyID = dbq.SelectDeputy(vacationRequest);
            if (!deputyID.Equals("null") && deputyID != String.Empty)
            {
                var overlapList = dbq.SelectUncanceledEmployeeVacationRequestInTimePeriod(vacationRequest, deputyID);
                if (overlapList.Count > 0)
                {
                    foreach (var i in overlapList)
                    {
                        //change OverlapNote of existing VacationRequest of Deputy
                        result = dbq.UpdateVacationRequestPeriodOverlapNote(i.Item1, false);
                        if (!result) break;
                    }
                }
            }
            return result;
        }
    }
}

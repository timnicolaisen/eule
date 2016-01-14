using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VacationManagement;
using DatabaseManagement;
using Microsoft.AspNet.Identity;
using GUIManagement;
using CommunicationManagement;


namespace AppEule.Controllers
{
    /// <summary>
    /// The controller for the view PendingVacationRequests/Index.cshtml, available only for division manager (Bereichsleiter)
    /// Shows all the vacation requests that are waiting for a permission from a division manager.
    /// </summary>
    public class PendingVacationRequestsController : Controller
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Bereichsleiter")]
        public ActionResult Index()
        {
            //return View(GetPendingRequests());
            return View(GetAllPendingVacationRequestsOfDivision());
        }

        [Authorize(Roles = "Bereichsleiter")]
        public ActionResult Permit(ulong id)
        {
            DBQuery dbq = new DBQuery();
            VacationRequest vr1 = dbq.SelectVacationRequest(id);
            Boolean result = false;

            // if vacationRequestStartDate is in the past then the status will be set to "taken"
            if (vr1.getVacationStartDate() < DateTime.Now)
            {
                result = dbq.UpdateVacationRequestStatus(id, VacationRequestProcessingState.taken, DateTime.Now);
            }
            else
            {
                result = dbq.UpdateVacationRequestStatus(id, VacationRequestProcessingState.permitted, DateTime.Now);
            }
            if (result)
            {
                VacationRequest vr = dbq.SelectVacationRequest(id);
                CommunicationManagement.EmailService message = new CommunicationManagement.EmailService();
                message.sendMessage(vr, CommunicationManagement.EmailService.CONFIRMATION_EMAIL_PERMITTED);

                TempData["success"] = "Der Urlaubsantrag wurde befürwortet."; 
            }
            else
            {
                ErrorState.ErrorStateInstance.setError(ErrorState.DATABASE_ERROR);
                //TempData["error"] = "Die Änderung konnte nicht in die Datenbank übernommen werden!"; 
            }

            int lastError = ErrorState.ErrorStateInstance.getLastError();
            if (lastError != ErrorState.OK)
            {
                TempData["error"] = ErrorState.ErrorStateInstance.getErrorMessage(lastError);
            }

            return View("Index", GetAllPendingVacationRequestsOfDivision());
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Bereichsleiter")]
        public ActionResult Reject(ulong id)
        {
            String currentUserId = User.Identity.GetUserId();
            DBQuery dbq = new DBQuery();
            Boolean result = false;

            result  = dbq.UpdateVacationRequestStatus(id, VacationRequestProcessingState.rejectedByDivisionManager, DateTime.Now);
            if (result)
            {
                // Restore VacationEntitlement to Employee
                VacationRequest vr = dbq.SelectVacationRequest(id);                          
                CommunicationManagement.EmailService message = new CommunicationManagement.EmailService();
                message.sendMessage(vr, CommunicationManagement.EmailService.CONFIRMATION_EMAIL_REJECTED);
                TempData["success"] = "Der Urlaubsantrag wurde abgelehnt.";                   
            }
            else
            {
                ErrorState.ErrorStateInstance.setError(ErrorState.DATABASE_ERROR);
                // TempData["error"] = "Die Änderung konnte nicht in die Datenbank übernommen werden!"; 
            }

            int lastError = ErrorState.ErrorStateInstance.getLastError();
            if (lastError != ErrorState.OK)
            {
                TempData["error"] = ErrorState.ErrorStateInstance.getErrorMessage(lastError);
            }

            return View("Index", GetAllPendingVacationRequestsOfDivision());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PendingVacationRequestViewItem> GetPendingRequests()
        {
            String currentUserId = User.Identity.GetUserId();
            List<String> statesListWithShiftPartner = new List<string> { DBQuery.AGREED, DBQuery.REJECTED_BY_DEPUTY };
            List<String> statesListWithoutShiftPartner = new List<string> { DBQuery.SUBMITTED };

            DBQuery dbq = new DBQuery();

            var DivisionVacationRequestList = new List<PendingVacationRequestViewItem>();
            var SortedDivisionVacationRequestList = new List<PendingVacationRequestViewItem>();
            var EmployeeList = new List<Employee>();

            EmployeeList.AddRange(dbq.SelectEmployeesOfDivision(currentUserId));
            if (EmployeeList.Count() > 0)
            {
                foreach (Employee e in EmployeeList)
                {
                    // look for all employees of division but Division Manager self
                    if (!e.getEmployeeID().Equals(currentUserId))
                    {
                        var vrList = new List<VacationManagement.VacationRequest>();
                        String requesterName = e.getFullName();
                        String deputyID = dbq.SelectShiftPartner(e.getEmployeeID());
                        int remainingVacationDays = dbq.SelectRemainingVacationDays(e.getEmployeeID());

                        // has shiftpartner
                        if (!deputyID.Equals("null"))
                        {
                            String deputyName = dbq.SelectEmployeeFullName(deputyID);

                            foreach (string s in statesListWithShiftPartner)
                            {
                                vrList.AddRange(dbq.SelectVacationRequestsInState(e.getEmployeeID(), s));
                            }
                            foreach (VacationManagement.VacationRequest vr in vrList)
                            {
                                DivisionVacationRequestList.Add(new PendingVacationRequestViewItem(vr, requesterName, deputyName, remainingVacationDays));
                            }
                        }
                        else
                        {
                            String deputyName = "-";
                            foreach (string s in statesListWithoutShiftPartner)
                            {
                                vrList.AddRange(dbq.SelectVacationRequestsInState(e.getEmployeeID(), s));
                            }
                            foreach (VacationManagement.VacationRequest vr in vrList)
                            {
                                DivisionVacationRequestList.Add(new PendingVacationRequestViewItem(vr, requesterName, deputyName, remainingVacationDays));
                            }
                        }
                    }
                }
                // Sort all Vacation Requests by Startdate, Ascending
               SortedDivisionVacationRequestList = DivisionVacationRequestList.OrderBy(c => c.getVacationStartDate()).ToList();
            }
            return SortedDivisionVacationRequestList;
        }

        /// <summary>
        /// Picks all vacation requests of division but vacation requests of division manager in either state "submitted", "agreed" or "rejected by deputy"
        /// </summary>
        /// <returns>ViewItem for Division Manger role with details about vacation request, shift partners and vacation entitlement of requester</returns>
        private List<PendingVacationRequestViewItem> GetAllPendingVacationRequestsOfDivision()
        {
            String currentUserId = User.Identity.GetUserId();
            List<String> statesListForDeputy = new List<string> { DBQuery.SUBMITTED, DBQuery.AGREED, DBQuery.REJECTED_BY_DEPUTY };
            //List<String> statesListWithoutShiftPartner = new List<string> { DBQuery.SUBMITTED };
            DBQuery dbq = new DBQuery();

            var DivisionVacationRequestList = new List<PendingVacationRequestViewItem>();
            var SortedDivisionVacationRequestList = new List<PendingVacationRequestViewItem>();
            var EmployeeList = new List<Employee>();

            EmployeeList.AddRange(dbq.SelectEmployeesOfDivision(currentUserId));
            if (EmployeeList.Count() > 0)
            {
                foreach (Employee e in EmployeeList)
                {
                    // look for all employees of division but Division Manager self
                    if (!e.getEmployeeID().Equals(currentUserId))
                    {
                        String requesterName = e.getFullName();
                        int remainingVacationDays = dbq.SelectRemainingVacationDays(e.getEmployeeID());
                        String deputyID = dbq.SelectShiftPartner(e.getEmployeeID());
                        String deputyName = "-";
                        if (deputyID != null)
                        {
                            deputyName = dbq.SelectEmployeeFullName(deputyID);
                        }
                        var vrList = new List<VacationManagement.VacationRequest>();

                        foreach (string s in statesListForDeputy)
                        {
                            vrList.AddRange(dbq.SelectVacationRequestsInState(e.getEmployeeID(), s));
                        }
                        foreach (VacationManagement.VacationRequest vr in vrList)
                        {
                            DivisionVacationRequestList.Add(new PendingVacationRequestViewItem(vr, requesterName, deputyName, remainingVacationDays));
                        }
                    }
                }
                // Sort all Vacation Requests by Startdate, Ascending
                SortedDivisionVacationRequestList = DivisionVacationRequestList.OrderBy(c => c.getVacationStartDate()).ToList();
            }
            return SortedDivisionVacationRequestList;
        }
    }
}
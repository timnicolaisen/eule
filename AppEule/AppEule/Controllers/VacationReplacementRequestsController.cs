using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AppEule.Controllers;
using Microsoft.AspNet.Identity;
using DatabaseManagement;
using VacationManagement;
using CommunicationManagement;

namespace GUIManagement
{
    /// <summary>
    /// The controller to manage vacation replacement requests (Vertretungsanfrage), a use can see them and confirm or deny.
    /// </summary>
    [HandleError()]
    public class VacationReplacementRequestsController : BaseController
    {
       

        // GET: VacationReplacementRequests
        public ActionResult Index()
        {
            // identify logged-in user
            String currentUserId = User.Identity.GetUserId();

            DBQuery dbq = new DBQuery();
            var ReplacementReqList = new List<GUIManagement.VacationReplacementRequestViewItem>();
            
            String shiftPartnerID = dbq.SelectShiftPartner(currentUserId);
           
            if (shiftPartnerID != null)
            {
                var VRList = dbq.SelectSubmittedVacationRequestsOfShiftPartner(shiftPartnerID);
                String name = dbq.SelectEmployeeFullName(shiftPartnerID);

                foreach (VacationManagement.VacationRequest vr in VRList)
                {
                    GUIManagement.VacationReplacementRequestViewItem vrr = new GUIManagement.VacationReplacementRequestViewItem(vr, name);
                    ReplacementReqList.Add(vrr);
                }
            }
            else
            {
                // Labelausgabe GUI

            }
            return View(ReplacementReqList);
        }


        public ActionResult Agree(ulong id)
        {
            DBQuery dbq = new DBQuery();
            Boolean result = false;
            result = dbq.UpdateVacationRequestStatus(id, VacationRequestProcessingState.agreed, DateTime.Now);

            if (result)
            {
                VacationRequest vr = dbq.SelectVacationRequest(id);
                CommunicationManagement.EmailService message = new CommunicationManagement.EmailService();
                message.sendMessage(vr, CommunicationManagement.EmailService.CONFIRMATION_EMAIL_AGREED);
                
                TempData["success"] = "Dem Urlaubsantrag wurde zugestimmt.";
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

            return RedirectToAction("Index", "VacationRequests");
        }


        public ActionResult Disagree(ulong id)
        {
            DBQuery dbq = new DBQuery();
            Boolean result = false;
            result = dbq.UpdateVacationRequestStatus(id, VacationRequestProcessingState.rejectedByDeputy, DateTime.Now);
            if (result)
            {
                VacationRequest vr = dbq.SelectVacationRequest(id);
                CommunicationManagement.EmailService message = new CommunicationManagement.EmailService();
                message.sendMessage(vr, CommunicationManagement.EmailService.CONFIRMATION_EMAIL_REJECTED);

                TempData["success"] = "Die Vertretungsanfrage wurde abgelehnt.";
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

            return RedirectToAction("Index", "VacationRequests");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEule.Controllers
{
    [HandleError()]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("RoleView", "VacationRequests");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

      
    }
}
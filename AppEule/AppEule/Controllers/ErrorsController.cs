using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEule.Controllers
{
    [HandleError()]
    public class ErrorsController : BaseController
    {
        // GET: Errors
        public ActionResult Show404()
        {
            return View();
        }
        public ActionResult Test()
        {
            object i = null;
            int j = (int)i;
            return View("Login");
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            string actionName = filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            HandleErrorInfo haErInfo = new HandleErrorInfo(e, controllerName, actionName);
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(haErInfo)
            };
        }
    }
}
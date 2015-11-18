using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEule.Controllers
{
    ///<summary>
    ///This controller is used only to override the OnException method. Is a parent-class for all the controllers 
    ///</summary>
    [HandleError()]
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            string actionName = filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            HandleErrorInfo haErInfo= new HandleErrorInfo(e, controllerName, actionName);
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(haErInfo)
            };
        }
    }
}
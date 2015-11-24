using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppEule.Models;
using DatabaseManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VacationManagement;

namespace AppEule.Controllers
{
    /// <summary>
    /// Is used to show the list of all the employees and edit them.
    /// </summary>
    [HandleError()]
    public class EmployeeController : BaseController
    {
        private DBQuery _dbq = new DBQuery();

        // GET: Employee
        public ActionResult Index()
        {

            string currentId = User.Identity.GetUserId();
            List<Employee> resultList = _dbq.SelectEmployeesOfDivision(currentId);


            return View(resultList);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee emp = _dbq.SelectEmployeebyId(id);
            if (emp == null)
            {
                return HttpNotFound();
            }

            return View(emp);
        }


        public ActionResult ResetDB()
        {
            _dbq.ResetDB();
            return RedirectToAction("RoleView", "VacationRequests");
        }

        public ActionResult ResetToDefaultDB()
        {
            _dbq.RestoreDemoDB();
            return RedirectToAction("RoleView", "VacationRequests");
        }

        public ActionResult ResetDemoDB()
        {
            _dbq.RestoreDemoDB();
            return RedirectToAction("RoleView", "VacationRequests");
        }
    }

}

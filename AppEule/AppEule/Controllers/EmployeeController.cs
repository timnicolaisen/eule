using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppEule.Models;
using DatabaseManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VacationManagement;
using GUIManagement;

namespace AppEule.Controllers
{
    /// <summary>
    /// Is used to show the list of all the employees and edit them.
    /// </summary>
    [HandleError()]
    public class EmployeeController : BaseController
    {
        private DBQuery _dbq = new DBQuery();
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context = new ApplicationDbContext();

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
            EmployeeDetailsViewItem emp = _dbq.SelectEmployeebyDetailsById(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
             
            return View(emp);
        }

        public ActionResult Details(string id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            var ListOfAllEmployee = _dbq.SelectListOfAllEmployees();
            ViewBag.Employee = ListOfAllEmployee;

            EmployeeDetailsViewItem emp = _dbq.SelectEmployeebyDetailsById(id);
            if (emp == null)
            {
                return HttpNotFound();
            }

            return View(emp);
        }


        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult Details(EmployeeDetailsViewItem EmployeeDetails,  string id, string Roles, string Lastname, string LastName)
        {
            try
            {
                //context.Entry(Roles).State = System.Data.Entity.EntityState.Modified;
                //context.SaveChanges();

                if(EmployeeDetails.ShiftGroupPartnerName == null)
                {
                    return RedirectToAction("Index");
                }
                if (_dbq.CheckForShiftgroup(id) == true)
                {
                    _dbq.DeleteShiftGroup(id);
                }
                ShiftGroup UpdateShiftGroup = new ShiftGroup(EmployeeDetails.ShiftGroupPartnerName, id);
                _dbq.InsertNewShiftgroup(UpdateShiftGroup);

                EmployeeDetails.UserId = id;
                _dbq.UpdateEmployeebyDetail(EmployeeDetails);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

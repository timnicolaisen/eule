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
    public class VacationEntitlementController : Controller
    {

        private DBQuery _dbq = new DBQuery();
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: VacationEntitlement
        public ActionResult Index()
        {
            string currentId = User.Identity.GetUserId();
            List<VacationEntitlementViewItem> resultList = _dbq.SelectEntitlementsOfDivision();
            return View(resultList);
        }

        // GET: VacationEntitlement/Details/5
        public ActionResult Details(String id)
        {
            
            VacationEntitlementViewItem resultList = _dbq.SelectEntitlementsOfEmployee(id);

            return View(resultList);
        }

        [HttpPost]
        public ActionResult Details(VacationEntitlementViewItem VacationEntitlement, string id)
        {

            _dbq.UpdateEntitlementOfEmployee(VacationEntitlement);

            return RedirectToAction("Index");
        }

        // GET: VacationEntitlement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VacationEntitlement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: VacationEntitlement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VacationEntitlement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: VacationEntitlement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VacationEntitlement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

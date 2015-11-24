using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppEule.Models;
using DatabaseManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VacationManagement;


namespace AppEule.Controllers
{
    public class ShiftGroupController : Controller
    {
        private DBQuery _dbq = new DBQuery();
        // GET: ShiftGroup
        public ActionResult Index()
        {
            
            List<ShiftGroup> resultList = _dbq.SelectShiftGroupOfDivision();

            return View(resultList);
        }

        // GET: ShiftGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShiftGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShiftGroup/Create
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

        // GET: ShiftGroup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShiftGroup/Edit/5
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

        // GET: ShiftGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShiftGroup/Delete/5
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

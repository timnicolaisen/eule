using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEule.Controllers
{
    public class ShiftGroupsController : Controller
    {
        // GET: ShiftGroups
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShiftGroups/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShiftGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShiftGroups/Create
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

        // GET: ShiftGroups/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShiftGroups/Edit/5
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

        // GET: ShiftGroups/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShiftGroups/Delete/5
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

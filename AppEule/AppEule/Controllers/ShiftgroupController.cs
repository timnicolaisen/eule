using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEule.Controllers
{
    public class ShiftgroupController : Controller
    {
        // GET: Shiftgroup
        public ActionResult Index()
        {
            return View();
        }

        // GET: Shiftgroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shiftgroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shiftgroup/Create
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

        // GET: Shiftgroup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shiftgroup/Edit/5
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

        // GET: Shiftgroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shiftgroup/Delete/5
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

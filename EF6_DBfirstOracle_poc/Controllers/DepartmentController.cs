using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF6_DBfirstOracle_poc.Models;

namespace EF6_DBfirstOracle_poc.Controllers
{
    public class DepartmentController : Controller
    {
        private Entities db = new Entities();

        // GET: Department
        public ActionResult Index()
        {
            var dEPARTMENTS = db.DEPARTMENTS.Include(d => d.EMPLOYEE);
            return View(dEPARTMENTS.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTMENT dEPARTMENT = db.DEPARTMENTS.Find(id);
            if (dEPARTMENT == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTMENT);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.MANAGER_ID = new SelectList(db.EMPLOYEES, "EMPLOYEE_ID", "FIRST_NAME");
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID,LOCATION_ID")] DEPARTMENT dEPARTMENT)
        {
            if (ModelState.IsValid)
            {
                db.DEPARTMENTS.Add(dEPARTMENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANAGER_ID = new SelectList(db.EMPLOYEES, "EMPLOYEE_ID", "FIRST_NAME", dEPARTMENT.MANAGER_ID);
            return View(dEPARTMENT);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTMENT dEPARTMENT = db.DEPARTMENTS.Find(id);
            if (dEPARTMENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANAGER_ID = new SelectList(db.EMPLOYEES, "EMPLOYEE_ID", "FIRST_NAME", dEPARTMENT.MANAGER_ID);
            return View(dEPARTMENT);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID,LOCATION_ID")] DEPARTMENT dEPARTMENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dEPARTMENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANAGER_ID = new SelectList(db.EMPLOYEES, "EMPLOYEE_ID", "FIRST_NAME", dEPARTMENT.MANAGER_ID);
            return View(dEPARTMENT);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTMENT dEPARTMENT = db.DEPARTMENTS.Find(id);
            if (dEPARTMENT == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTMENT);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            DEPARTMENT dEPARTMENT = db.DEPARTMENTS.Find(id);
            db.DEPARTMENTS.Remove(dEPARTMENT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

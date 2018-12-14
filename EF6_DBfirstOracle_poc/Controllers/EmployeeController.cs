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
    public class EmployeeController : Controller
    {
        private Entities db = new Entities();

        // GET: Employee
        public ActionResult Index()
        {
            var eMPLOYEES = db.EMPLOYEES.Include(e => e.DEPARTMENT).Include(e => e.EMPLOYEE1);
            return View(eMPLOYEES.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEES.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.DEPARTMENT_ID = new SelectList(db.DEPARTMENTS, "DEPARTMENT_ID", "DEPARTMENT_NAME");
            ViewBag.MANAGER_ID = new SelectList(db.EMPLOYEES, "EMPLOYEE_ID", "FIRST_NAME");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL,PHONE_NUMBER,HIRE_DATE,JOB_ID,SALARY,COMMISSION_PCT,MANAGER_ID,DEPARTMENT_ID")] EMPLOYEE eMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                db.EMPLOYEES.Add(eMPLOYEE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DEPARTMENT_ID = new SelectList(db.DEPARTMENTS, "DEPARTMENT_ID", "DEPARTMENT_NAME", eMPLOYEE.DEPARTMENT_ID);
            ViewBag.MANAGER_ID = new SelectList(db.EMPLOYEES, "EMPLOYEE_ID", "FIRST_NAME", eMPLOYEE.MANAGER_ID);
            return View(eMPLOYEE);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEES.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            ViewBag.DEPARTMENT_ID = new SelectList(db.DEPARTMENTS, "DEPARTMENT_ID", "DEPARTMENT_NAME", eMPLOYEE.DEPARTMENT_ID);
            ViewBag.MANAGER_ID = new SelectList(db.EMPLOYEES, "EMPLOYEE_ID", "FIRST_NAME", eMPLOYEE.MANAGER_ID);
            return View(eMPLOYEE);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL,PHONE_NUMBER,HIRE_DATE,JOB_ID,SALARY,COMMISSION_PCT,MANAGER_ID,DEPARTMENT_ID")] EMPLOYEE eMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLOYEE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DEPARTMENT_ID = new SelectList(db.DEPARTMENTS, "DEPARTMENT_ID", "DEPARTMENT_NAME", eMPLOYEE.DEPARTMENT_ID);
            ViewBag.MANAGER_ID = new SelectList(db.EMPLOYEES, "EMPLOYEE_ID", "FIRST_NAME", eMPLOYEE.MANAGER_ID);
            return View(eMPLOYEE);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEES.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMPLOYEE eMPLOYEE = db.EMPLOYEES.Find(id);
            db.EMPLOYEES.Remove(eMPLOYEE);
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

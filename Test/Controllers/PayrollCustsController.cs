using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class PayrollCustsController : Controller
    {
        private RCMSEntities2 db = new RCMSEntities2();

        // GET: PayrollCusts
        public ActionResult Index()
        {
            var payrollCusts = db.PayrollCusts.Include(p => p.Customer).Include(p => p.Organization);
            return View(payrollCusts.ToList());
        }

        // GET: PayrollCusts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayrollCust payrollCust = db.PayrollCusts.Find(id);
            if (payrollCust == null)
            {
                return HttpNotFound();
            }
            return View(payrollCust);
        }

        // GET: PayrollCusts/Create
        public ActionResult Create()
        {
            ViewBag.prCust_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname");
            ViewBag.prCust_OrgId = new SelectList(db.Organizations, "org_id", "org_name");
            return View();
        }

        // POST: PayrollCusts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prCust_id,prCust_OrgId,prCust_CustId,prCust_paymnt,prCust_date")] PayrollCust payrollCust)
        {
            if (ModelState.IsValid)
            {
                db.PayrollCusts.Add(payrollCust);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prCust_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", payrollCust.prCust_CustId);
            ViewBag.prCust_OrgId = new SelectList(db.Organizations, "org_id", "org_name", payrollCust.prCust_OrgId);
            return View(payrollCust);
        }

        // GET: PayrollCusts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayrollCust payrollCust = db.PayrollCusts.Find(id);
            if (payrollCust == null)
            {
                return HttpNotFound();
            }
            ViewBag.prCust_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", payrollCust.prCust_CustId);
            ViewBag.prCust_OrgId = new SelectList(db.Organizations, "org_id", "org_name", payrollCust.prCust_OrgId);
            return View(payrollCust);
        }

        // POST: PayrollCusts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prCust_id,prCust_OrgId,prCust_CustId,prCust_paymnt,prCust_date")] PayrollCust payrollCust)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payrollCust).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prCust_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", payrollCust.prCust_CustId);
            ViewBag.prCust_OrgId = new SelectList(db.Organizations, "org_id", "org_name", payrollCust.prCust_OrgId);
            return View(payrollCust);
        }

        // GET: PayrollCusts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayrollCust payrollCust = db.PayrollCusts.Find(id);
            if (payrollCust == null)
            {
                return HttpNotFound();
            }
            return View(payrollCust);
        }

        // POST: PayrollCusts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayrollCust payrollCust = db.PayrollCusts.Find(id);
            db.PayrollCusts.Remove(payrollCust);
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

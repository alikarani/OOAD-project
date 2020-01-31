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
    public class payrollLabsController : Controller
    {
        private RCMSEntities2 db = new RCMSEntities2();

        // GET: payrollLabs
        public ActionResult Index()
        {
            var payrollLabs = db.payrollLabs.Include(p => p.Labor).Include(p => p.Organization);
            return View(payrollLabs.ToList());
        }

        // GET: payrollLabs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payrollLab payrollLab = db.payrollLabs.Find(id);
            if (payrollLab == null)
            {
                return HttpNotFound();
            }
            return View(payrollLab);
        }

        // GET: payrollLabs/Create
        public ActionResult Create()
        {
            ViewBag.prLab_LabId = new SelectList(db.Labors, "Lab_id", "Lab_name");
            ViewBag.prLab_OrgId = new SelectList(db.Organizations, "org_id", "org_name");
            return View();
        }

        // POST: payrollLabs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prLab_id,prLab_OrgId,prLab_LabId,prLab_paymnt,Date")] payrollLab payrollLab)
        {
            if (ModelState.IsValid)
            {
                db.payrollLabs.Add(payrollLab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prLab_LabId = new SelectList(db.Labors, "Lab_id", "Lab_name", payrollLab.prLab_LabId);
            ViewBag.prLab_OrgId = new SelectList(db.Organizations, "org_id", "org_name", payrollLab.prLab_OrgId);
            return View(payrollLab);
        }

        // GET: payrollLabs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payrollLab payrollLab = db.payrollLabs.Find(id);
            if (payrollLab == null)
            {
                return HttpNotFound();
            }
            ViewBag.prLab_LabId = new SelectList(db.Labors, "Lab_id", "Lab_name", payrollLab.prLab_LabId);
            ViewBag.prLab_OrgId = new SelectList(db.Organizations, "org_id", "org_name", payrollLab.prLab_OrgId);
            return View(payrollLab);
        }

        // POST: payrollLabs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prLab_id,prLab_OrgId,prLab_LabId,prLab_paymnt,Date")] payrollLab payrollLab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payrollLab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prLab_LabId = new SelectList(db.Labors, "Lab_id", "Lab_name", payrollLab.prLab_LabId);
            ViewBag.prLab_OrgId = new SelectList(db.Organizations, "org_id", "org_name", payrollLab.prLab_OrgId);
            return View(payrollLab);
        }

        // GET: payrollLabs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payrollLab payrollLab = db.payrollLabs.Find(id);
            if (payrollLab == null)
            {
                return HttpNotFound();
            }
            return View(payrollLab);
        }

        // POST: payrollLabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            payrollLab payrollLab = db.payrollLabs.Find(id);
            db.payrollLabs.Remove(payrollLab);
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

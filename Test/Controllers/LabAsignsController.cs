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
    public class LabAsignsController : Controller
    {
        private RCMSEntities2 db = new RCMSEntities2();

        // GET: LabAsigns
        public ActionResult Index()
        {
            var labAsigns = db.LabAsigns.Include(l => l.Complaint).Include(l => l.Labor).Include(l => l.Organization).Include(l=>l.Status);
            return View(labAsigns.ToList());
        }

        // GET: LabAsigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabAsign labAsign = db.LabAsigns.Find(id);
            if (labAsign == null)
            {
                return HttpNotFound();
            }
            return View(labAsign);
        }

        // GET: LabAsigns/Create
        public ActionResult Create()
        {
            ViewBag.LbAs_CmplntId = new SelectList(db.Complaints, "Cmplnt_Id", "Cmplnt_Id");
            ViewBag.LbAs_complStatus = new SelectList(db.Complaints, "Cmplnt_Id", "Status.State");
            ViewBag.LbAs_LabId = new SelectList(db.Labors, "Lab_id", "Lab_name");
            ViewBag.LbAs_orgId = new SelectList(db.Organizations, "org_id", "org_name");
            return View();
        }

        // POST: LabAsigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LbAs_id,LbAs_orgId,LbAs_LabId,Date,LbAs_CmplntId,LbAs_complStatus")] LabAsign labAsign)
        {
            if (ModelState.IsValid)
            {
                db.LabAsigns.Add(labAsign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LbAs_CmplntId = new SelectList(db.Complaints, "Cmplnt_Id", "Cmplnt_content", labAsign.LbAs_CmplntId);
            ViewBag.LbAs_LabId = new SelectList(db.Labors, "Lab_id", "Lab_name", labAsign.LbAs_LabId);
            ViewBag.LbAs_orgId = new SelectList(db.Organizations, "org_id", "org_name", labAsign.LbAs_orgId);
            return View(labAsign);
        }

        // GET: LabAsigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabAsign labAsign = db.LabAsigns.Find(id);
            Complaint complaint = db.Complaints.Find(id);
            if (labAsign == null)
            {
                return HttpNotFound();
            }
            ViewBag.LbAs_CmplntId = new SelectList(db.Complaints, "Cmplnt_Id", "Cmplnt_Id");
            ViewBag.LbAs_complStatus = new SelectList(db.Status, "St_ID", "State");
            ViewBag.LbAs_LabId = new SelectList(db.Labors, "Lab_id", "Lab_name");
            ViewBag.LbAs_orgId = new SelectList(db.Organizations, "org_id", "org_name");
            return View(labAsign);
        }

        // POST: LabAsigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LbAs_id,LbAs_orgId,LbAs_LabId,Date,LbAs_CmplntId,LbAs_complStatus")] LabAsign labAsign, Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labAsign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LbAs_complStatus = new SelectList(db.Status, "St_ID", "State", labAsign.LbAs_complStatus);
            ViewBag.LbAs_LabId = new SelectList(db.Labors, "Lab_id", "Lab_name", labAsign.LbAs_LabId);
            ViewBag.LbAs_orgId = new SelectList(db.Organizations, "org_id", "org_name", labAsign.LbAs_orgId);
            return View(labAsign);
        }

        // GET: LabAsigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabAsign labAsign = db.LabAsigns.Find(id);
            if (labAsign == null)
            {
                return HttpNotFound();
            }
            return View(labAsign);
        }

        // POST: LabAsigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LabAsign labAsign = db.LabAsigns.Find(id);
            db.LabAsigns.Remove(labAsign);
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

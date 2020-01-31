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
    public class ComplaintsController : Controller
    {
        private RCMSEntities2 db = RCMSEntities2.getInstance();

        // GET: Complaints
        public ActionResult Index()
        {
            //int CustId = (int)Session["CustId"];
            //Session["CustId"] = CustId;
            //int OrgId = (int)Session["OrgId"];
            //Session["OrgId"] = OrgId;
            var complaints = db.Complaints.Include(c => c.Customer).Include(c => c.Organization).Include(c => c.Customer).Include(c => c.Status1);
            return View(complaints.ToList());
        }

        // GET: Complaints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // GET: Complaints/Create
        public ActionResult Create()
        {
            ViewBag.Cmplnt_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname");
            ViewBag.Cmplnt_orgId = new SelectList(db.Organizations, "org_id", "org_name");
            ViewBag.Cmplnt_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname");
            ViewBag.Status = new SelectList(db.Status, "St_ID", "State");
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cmplnt_Id,Cmplnt_CustId,Cmplnt_orgId,Date,Cmplnt_complStat,Complnt_content,Status")] Complaint complaint)
        {
            complaint.Cmplnt_CustId = (Int32)Session["CustId"];
            if (ModelState.IsValid)
            {
                db.Complaints.Add(complaint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cmplnt_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", complaint.Cmplnt_CustId);
            ViewBag.Cmplnt_orgId = new SelectList(db.Organizations, "org_id", "org_name", complaint.Cmplnt_orgId);
            ViewBag.Status = new SelectList(db.Status, "St_ID", "State", complaint.Status);
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cmplnt_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", complaint.Cmplnt_CustId);
            ViewBag.Cmplnt_orgId = new SelectList(db.Organizations, "org_id", "org_name", complaint.Cmplnt_orgId);
            ViewBag.Cmplnt_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", complaint.Cmplnt_CustId);
            ViewBag.Status = new SelectList(db.Status, "St_ID", "State", complaint.Status);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cmplnt_Id,Cmplnt_CustId,Cmplnt_orgId,Date,Cmplnt_complStat,Complnt_content,Status")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cmplnt_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", complaint.Cmplnt_CustId);
            ViewBag.Cmplnt_orgId = new SelectList(db.Organizations, "org_id", "org_name", complaint.Cmplnt_orgId);
            ViewBag.Cmplnt_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", complaint.Cmplnt_CustId);
            ViewBag.Status = new SelectList(db.Status, "St_ID", "State", complaint.Status);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            db.Complaints.Remove(complaint);
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

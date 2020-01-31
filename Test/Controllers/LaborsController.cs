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
    public class LaborsController : Controller
    {
        private RCMSEntities2 db = new RCMSEntities2();

        // GET: Labors
        public ActionResult Index()
        {
            var labors = db.Labors.Include(l => l.Organization);
            return View(labors.ToList());
        }

        // GET: Labors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            return View(labor);
        }

        // GET: Labors/Create
        public ActionResult Create()
        {
            ViewBag.Lab_OrgId = new SelectList(db.Organizations, "org_id", "org_name");
            return View();
        }

        // POST: Labors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lab_id,Lab_name,Lab_OrgId,Lab_status,Ph_no,address,LastName,RoleID,Password,Email")] Labor labor)
        {
            if (ModelState.IsValid)
            {
                db.Labors.Add(labor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Lab_OrgId = new SelectList(db.Organizations, "org_id", "org_name", labor.Lab_OrgId);
            return View(labor);
        }

        // GET: Labors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lab_OrgId = new SelectList(db.Organizations, "org_id", "org_name", labor.Lab_OrgId);
            return View(labor);
        }

        // POST: Labors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lab_id,Lab_name,Lab_OrgId,Lab_status,Ph_no,address,LastName,RoleID,Password,Email")] Labor labor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Lab_OrgId = new SelectList(db.Organizations, "org_id", "org_name", labor.Lab_OrgId);
            return View(labor);
        }

        // GET: Labors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            return View(labor);
        }

        // POST: Labors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Labor labor = db.Labors.Find(id);
            db.Labors.Remove(labor);
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

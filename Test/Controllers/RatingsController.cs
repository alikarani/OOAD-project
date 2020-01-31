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
    public class RatingsController : Controller
    {
        private RCMSEntities2 db = new RCMSEntities2();

        // GET: Ratings
        public ActionResult Index()
        {
            var ratings = db.Ratings.Include(r => r.Customer).Include(r => r.Organization).Include(r => r.Rating_Values);
            return View(ratings.ToList());
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.Rat_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname");
            ViewBag.Rat_orgId = new SelectList(db.Organizations, "org_id", "org_name");
            ViewBag.Rat_value = new SelectList(db.Rating_Values, "ID", "values");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rat_Id,Rat_CustId,Rat_orgId,Rat_value")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rat_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", rating.Rat_CustId);
            ViewBag.Rat_orgId = new SelectList(db.Organizations, "org_id", "org_name", rating.Rat_orgId);
            ViewBag.Rat_value = new SelectList(db.Rating_Values, "ID", "values", rating.Rat_value);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rat_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", rating.Rat_CustId);
            ViewBag.Rat_orgId = new SelectList(db.Organizations, "org_id", "org_name", rating.Rat_orgId);
            ViewBag.Rat_value = new SelectList(db.Rating_Values, "ID", "values", rating.Rat_value);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rat_Id,Rat_CustId,Rat_orgId,Rat_value")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rat_CustId = new SelectList(db.Customers, "Cust_id", "Cust_Firstname", rating.Rat_CustId);
            ViewBag.Rat_orgId = new SelectList(db.Organizations, "org_id", "org_name", rating.Rat_orgId);
            ViewBag.Rat_value = new SelectList(db.Rating_Values, "ID", "values", rating.Rat_value);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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

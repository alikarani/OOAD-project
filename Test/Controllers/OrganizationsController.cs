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
    public class OrganizationsController : Controller
    {
        private string Email;
        private RCMSEntities2 db = RCMSEntities2.getInstance();

        public ActionResult LoginOrRegister()
        {
            return View();
        }
        // POST: Organization/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult AuthorizeOrg(Authentication Auth)
        {
            TempData["Email"] = Auth.Email;
            var authResult = db.Authentications.Where(x => x.Email == Auth.Email && x.Password == Auth.Password && x.RoleID == 2).FirstOrDefault();
            if (authResult == null)
            {
                Auth.ErrorMessage = "Invalid Email or Password";
                return View("LoginOrRegister", Auth);
            }
            else
            {
                TempData["RoleID"] = 2;
                return RedirectToAction("Index");
            }
        }

        // GET: Organizations
        public ActionResult Index()
        {
            Email = (string )TempData["Email"];
            Session["OrgEmail"]=Email;
            Session["RoleID"] = TempData["RoleID"];
            var organizations = db.Organizations.Include(o => o.Ratings);
            return View(organizations.ToList());
        }

        // GET: Organizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // GET: Organizations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "org_id,org_name,address,Ph_no,RoleID,Password,Email,ConfirmPassword")] Organization organization)
        {
            organization.RoleID = 2;
            if (ModelState.IsValid)
            {
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("LoginOrRegister");
            }
            return View(organization);
        }

        // GET: Organizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "org_id,org_name,address,Ph_no,RoleID,Password,Email,ConfirmPassword")] Organization organization)
        {
            organization.RoleID = 2;
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LoginOrRegister");
            }
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
            db.SaveChanges();
            return RedirectToAction("LoginOrRegister");
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

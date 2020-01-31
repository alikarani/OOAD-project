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
    public class LoginProxyController : Controller, ILogin
    {
        private string Email;

        RCMSEntities2 db = RCMSEntities2.getInstance();
        SensitiveResource sr = null;
        [HttpPost]
        public ActionResult DoWork(Authentication Auth)
        {
            if (AuthenticateCustomer(Auth))
            {
                System.Web.HttpContext.Current.Session["CustEmail"] = Email;
                sr = new SensitiveResource();                         
                return sr.DoWork(Auth);
            }
            else
            {
                Auth.ErrorMessage = "Invalid Email or Password";
                return View("LoginOrRegister", Auth);
            }            
        }
        private bool AuthenticateCustomer(Authentication Auth)
        {
            Email = (String)Auth.Email;
            
            var authResult = db.Authentications.Where(x => x.Email == Auth.Email && x.Password == Auth.Password && x.RoleID == 1).FirstOrDefault();
            if (authResult == null)
            {

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
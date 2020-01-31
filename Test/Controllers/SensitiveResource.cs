using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
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
    public class SensitiveResource :Controller, ILogin
    {
        RCMSEntities2 db = RCMSEntities2.getInstance();

        public ActionResult DoWork(Authentication Auth)
        {
            System.Web.HttpContext.Current.Session["RoleID"] = 1;
            return RedirectToAction("Index", "Customers");
        }
    }
}
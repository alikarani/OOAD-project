using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    interface ILogin
    {
        ActionResult DoWork(Authentication Auth);
    }
}

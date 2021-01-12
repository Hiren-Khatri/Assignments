using ProductManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }
    }
}
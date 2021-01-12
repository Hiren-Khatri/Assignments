using ProductManagement.Models;
using ProductManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProductManagement.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //Post Method For Login
        [HttpPost]
        public ActionResult Login(MvcUserModel UserView,string ReturnUrl)
        {
            try
            {
                HttpResponseMessage Response = GlobalVariables.WebApiClient.PostAsJsonAsync("Users/Login", UserView).Result;

                if (Response.IsSuccessStatusCode)
                {
                    MvcUserModel user = Response.Content.ReadAsAsync<MvcUserModel>().Result;

                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["Email"] = user.Email;
                    Session["Password"] = user.Password;
                    Session["Username"] = user.FirstName + " " + user.LastName;
                    Session["UserId"] = user.Id;
                    if (ReturnUrl == null || ReturnUrl.Equals(""))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Incorrect Email or Password!");
                    return View("Index", UserView);
                }
            }catch (Exception Ex)
            {
                AppLogger.GetLogger("appLoggerRules").Error(Ex, "Exception generated in product index page.");
                return View("Index");
            }
        }

        //Logout Action
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
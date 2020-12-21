using NLog;
using SourceControlFinalAssignment.Models;
using SourceControlFinalAssignment.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SourceControlFinalAssignment.Controllers
{
    public class LoginController : Controller
    {
        private UserDBContext db = new UserDBContext();
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password,int rememberMeCheckBox=0)
        {
            try
            {
                User user = db.userList.ToList().Where(x => x.UserEmail.Equals(email) && x.UserPassword.Equals(password)).FirstOrDefault();

                if (user == null)
                {

                    if (db.userList.ToList().Where(x => x.UserEmail.Equals(email)).FirstOrDefault() != null)
                    {
                        AppLogger.GetLogger("appLoggerRules").Info("Login failed due to incorrect password for email " + email);
                        ModelState.AddModelError("Password", "Incorrect Password!");
                        ViewBag.Message = "Incorrect password!";
                        ViewBag.Email = email;
                    
                        return View();
                    }

                    AppLogger.GetLogger("appLoggerRules").Info("Login failed due to incorrect email.");
                    ModelState.AddModelError("Email", "No user registered with this email!");
                    ViewBag.Message = "No user registered with this email!";
                    return View();
                }

                else
                {
                    AppLogger.GetLogger("appLoggerRules").Info(email + " successfully logged in.");
                    if (rememberMeCheckBox == 1)
                    {
                        FormsAuthentication.SetAuthCookie(email, true);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(email, false);
                    }
                    return RedirectToAction("Details", "Home", new { id = user.UserID });
                }
            }catch (Exception e)
            {
                AppLogger.GetLogger("appLoggerRules").Error(e,"An Exception generated in LoginController "+e.Message);
                return Content("Exception in Login " + e.Message);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/");
        }
    }
}
using SourceControlFinalAssignment.Models;
using SourceControlFinalAssignment.Services.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SourceControlFinalAssignment.Controllers
{
    public class RegisterController : Controller
    {
        private UserDBContext db = new UserDBContext();

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserView user)
        {
            try
            {
                var validImageTypes = new string[]
                {
                "image/jpeg",
                "image/jpg",
                "image/png"
                };


                if (user.UserImage != null && user.UserImage.ContentLength != 0 && !validImageTypes.Contains(user.UserImage.ContentType))
                {
                    ModelState.AddModelError("UserImage", "Please choose either a  JPG or PNG image.");
                    return View(user);
                }

                if (ModelState.IsValid)
                {
                    User user1 = new User();
                    user1.UserID = user.UserID;
                    user1.UserName = user.UserName;
                    user1.UserEmail = user.UserEmail;
                    user1.UserPassword = user.UserPassword;
                    user1.UserBirthDate = user.UserBirthDate;
                    user1.UserPhone = user.UserPhone;

                    if (user.UserImage != null && user.UserImage.ContentLength > 0)
                    {
                        var uploadDir = "~/uploads";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), user.UserImage.FileName);
                        var imageUrl = Path.Combine(uploadDir, user.UserImage.FileName);
                        user.UserImage.SaveAs(imagePath);
                        user1.UserImage = imageUrl;
                    }

                    db.userList.Add(user1);
                    db.SaveChanges();

                    AppLogger.GetLogger("appLoggerRules").Info("New User Registered with email " + user1.UserEmail);

                    FormsAuthentication.SetAuthCookie(user1.UserEmail, false);
                    return RedirectToAction("Details", "Home", new { id = user1.UserID });
                }

                return View(user);
            }catch (Exception e)
            {
                AppLogger.GetLogger("appLoggerRules").Error(e,"Exception generated while creating new user.");
                return View();
            }
        }

    }
}
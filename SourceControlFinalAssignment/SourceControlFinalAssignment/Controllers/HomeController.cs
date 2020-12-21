using NLog;
using SourceControlFinalAssignment.Models;
using SourceControlFinalAssignment.Services.Utilities;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace SourceControlFinalAssignment.Controllers
{
    public class HomeController : Controller
    {
        private UserDBContext db = new UserDBContext();

        public ActionResult Index()
        {
          
            if (Request.IsAuthenticated)
            {
                using (UserDBContext db = new UserDBContext())
                {
                    int userID = db.userList.ToList().Where(x => User.Identity.Name.Equals(x.UserEmail)).FirstOrDefault().UserID;
                    
                    if(userID == 0)
                    {
                        return View();
                    }
                    return RedirectToAction("Details", new { id = userID });
                }
            }
            return View();
        }

       [Authorize]
        public ActionResult Details(int? id)
        {
            //Get Current User Id from cookie/session
            int currentUserId = db.userList.FirstOrDefault(x => x.UserEmail == System.Web.HttpContext.Current.User.Identity.Name).UserID;

            //if user tries to access other user's id send him to details page with his id
            if (id != currentUserId)
            {
                id = currentUserId;
            }

             User user = db.userList.Find(id);
                return View(user);

        }

      [Authorize]
        public ActionResult Edit(int? id)
        {  
            //Get Current User Id from cookie/session
            int currentUserId = db.userList.FirstOrDefault(x => x.UserEmail == System.Web.HttpContext.Current.User.Identity.Name).UserID;

            //if user tries to access other user's id send him to edit page with his id
            if (id != currentUserId) {
                id = currentUserId;
            }
            
                User user = db.userList.Find(id);

                if (user != null)
                {
                    UserView userView = new UserView()
                    {
                        UserID = user.UserID,
                        UserName = user.UserName,
                        UserEmail = user.UserEmail,
                        UserPassword = user.UserPassword,
                        UserPhone = user.UserPhone,
                        UserBirthDate = user.UserBirthDate,
                        ReType = user.UserPassword
                    };
                    ViewBag.UserImage = user.UserImage;
                    return View(userView);
                }
                else
                {
                    return View();
                }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(UserView userView)
        {
            if (ModelState.IsValid)
            {
                int id = db.userList.FirstOrDefault(x => x.UserEmail == System.Web.HttpContext.Current.User.Identity.Name).UserID;

                var validImageTypes = new string[]
                {
                    "image/jpeg",
                    "image/jpg",
                    "image/png"
                };

                if (userView.UserImage != null && userView.UserImage.ContentLength != 0 && !validImageTypes.Contains(userView.UserImage.ContentType))
                {
                    ModelState.AddModelError("UserImage", "Please choose either a  JPG or PNG image.");
                    return View(userView);
                }


                using (UserDBContext db = new UserDBContext())
                {
                    User preUser = db.userList.Find(id);
                    string previousUserImagePath = Server.MapPath(preUser.UserImage); 

                    if(preUser != null)
                    {
                        string email = preUser.UserEmail;
                        
                        preUser.UserName = userView.UserName;
                        preUser.UserEmail = userView.UserEmail;
                        preUser.UserPhone = userView.UserPhone;
                        preUser.UserBirthDate = userView.UserBirthDate;
                        preUser.UserPassword = userView.UserPassword;

                        if (userView.UserImage != null && userView.UserImage.ContentLength > 0)
                        {
                            var uploadDir = "~/uploads";

                            string fileName = Path.GetFileNameWithoutExtension(userView.UserImage.FileName);
                            string fileExtension = Path.GetExtension(userView.UserImage.FileName);

                            var imagePath = Path.Combine(Server.MapPath(uploadDir), fileName+"_"+id+fileExtension);
                            var imageUrl = Path.Combine(uploadDir, fileName + "_" + id + fileExtension);
                            userView.UserImage.SaveAs(imagePath);
                            preUser.UserImage = imageUrl;

                            if (System.IO.File.Exists(previousUserImagePath))
                            {
                                System.IO.File.Delete(previousUserImagePath);
                            }
                        }

                        if (!email.Equals(userView.UserEmail))
                        {
                            FormsAuthentication.SetAuthCookie(userView.UserEmail, false);
                        }

                        db.Entry(preUser).State = EntityState.Modified;
                        db.SaveChanges();

                        

                        return RedirectToAction("Details", new { id = id}); 
                    }
                    else
                    {
                        return View(userView);
                    }
                }
            }
            else
            {
                return View(userView);
            }
        }

    }
}
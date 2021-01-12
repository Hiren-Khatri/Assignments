using ProductManagement.Models;
using ProductManagement.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    [Authorize]// =>froms authentication
    public class ProductController : Controller
    {
        readonly IEnumerable<MvcProductCategoryModel> ProductCategories;

        //adding Basic authentication attaribute to request header
        // and loading product categories to 
        public ProductController()
        {
            //reading email and password from session
            //and converting into byes
            var EmailPasswordInByte = Encoding.ASCII.GetBytes(System.Web.HttpContext.Current.Session["Email"] + ":" + System.Web.HttpContext.Current.Session["Password"]);
            
            //cenverting bytes to base 64 string and adding it to webapi request header
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(EmailPasswordInByte));

            HttpResponseMessage categoryResponse = GlobalVariables.WebApiClient.GetAsync("ProductCategories").Result;
            if (categoryResponse.IsSuccessStatusCode)
            {
                 ProductCategories = categoryResponse.Content.ReadAsAsync<IEnumerable<MvcProductCategoryModel>>().Result;
            }
            else
            {
                ProductCategories = new List<MvcProductCategoryModel>();
            }
        }
       

        // GET: Product
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                    return new HttpUnauthorizedResult();
            }
            
            try
            {
                //storing categories to viewbag to show in category dropdown 
                ViewBag.ProductCategories = ProductCategories;
                IEnumerable<MvcProductModel> ProductList;
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Products").Result;
                if (response.IsSuccessStatusCode)
                {
                    ProductList = response.Content.ReadAsAsync<IEnumerable<MvcProductModel>>().Result;
                    return View(ProductList);
                }
                else
                {
                    return new HttpUnauthorizedResult();
                }
            }catch (Exception Ex)
            {
                AppLogger.GetLogger("appLoggerRules").Error(Ex, "Exception generated in product index page.");
                return View();
            }
        }

        public ActionResult AddOrEdit(int Id = 0)
        {
            if (Session["Username"] == null)
            {
                return new HttpUnauthorizedResult();
            }

            try
            {
                //storing categories to viewbag to show in category dropdown 
                ViewBag.ProductCategories = ProductCategories;

                if (Id == 0)
                {
                    return View(new MvcProductModel());
                }
                else
                {
                    HttpResponseMessage Response = GlobalVariables.WebApiClient.GetAsync("Products/" + Id.ToString()).Result;
                    if (Response.IsSuccessStatusCode)
                    {
                        MvcProductModel dbProductModel = Response.Content.ReadAsAsync<MvcProductModel>().Result;
                        //dbProductModel.productCategories = mvcProductModel.productCategories;
                        return View(dbProductModel);
                    }
                    else
                    {
                        return View(new MvcProductModel()
                        {
                            //productCategories = mvcProductModel.productCategories
                        });
                    }
                }
            }catch(Exception E)
            {
                AppLogger.GetLogger("appLoggerRules").Error(E, "Exception generated in product index page.");
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(MvcProductModel Model)
        {
            if (Session["Username"] == null)
            {
                return new HttpUnauthorizedResult();
            }

            try
            {
                //storing categories to viewbag to show in category dropdown 
                ViewBag.ProductCategories = ProductCategories;

                if (ModelState.IsValid)
                {
                    if (Model.Id == 0)
                    {
                        if (Model.SmallImageFile == null)
                        {
                            ModelState.AddModelError("SmallImageFile", "Required*");
                            return View(Model);
                        }
                        if(Session["UserId"] == null)
                        {
                            return new HttpUnauthorizedResult();
                        }
                    }
                    else
                    {
                        HttpResponseMessage ResponseMessage = GlobalVariables.WebApiClient.GetAsync("Products/" + Model.Id.ToString()).Result;
                        if (ResponseMessage.IsSuccessStatusCode)
                        {
                            var dbProductModel = ResponseMessage.Content.ReadAsAsync<MvcProductModel>().Result;
                            Model.SmallImage = dbProductModel.SmallImage;
                            Model.LargeImage = dbProductModel.LargeImage;
                            Model.CreatedBy = dbProductModel.CreatedBy;
                            Model.CreatedDate = dbProductModel.CreatedDate;

                            if (Model.SmallImageFile != null)
                            {
                                DeletePreviousUploadImage(Server.MapPath(dbProductModel.SmallImage));
                            }

                            if (Model.LargeImageFile != null)
                            {
                                DeletePreviousUploadImage(Server.MapPath(dbProductModel.LargeImage));
                            }
                        }
                    }

                    if (Model.SmallImageFile != null)
                    {
                        Model.SmallImage = SaveImage(Model.SmallImageFile, true);
                        Model.SmallImageFile = null;
                    }

                    if (Model.LargeImageFile != null)
                    {
                        Model.LargeImage = SaveImage(Model.LargeImageFile, false);
                        Model.LargeImageFile = null;
                    }

                    HttpResponseMessage Response;
                    if (Model.Id == 0)
                    {
                        Model.CreatedBy = Int32.Parse(Session["UserId"].ToString());
                        Model.CreatedDate = DateTime.Now;
                        Response = GlobalVariables.WebApiClient.PostAsJsonAsync("Products", Model).Result;
                        if (Response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = Model.Name + " Added Successfully!";
                            return RedirectToAction("Index");
                        }
                        else{
                            TempData["ErrorMessage"] = "Error While Adding Product!";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                        Model.ModifiedBy = Int32.Parse(Session["UserId"].ToString());
                        Model.ModifiedDate = DateTime.Now;
                    {
                        Response = GlobalVariables.WebApiClient.PutAsJsonAsync("Products/" + Model.Id, Model).Result;
                        if (Response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = Model.Name + " Updated Successfully!";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "An Error occured while updating product!";
                            return View(Model);
                        }
                    }
                }
                TempData["ErrorMessage"] = "Please Enter all the details!";
                return View(Model);
            }catch (Exception E)
            {
                AppLogger.GetLogger("appLoggerRules").Error(E, "Exception generated in product AddOrEdit page.");
                return View();
            }
        }

        public ActionResult Delete(int Id)
        {
            if (Session["Username"] == null)
            {
                return new HttpUnauthorizedResult();
            }

            try
            {
                if (Id != 0)
                {
                    HttpResponseMessage Response = GlobalVariables.WebApiClient.DeleteAsync("Products/" + Id.ToString()).Result;
                    if (Response.IsSuccessStatusCode)
                    {
                        MvcProductModel DeletedProductModel = Response.Content.ReadAsAsync<MvcProductModel>().Result;

                        if (DeletedProductModel.SmallImage != null)
                        {
                            DeletePreviousUploadImage(Server.MapPath(DeletedProductModel.SmallImage));
                        }

                        if (DeletedProductModel.LargeImage != null)
                        {
                            DeletePreviousUploadImage(Server.MapPath(DeletedProductModel.LargeImage));
                        }

                        TempData["SuccessMessage"] = DeletedProductModel.Name + " Deleted Successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "An Error Occurred While Deleting Product, Please Try Again!";
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }catch (Exception E)
            {
                AppLogger.GetLogger("appLoggerRules").Error(E, "Exception generated while deleting Product.");
                return View();
            }
        }

        public ActionResult DeleteMultiple(int[] Ids)
        {
            if (Session["Username"] == null)
            {
                return new HttpUnauthorizedResult();
            }

            try
            {
                foreach (int Id in Ids)
                {
                    if (Id != 0)
                    {
                        HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Products/" + Id.ToString()).Result;
                        if (response.IsSuccessStatusCode)
                        {

                            MvcProductModel DeletedProductModel = response.Content.ReadAsAsync<MvcProductModel>().Result;

                            if (DeletedProductModel.SmallImage != null)
                            {
                                DeletePreviousUploadImage(Server.MapPath(DeletedProductModel.SmallImage));
                            }

                            if (DeletedProductModel.LargeImage != null)
                            {
                                DeletePreviousUploadImage(Server.MapPath(DeletedProductModel.LargeImage));
                            }

                            if (Id == Ids[Ids.Length - 1])
                            {
                                TempData["SuccessMessage"] = "Multiple Products Deleted Successfully!";
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "An Error Occurred While Deleting Product, Please Try Again!";
                            return RedirectToAction("Index");
                        }
                    }
                }
                return View();
            }catch (Exception E)
            {
                AppLogger.GetLogger("appLoggerRules").Error(E, "Exception generated while deleting multiple Products.");
                return View();
            }
        }

        private string SaveImage(HttpPostedFileBase Image, bool IsSmallImage)
        {
            string uploadDir;

            if (IsSmallImage)
            {
                uploadDir = "~/Images/Small_Images";
            }
            else
            {
                uploadDir = "~/Images/Large_Images";
            }

            string fileName = Path.GetFileNameWithoutExtension(Image.FileName);
            string fileExtension = Path.GetExtension(Image.FileName);

            var ticks = Convert.ToString((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            var imagePath = Path.Combine(Server.MapPath(uploadDir), fileName + "_" + ticks + fileExtension);
            var imageUrl = Path.Combine(uploadDir, fileName + "_" + ticks + fileExtension);

            Image.SaveAs(imagePath);
            return imageUrl;
        }

        private void DeletePreviousUploadImage(string ImgPath)
        {
            if (System.IO.File.Exists(ImgPath))
            {
                System.IO.File.Delete(ImgPath);
            }
        }
    }
}
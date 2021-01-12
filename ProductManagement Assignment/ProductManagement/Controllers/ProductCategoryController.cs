using ProductManagement.Models;
using ProductManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class ProductCategoryController : Controller
    {

        //adding Basic authentication attaribute to request header
        public ProductCategoryController()
        {
            //reading email and password from session
            //and converting into byes
            var EmailPasswordInByte = Encoding.ASCII.GetBytes(System.Web.HttpContext.Current.Session["Email"] + ":" + System.Web.HttpContext.Current.Session["Password"]);
            //ConvertingEmailPasswrodInByte To Base64 String And Adding it into the HttpClient Headers as Authorization Attribute
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(EmailPasswordInByte));
        }

        // GET: ProductCategory
        public ActionResult Index()
        { 
            //Username is null ,redirect User To Login Page By Returning UnAuthorized
            if(Session["Username"] == null)
            {
                return new HttpUnauthorizedResult();
            }
            try
            {
                //Sending Get Request To WebApi ProductCategoriesController
                HttpResponseMessage categoryResponse = GlobalVariables.WebApiClient.GetAsync("ProductCategories").Result;

                if (categoryResponse.IsSuccessStatusCode)
                {
                    IEnumerable<MvcProductCategoryModel> productCategoryList = categoryResponse.Content.ReadAsAsync<IEnumerable<MvcProductCategoryModel>>().Result;
                    return View(productCategoryList);
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

        //add or edit from same page
        //if Id is 0 => it's new  else user is updating Category
        public ActionResult AddOrEdit(int Id = 0)
        {
            //Username is null ,redirect User To Login Page By Returning UnAuthorized
            if (Session["Username"] == null)
            {
                return new HttpUnauthorizedResult();
            }

            try
            {
                if (Id == 0)
                {
                    return View(new MvcProductCategoryModel());
                }
                else
                {
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProductCategories/" + Id.ToString()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MvcProductCategoryModel dbProductCategoryModel = response.Content.ReadAsAsync<MvcProductCategoryModel>().Result;
                        //dbProductModel.productCategories = mvcProductModel.productCategories;
                        return View(dbProductCategoryModel);
                    }
                    else
                    {
                        return View(new MvcProductCategoryModel());
                    }
                }
            }catch (Exception E)
            {
                //log exception
                AppLogger.GetLogger("appLoggerRules").Error(E, "Exception generated while getting AddOrUpdate Page for ProductCategory.");
                return View();
            }
        }

        //handling post request of adding or updating productcategory
        //if id is 0 => it's new productcategory else user is updating 
        [HttpPost]
        public ActionResult AddOrEdit(MvcProductCategoryModel ProductCategory)
        {
            if (Session["Username"] == null)
            {
                return new HttpUnauthorizedResult();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage Response;
                    if (ProductCategory.Id == 0)
                    {
                        //Sending Post Request To WebApiProductCategoriesController (Adding New Category)
                        Response = GlobalVariables.WebApiClient.PostAsJsonAsync("ProductCategories", ProductCategory).Result;
                        if (Response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = ProductCategory.Name + " Added Successfully!";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            if(Response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                TempData["ErrorMessage"] = "Category Already Exists With Name " + ProductCategory.Name;
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Error while adding " + ProductCategory.Name;
                            }
                            return View(ProductCategory);
                        }
                    }
                    else
                    {
                        //Sending Put Request To WebApiProductCategoriesController (Updating Exsisting Category )
                        Response = GlobalVariables.WebApiClient.PutAsJsonAsync("ProductCategories/" + ProductCategory.Id, ProductCategory).Result;
                        if (Response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = ProductCategory.Name + " Updated Successfully!";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "An Error occured While Updating Category " + Response.StatusCode;
                            return View(ProductCategory);
                        }
                    }
                }
                else
                {
                    return View(ProductCategory);
                }
            }catch (Exception E)
            {
                //Log Exception
                AppLogger.GetLogger("appLoggerRules").Error(E, "Exception generated while adding or updating ProductCategory.");
                return View();
            }
        }

        //deleting product
        public ActionResult Delete(int Id)
        {
            //Username is null ,redirect User To Login Page By Returning UnAuthorized
            if (Session["Username"] == null)
            {
                return new HttpUnauthorizedResult();
            }

            try {
                if (Id != 0)
                {
                    //Sending Delete Request To WebaPi ProductCategoriesController
                    HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("ProductCategories/" + Id.ToString()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MvcProductCategoryModel mvcProductCategory = response.Content.ReadAsAsync<MvcProductCategoryModel>().Result;
                        TempData["SuccessMessage"] = mvcProductCategory.Name + " Deleted Successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
                        {
                            TempData["ErrorMessage"] = response.Content.ReadAsStringAsync().Result;

                        }
                        else
                        {
                        TempData["ErrorMessage"] = "An Error Occurred While Deleting Product, Please Try Again!";
                        }

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }catch (Exception E)
            {
                AppLogger.GetLogger("appLoggerRules").Error(E, "Exception generated while deleting ProductCategory.");
                return View("Index");
            }
         }
    }
}
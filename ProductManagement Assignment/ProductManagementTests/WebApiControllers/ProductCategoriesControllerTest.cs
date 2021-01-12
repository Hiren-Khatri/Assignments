using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagementWebApi.Controllers;
using ProductManagementWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ProductManagementTests.WebApiControllers
{
    [TestClass]
    public class ProductCategoriesControllerTest
    {
        //Send Get Request With Authorization Header
        [TestMethod]
        public void GetAllProductCategoriesTest_ShouldReturnUnauthorized()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            //Act
            HttpResponseMessage CategoryResponse = WebApiClient.GetAsync("ProductCategories").Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, CategoryResponse.StatusCode);
            WebApiClient.Dispose();
        }

        [TestMethod]
        public void GetAllProductCategoriesTest_ShouldReturnProducts()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            //Act
            HttpResponseMessage CategoryResponse = WebApiClient.GetAsync("ProductCategories").Result;
            var ProductCategoryList = CategoryResponse.Content.ReadAsAsync<IEnumerable<ProductCategory>>().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, CategoryResponse.StatusCode);
            Assert.AreNotEqual(ProductCategoryList.Count(), 0);
            WebApiClient.Dispose();
        }

        [TestMethod]
        public void PostProductCategoryTest_ShouldReturnSuccess()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            var productCategory = new ProductCategory()
            {
                Name = "Fashion"
            };

            //Act
            HttpResponseMessage Response = WebApiClient.PostAsJsonAsync("ProductCategories", productCategory).Result;
            var Category = Response.Content.ReadAsAsync<ProductCategory>().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Created,Response.StatusCode);
            Assert.IsNotNull(Category);
            WebApiClient.Dispose();
        }

        //There is no Category With Id 5 ,Should Return NotFound
        [TestMethod]
        public void DeleteProductCategoryTest_ShouldReturnNotFound()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            //Act
            HttpResponseMessage Response = WebApiClient.DeleteAsync("ProductCategories/5").Result;
            var DeletedCategory = Response.Content.ReadAsAsync<ProductCategory>().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, Response.StatusCode);
            Assert.IsNull(DeletedCategory);

            WebApiClient.Dispose();
        }

        [TestMethod]
        public void UpdateProductCategoryTest_ShouldReturnAccepted()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            ProductCategory UpdateCategory = new ProductCategory()
            {
                Id = 1,
                Name = "Mobile"
            };

            //Act
            HttpResponseMessage Response = WebApiClient.PutAsJsonAsync("ProductCategories/"+UpdateCategory.Id,UpdateCategory).Result;
            
            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, Response.StatusCode);

            WebApiClient.Dispose();
        }
    }
}

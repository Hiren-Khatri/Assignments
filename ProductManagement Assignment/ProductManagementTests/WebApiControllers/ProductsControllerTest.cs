using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ProductsControllerTest
    {
        [TestMethod]
        public void GetAllProductsTest_ShouldReturnUnAuthorized()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            HttpResponseMessage Response = WebApiClient.GetAsync("Products").Result;
            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, Response.StatusCode);
            WebApiClient.Dispose();
        }

        [TestMethod]
        public void GetAllProductsTest_ShouldReturnAllProducts()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            //Act
            HttpResponseMessage Response = WebApiClient.GetAsync("Products").Result;
            var ProductList = Response.Content.ReadAsAsync<IEnumerable<Product>>().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
            Assert.AreNotEqual(0, ProductList.Count());
            WebApiClient.Dispose();
        }

        [TestMethod]
        public void PostProductTest_ShouldReturnAddedProduct()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            Product ProductToBeAdded = new Product()
            {
                Name="Abc Xyz",
                CategoryId=1,
                CreatedBy=1,
                CreatedDate=DateTime.Now,
                Price=1200,
                Quantity=5,
                ShortDescription="abcdefxyz",
                SmallImage= "~/Images/Small_Images\\Picture2_1609938345.png",
            };

            //Act
            HttpResponseMessage Response = WebApiClient.PostAsJsonAsync("Products",ProductToBeAdded).Result;
            var AddedProduct = Response.Content.ReadAsAsync<Product>().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, Response.StatusCode);
            Assert.IsNotNull( AddedProduct);
            WebApiClient.Dispose();
        }

        [TestMethod]
        //Request Should Return Bad Request bcz Some Properties are not provided
        public void UpdateProductTest_ShouldReturnBadRequest()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            Product ProductToBeAdded = new Product()
            {
                Name = "Abc Xyz1",
                Quantity = 5,
                ShortDescription = "abcdefxyz",
                SmallImage = "~/Images/Small_Images\\Picture2_1609938345.png",
            };

            //Act
            HttpResponseMessage Response = WebApiClient.PutAsJsonAsync("Products/18", ProductToBeAdded).Result;
            
            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, Response.StatusCode);
            WebApiClient.Dispose();
        }

        [TestMethod]
        public void UpdateProductTest_ShouldReturnNoContent()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            Product ProductToBeAdded = new Product()
            {
                Id=18,
                Name = "Abc Xyz1",
                CategoryId = 1,
                CreatedBy = 1,
                CreatedDate = new DateTime(2021,01,08,20,12,12),
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now,
                Price = 1200,
                Quantity = 5,
                ShortDescription = "abcdefxyz",
                SmallImage = "~/Images/Small_Images\\Picture2_1609938345.png",
            };

            //Act
            HttpResponseMessage Response = WebApiClient.PutAsJsonAsync("Products/18", ProductToBeAdded).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, Response.StatusCode);
            WebApiClient.Dispose();
        }

        //No Product With Id 16,Should Return NotFound
        [TestMethod]
        public void UpdateProductTest_ShouldReturnNotFound()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            Product ProductToBeAdded = new Product()
            {
                Id = 16,
                Name = "Abc Xyz1",
                CategoryId = 1,
                CreatedBy = 1,
                CreatedDate = new DateTime(2021, 01, 08, 20, 12, 12),
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now,
                Price = 1200,
                Quantity = 5,
                ShortDescription = "abcdefxyz",
                SmallImage = "~/Images/Small_Images\\Picture2_1609938345.png",
            };

            //Act
            HttpResponseMessage Response = WebApiClient.PutAsJsonAsync("Products/16", ProductToBeAdded).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, Response.StatusCode);
            WebApiClient.Dispose();
        }

        //No Product With Id 16 Should Return NotFound
        [TestMethod]
        public void DeleteProductTest_ShouldReturnNotFound()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            //Act
            HttpResponseMessage Response = WebApiClient.DeleteAsync("Products/16").Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, Response.StatusCode);
            WebApiClient.Dispose();
        }

        //Product With Id 17 Is There So Should Return OK
        [TestMethod]
        public void DeleteProductTest_ShouldReturnOK()
        {
            //Assert
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW5AZ21haWwuY29tOkFkbWluQDEyMw ==");

            //Act
            HttpResponseMessage Response = WebApiClient.DeleteAsync("Products/19").Result;
            var DeletedProducted = Response.Content.ReadAsAsync<Product>().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
            Assert.IsNotNull(DeletedProducted);
            Assert.AreEqual(19, DeletedProducted.Id);
            WebApiClient.Dispose();
        }

    }
}

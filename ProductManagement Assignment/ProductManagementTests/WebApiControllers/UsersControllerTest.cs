using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagementWebApi.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ProductManagementTests.WebApiControllers
{
    [TestClass]
    public class UsersControllerTest
    {
        //As there is no User With abc@gmail.com Abc123
        [TestMethod]
        public void LoginTestMethod_ShouldReturnUnAuthorized()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            User UserModel = new User()
            {
                Email="abc@gmail.com",
                Password = "Abc123"
            };

            //Act
            HttpResponseMessage Response = WebApiClient.PostAsJsonAsync("Users/Login",UserModel).Result;
            var GotUser = Response.Content.ReadAsAsync<User>().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, Response.StatusCode);
            Assert.IsNull(GotUser);
        }

        //As there is no User With admin@gmail.com, Admin@123
        [TestMethod]
        public void LoginTestMethod_ShouldReturnOK()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44361/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            User UserModel = new User()
            {
                Email = "admin@gmail.com",
                Password = "Admin@123"
            };

            //Act
            HttpResponseMessage Response = WebApiClient.PostAsJsonAsync("Users/Login", UserModel).Result;
            var GotUser = Response.Content.ReadAsAsync<User>().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
            Assert.IsNotNull(GotUser);
            Assert.AreEqual(UserModel.Email, GotUser.Email);
            Assert.AreEqual(UserModel.Password, GotUser.Password);
        }
    }
}

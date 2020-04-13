using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafewalkApplication.Models;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SafewalkApplication.Tests
{
    [TestClass]
    public class UserTests
    {
        private readonly HttpClient _client;

        public UserTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        const string User11Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImRjODRiNDRkLTA0MjUtNGI0Ny05ZDM1LWRiY2M3ZDQ2YTFjNSIsIm5iZiI6MTU4Njc1NDY5OCwiZXhwIjoxNTg2ODQxMDk4LCJpYXQiOjE1ODY3NTQ2OTh9.iGAJgUurTsBLvIscj9Hpu3Hlyjx9LTMm5f_Lzk-JIi8";
        const string User12Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFmMDAwNDBiLWFlZTMtNDg5ZS1hMWQ5LWIxNzNkZjZkM2NkMCIsIm5iZiI6MTU4Njc1NDcxMSwiZXhwIjoxNTg2ODQxMTExLCJpYXQiOjE1ODY3NTQ3MTF9.S2HiceiEgseadYK2tQVXuW-7-rqZCsMk1A_s95D5p1Y";
        const string User13Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjFhODBiNDJiLWIyY2QtNDNjMy04OGIzLTRmZmJhOTRjMTUyYSIsIm5iZiI6MTU4Njc1NDcyMywiZXhwIjoxNTg2ODQxMTIzLCJpYXQiOjE1ODY3NTQ3MjN9.gyt34frNCSikR-ZSIC-8M1toxaChhShtU-7XItlV12k";
        const string User14Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ijc2YTc4NTMyLWNhNzMtNGFhMC1iNjMwLWFlMjRhZDM1YjUwMSIsIm5iZiI6MTU4Njc1NDczNSwiZXhwIjoxNTg2ODQxMTM1LCJpYXQiOjE1ODY3NTQ3MzV9.3_Gh80-ncDokh5sr-JUkVMCIo1lAtGYAiQD71P9UUzs";
        const string User15Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjVmOWVmODA3LTg5NmEtNDVhNy1iYjJiLWE4OGE1MDA2MmNiZSIsIm5iZiI6MTU4Njc1NDc0NSwiZXhwIjoxNTg2ODQxMTQ1LCJpYXQiOjE1ODY3NTQ3NDV9.lJv_UmL4FPLkqqeNnwx77DY7zHlMq_OCI61J3MDpD-E";

        const string User11Email = "usertest11@wisc.edu";
        const string User12Email = "usertest12@wisc.edu";
        const string User13Email = "usertest13@wisc.edu";
        const string User14Email = "usertest14@wisc.edu";
        const string User15Email = "usertest15@wisc.edu";

        const string SafeWalkerPassword = "test11";


        // GetUser Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow(User11Token, User11Email, User11Email, "true")]
        [DataRow(User12Token, User12Email, User12Email, "true")]
        [DataRow(User13Token, User13Email, User13Email, "true")]
        public void GetUser_Ok(string token, string email, string userEmail, string isUser)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Users/{userEmail}");

            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);
            request.Headers.Add("email", email);
            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        [DataRow("dummytoken", User11Email, "true")]
        [DataRow(User12Token, "Fakeycho@wisc.edu", "true")]
        [DataRow("dummytoken", User13Email, "true")]
        public void GetUser_Unauthorized(string token, string email, string isUser)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Users/{email}");

            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);
            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
/*
        // PutUser Tests ----------------------------------------------------------------------
        [TestMethod]
        [DataRow(User14Token, "fakedeuman@wisc.edu", "{\"LastName\":\"Deuman\", \"FirstName\":\"Freakin\", \"Password\":\"crap11\", \"PhoneNumber\":\"4567890\", \"HomeAddress\":\"lakeshore\"}")]
        [DataRow("dummytoken", User14Email, "{\"LastName\":\"Cho\", \"FirstName\":\"Yoon\", \"Password\":\"cat11\", \"PhoneNumber\":\"8342532244\", \"HomeAddress\":\"9384 street\"}")]
        public void PutUser_Unauthorized(string token, string email, string user)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/Users/{email}");

            request.Headers.Add("token", token);
            request.Content = new StringContent(user, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        [DataRow(User14Token, User14Email, "{\"LastName\":\"Deuman\", \"FirstName\":\"Freakin\", \"Password\":\"crap11\", \"PhoneNumber\":\"4567890\", \"HomeAddress\":\"lakeshore\"}")]
        [DataRow(User15Token, User15Email, "{\"LastName\":\"Cho\", \"FirstName\":\"Yoon\", \"Password\":\"cat11\", \"PhoneNumber\":\"8342532244\", \"HomeAddress\":\"9384 street\"}")]
        public void PutUser_Ok(string token, string email, string user)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/Users/{email}");

            request.Headers.Add("token", token);
            request.Content = new StringContent(user, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        // PostUser Tests ----------------------------------------------------------------------


        [TestMethod]
        [DataRow("{\"LastName\":\"Bajkowski\", \"FirstName\":\"Katie\", \"Password\":\"mouse11\", \"PhoneNumber\":\"4583974\", \"HomeAddress\":\"Southeast\", \"Interest\":\"running\", \"Email\":\"katie@wisc.edu\"}")]
        [DataRow("{\"LastName\":\"Latonis\", \"FirstName\":\"Jacob\", \"Password\":\"hello11\", \"PhoneNumber\":\"3432343\", \"HomeAddress\":\"Taco Bell\", \"Interest\":\"Biking\", \"Email\":\"jacob@wisc.edu\"}")]
        public void PostUser_Ok(string user)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"/api/Users/");
            request.Content = new StringContent(user, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow("{\"LastName\":\"Bajkowski\", \"FirstName\":\"Katie\", \"Password\":\"mouse11\", \"PhoneNumber\":\"4583974\", \"HomeAddress\":\"Southeast\", \"Interest\":\"running\", \"Email\":\"katie@wisc.edu\"}")]
        [DataRow("{\"LastName\":\"Latonis\", \"FirstName\":\"Jacob\", \"Password\":\"hello11\", \"PhoneNumber\":\"3432343\", \"HomeAddress\":\"Taco Bell\", \"Interest\":\"Biking\", \"Email\":\"jacob@wisc.edu\"}")]
        public void PostUser_Conflict(string user)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"/api/Users/");
            request.Content = new StringContent(user, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }

        // DeleteUser Tests ----------------------------------------------------------------------


        [TestMethod]
        [DataRow("dummyToken", "katie@wisc.edu")]
        [DataRow("dummyToken", "jacob@wisc.edu")]
        public void DeleteUser_Unauthorized(string token, string email)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"/api/Users/{email}");
            request.Headers.Add("token", token);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjY2ZGQwYzM3LTllNTMtNDJhNi1hZjc5LTcxOTY3MzQ0MTA5MCIsIm5iZiI6MTU4Njc1NzA5MywiZXhwIjoxNTg2ODQzNDkzLCJpYXQiOjE1ODY3NTcwOTN9.XKZQBhcnGnjZI09p3o0A-kxhgGk2RvGMW70U60XgCvs", "ycho73@wisc.edu")]
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjFmOGI5NWExLTE3MmQtNGRmMi05NzBlLWYwMjAwM2ViZDUwMyIsIm5iZiI6MTU4Njc1NzE1NSwiZXhwIjoxNTg2ODQzNTU1LCJpYXQiOjE1ODY3NTcxNTV9.XedzpDalqPbXwbslJdRRIcLYaPWMtAm7yaT2kmXZKys", "ycho74@wisc.edu")]
        public void DeleteUser_Ok(string token, string email)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"/api/Users/{email}");
            request.Headers.Add("token", token);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }*/
    }
}

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
    public class SafewalkerTests
    {
        private readonly HttpClient _client;

        public SafewalkerTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        const string SafeWalker11Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImY3N2JlMDk4LTI0YWEtNDQxNi1iNzc1LTY4OGU3YjUzYzk4ZSIsIm5iZiI6MTU4Njc1NDM4NSwiZXhwIjoxNTg2ODQwNzg1LCJpYXQiOjE1ODY3NTQzODV9.J5Zv3EYgH3K9oJMmg7hORrHRHNLCxjLF2t3bRCaJHcc";
        const string SafeWalker12Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjFhMWViMGFjLTgxNTEtNGIzYi1hZmVkLTc3NWRjOTYzYzY3ZiIsIm5iZiI6MTU4Njc1NDQ3NSwiZXhwIjoxNTg2ODQwODc1LCJpYXQiOjE1ODY3NTQ0NzV9.3Mq-CJ8kl6CviA4jHiTRYBFynNCZUNt1kri1uCb0ov0";
        const string SafeWalker13Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImZiODA2ZWI0LWE1YmQtNDk1Mi1iZWIzLWIyYzdjZTU4MGE4MyIsIm5iZiI6MTU4Njc1NDQ4NiwiZXhwIjoxNTg2ODQwODg2LCJpYXQiOjE1ODY3NTQ0ODZ9.wEXi8BTwkK9d11_MxUFG8-tYuc_6bdTNDiVQ1YnuFho";
        const string SafeWalker14Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjdiZTJjNTNjLTczOGItNDEwZC1hNzg1LWY5YjYxNmJiZjk4MyIsIm5iZiI6MTU4Njc1NDQ5NywiZXhwIjoxNTg2ODQwODk3LCJpYXQiOjE1ODY3NTQ0OTd9.B6hatRrCdmlOBkVLqndJ1S8_JtibUzb8PtgD1CmYmu4";
        const string SafeWalker15Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImVjMzI4NDlhLTEyMzYtNDU3Mi1hODcyLTIwYWYwZWM2ZWIxMSIsIm5iZiI6MTU4Njc1NDU3MCwiZXhwIjoxNTg2ODQwOTcwLCJpYXQiOjE1ODY3NTQ1NzB9.YBy8jksddy67-7CauhFkzNvatIAK5f1ypOD0UEFYtcQ";

        const string SafeWalker11Email = "walkertest11@wisc.edu";
        const string SafeWalker12Email = "walkertest12@wisc.edu";
        const string SafeWalker13Email = "walkertest13@wisc.edu";
        const string SafeWalker14Email = "walkertest14@wisc.edu";
        const string SafeWalker15Email = "walkertest15@wisc.edu";

        const string SafeWalkerPassword = "test11";

        // GetSafewalker Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow(SafeWalker11Token, SafeWalker11Email, "false")]
        [DataRow(SafeWalker12Token, SafeWalker12Email, "false")]
        [DataRow(SafeWalker13Token, SafeWalker13Email, "false")]
        public void GetSafewalker_Ok(string token, string email, string isUser)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Safewalkers/{email}");

            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);
            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow("dummytoken", SafeWalker11Email, "false")]
        [DataRow(SafeWalker12Token, "shimura@wisc.edu", "false")]
        [DataRow("dummytoken", "dummyemail@wisc.edu", "false")]
        public void GetSafewalker_Unauthorized(string token, string email, string isUser)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Safewalkers/{email}");

            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);
            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // PutSafewalker Tests ----------------------------------------------------------------------
        [TestMethod]
        [DataRow(SafeWalker14Token, "fakedeuman@wisc.edu", "{\"LastName\":\"Deuman\", \"FirstName\":\"Freakin\", \"Password\":\"crap11\", \"PhoneNumber\":\"4567890\", \"HomeAddress\":\"lakeshore\"}")]
        [DataRow("dummytoken", SafeWalker14Email, "{\"LastName\":\"Cho\", \"FirstName\":\"Yoon\", \"Password\":\"cat11\", \"PhoneNumber\":\"8342532244\", \"HomeAddress\":\"9384 street\"}")]
        public void PutSafewalker_Unauthorized(string token, string email, string user)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/Safewalkers/{email}");

            request.Headers.Add("token", token);
            request.Content = new StringContent(user, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        [DataRow(SafeWalker15Token, SafeWalker15Email, "{\"LastName\":\"Shimura\", \"FirstName\":\"Tadao\", \"Password\":\"shimura11\", \"PhoneNumber\":\"9824323438\"}")]
        public void PutSafewalker_Ok(string token, string email, string user)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/Safewalkers/{email}");

            request.Headers.Add("token", token);
            request.Content = new StringContent(user, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

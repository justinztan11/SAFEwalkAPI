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

        // GetSafewalker Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA", "shimura@wisc.edu", "false")]
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
        [DataRow("dummytoken", "shimura@wisc.edu", "false")]
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
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE", "fakedeuman@wisc.edu", "{\"LastName\":\"Deuman\", \"FirstName\":\"Freakin\", \"Password\":\"crap11\", \"PhoneNumber\":\"4567890\", \"HomeAddress\":\"lakeshore\"}")]
        [DataRow("dummytoken", "ycho@wisc.edu", "{\"LastName\":\"Cho\", \"FirstName\":\"Yoon\", \"Password\":\"cat11\", \"PhoneNumber\":\"8342532244\", \"HomeAddress\":\"9384 street\"}")]
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
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA", "shimura@wisc.edu", "{\"LastName\":\"Shimura\", \"FirstName\":\"Tadao\", \"Password\":\"shimura11\", \"PhoneNumber\":\"9824323438\"}")]
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

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;

namespace SafewalkApplication.Tests
{
    [TestClass]
    public class LoginLogoutTests
    {
        private readonly HttpClient _client;

        // Dummies
        const string dummyEmail = "dummy@wisc.edu";
        const string dummyPassword = "dummy11";
        const string dummyToken = "000000000000000000000000000";

        public LoginLogoutTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        // Login Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow("usertest04@wisc.edu", "test11", "true")]
        [DataRow("usertest05@wisc.edu", "test11", "true")]
        [DataRow("walkertest04@wisc.edu", "test11", "false")]
        [DataRow("walkertest05@wisc.edu", "test11", "false")]
        public void GetLogin_Ok(string email, string password, string isUser)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Login");
            request.Headers.Add("email", email);
            request.Headers.Add("password", password);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow("usertest04@wisc.edu", "test11", "false")]
        [DataRow("usertest05@wisc.edu", dummyPassword, "true")]
        [DataRow(dummyEmail, "test11", "false")]
        public void GetLogin_NotFound(string email, string password, string isUser)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Login");
            request.Headers.Add("email", email);
            request.Headers.Add("password", password);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Logout Tests ----------------------------------------------------------------------

        //[TestMethod]
        //[DataRow("usertest04@wisc.edu", "true", user04Token)]
        //[DataRow("usertest05@wisc.edu", "true", user05Token)]
        //[DataRow("walkertest04@wisc.edu", "false", walker04Token)]
        //[DataRow("walkertest05@wisc.edu", "false", walker05Token)]
        //public void PutLogout_Ok(string email, string isUser, string token)
        //{
        //    //Arrange
        //    var request = new HttpRequestMessage(new HttpMethod("PUT"), "/api/Logout");
        //    request.Headers.Add("email", email);
        //    request.Headers.Add("isUser", isUser);
        //    request.Headers.Add("token", token);

        //    //Act
        //    var response = _client.SendAsync(request).Result;

        //    //Assert
        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //}

        [TestMethod]
        [DataRow("usertest04@wisc.edu", "false", dummyToken)]
        [DataRow("walkertest05@wisc.edu", "false", dummyToken)]
        public void PutLogout_Unauthorized(string email, string isUser, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), "/api/Logout");
            request.Headers.Add("email", email);
            request.Headers.Add("isUser", isUser);
            request.Headers.Add("token", token);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

    }
}

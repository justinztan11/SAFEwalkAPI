using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SafewalkApplication.Tests
{
    [TestClass]
    public class WalkTests
    {
        private readonly HttpClient _client;

        // User tokens
        const string user01Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ijg1YTk3OGFmLTJhODQtNDdlZi05ZTE1LThjNzIxMWUzNmM0YSIsIm5iZiI6MTU4Njc1NDE3MCwiZXhwIjoxNTg2ODQwNTcwLCJpYXQiOjE1ODY3NTQxNzB9.BpDnH79nSbcKL9V9Bjp12cCPU9OVqjF1yMAl3M0rsqc";
        const string user02Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImU3YjgxYWU2LThmMzMtNDM3Ny1hNjM5LWE2MTIxMWI5MjdiYSIsIm5iZiI6MTU4Njc1MjgyMiwiZXhwIjoxNTg2ODM5MjIyLCJpYXQiOjE1ODY3NTI4MjJ9.MqPfDOla1olu3M85O92VuTe4dCj84dkk-eRw3TnVPJE";
        const string user03Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjU0NjJmOGJlLTcwNDUtNDQ5Yy04NWY1LWYwYmNhMmE0YzJjMSIsIm5iZiI6MTU4Njc1Mjk0OSwiZXhwIjoxNTg2ODM5MzQ5LCJpYXQiOjE1ODY3NTI5NDl9.lHyP3sUMg7FUYBsabubS3gF6LEZKypQTIo7hakLNsUI";

        // Safewalker tokens
        const string walker01Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ3MjJkNWRiLTViMDEtNDdjNS1iYjczLTA1NDk4MDEzZjY4MCIsIm5iZiI6MTU4Njc1MjcwMSwiZXhwIjoxNTg2ODM5MTAxLCJpYXQiOjE1ODY3NTI3MDF9._eqgfis_F8c7NwMCN7haT4FgmKwJK2m8YlKM8ayHIb8";
        const string walker02Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFiYTIwM2ExLTliMzMtNDI4ZS1hMmE5LTczN2U5MDFjMmViMCIsIm5iZiI6MTU4Njc1MjczNCwiZXhwIjoxNTg2ODM5MTM0LCJpYXQiOjE1ODY3NTI3MzR9.EAK5UhAjSKc_fKBOJa6xFGylz3kDzbNX_C-3ZtGkQDk";
        const string walker03Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkMDVmZDlmLTViZmItNDA0OC05NmU4LWVjOTQ4MzA0YWQxNyIsIm5iZiI6MTU4Njc1Mjc1MCwiZXhwIjoxNTg2ODM5MTUwLCJpYXQiOjE1ODY3NTI3NTB9.ipDSAyarMFc37S7keaksoHt-ZLISlhnLqCUlYKVBXYM";

        // Walk Ids
        const string walk01Id = "77d22aaf-952e-43c1-8192-6669404010e6";
        const string walk02Id = "269b3873-1f11-4280-933d-f27646d6ffef";

        // Dummies (unauthorized)
        const string dummyToken = "000000000000000000000000000";
        const string dummyEmail = "dummy@wisc.edu";
        const string dummyWalkId = "00000000000000000000000000";

        public WalkTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        // GetWalks Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow("walkertest01@wisc.edu", walker01Token)]
        [DataRow("walkertest02@wisc.edu", walker02Token)]
        [DataRow("walkertest03@wisc.edu", walker03Token)]
        public void GetWalks_Ok(string email, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Walks");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow("walkertest01@wisc.edu", dummyToken)]
        [DataRow("walkertest02@wisc.edu", dummyToken)]
        [DataRow(dummyEmail, walker03Token)]
        [DataRow("usertest01@wisc.edu", user01Token)]
        public void GetWalks_Unauthorized(string email, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Walks");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // GetWalk Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow("walkertest01@wisc.edu", "false", walk01Id, walker01Token)]
        [DataRow("usertest01@wisc.edu", "true", walk01Id, user01Token)]
        [DataRow("walkertest02@wisc.edu", "false", walk02Id, walker02Token)]
        [DataRow("usertest02@wisc.edu", "true", walk02Id, user02Token)]
        public void GetWalk_Ok(string email, string isUser, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Walks/{id}");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow("walkertest01@wisc.edu", "true", walk01Id, walker01Token)]
        [DataRow(dummyEmail, "true", walk01Id, user01Token)]
        [DataRow("walkertest02@wisc.edu", "false", walk02Id, dummyToken)]
        public void GetWalk_Unauthorized(string email, string isUser, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Walks/{id}");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        [DataRow("walkertest01@wisc.edu", "false", dummyWalkId, walker01Token)]
        [DataRow("usertest01@wisc.edu", "true", dummyWalkId, user01Token)]
        public void GetWalk_NotFound(string email, string isUser, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Walks/{id}");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        // GetWalkStatus Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow("walkertest01@wisc.edu", "false", walk01Id, walker01Token)]
        [DataRow("usertest01@wisc.edu", "true", walk01Id, user01Token)]
        [DataRow("walkertest02@wisc.edu", "false", walk02Id, walker02Token)]
        [DataRow("usertest02@wisc.edu", "true", walk02Id, user02Token)]
        public void GetWalkStatus_Ok(string email, string isUser, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Walks/{id}/status");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow("walkertest01@wisc.edu", "true", walk01Id, walker01Token)]
        [DataRow("usertest01@wisc.edu", "true", walk01Id, dummyToken)]
        [DataRow("walkertest02@wisc.edu", "true", walk02Id, walker02Token)]
        [DataRow(dummyEmail, "true", walk02Id, user02Token)]
        public void GetWalkStatus_Unauthorized(string email, string isUser, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Walks/{id}/status");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        [DataRow("walkertest01@wisc.edu", "false", dummyWalkId, walker01Token)]
        [DataRow("usertest01@wisc.edu", "true", dummyWalkId, user01Token)]
        public void GetWalkStatus_NotFound(string email, string isUser, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Walks/{id}/status");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        // PostWalk Tests ----------------------------------------------------------------------

        //[TestMethod]
        //[DataRow("usertest03@wisc.edu", "{\"Time\":\"2020-03-10T06:27:40\", \"StartText\":\"home\", " +
        //    "\"StartLat\": 123.022, \"StartLng\": 243.7654, \"DestText\":\"college\", " +
        //    "\"DestLat\": 235.345, \"DestLng\": 765.732234}", user03Token)]
        //public void PostWalk_Ok(string email, string walk, string token)
        //{
        //    //Arrange
        //    var request = new HttpRequestMessage(new HttpMethod("POST"), "/api/Walks");
        //    request.Headers.Add("email", email);
        //    request.Headers.Add("token", token);
        //    request.Content = new StringContent(walk, Encoding.UTF8, "application/json");

        //    //Act
        //    var response = _client.SendAsync(request).Result;

        //    //Assert
        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //}

        [TestMethod]
        [DataRow("usertest03@wisc.edu", "{\"Time\":\"2020-03-10T06:27:40\", \"StartText\":\"home\", " +
            "\"StartLat\": 123.022, \"StartLng\": 243.7654, \"DestText\":\"college\", " +
            "\"DestLat\": 235.345, \"DestLng\": 765.732234}", dummyToken)]
        [DataRow("usertest02@wisc.edu", "{\"Time\":\"2020-03-10T06:27:40\", \"StartText\":\"home\", " +
            "\"StartLat\": 123.022, \"StartLng\": 243.7654, \"DestText\":\"college\", " +
            "\"DestLat\": 235.345, \"DestLng\": 765.732234}", user03Token)]
        public void PostWalk_Unauthorized(string email, string walk, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), "/api/Walks");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Content = new StringContent(walk, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        [DataRow("usertest01@wisc.edu", "{\"Time\":\"2020-03-10T06:27:40\", \"StartText\":\"home\", " +
            "\"StartLat\": 123.022, \"StartLng\": 243.7654, \"DestText\":\"college\", " +
            "\"DestLat\": 235.345, \"DestLng\": 765.732234}", user01Token)]
        [DataRow("usertest02@wisc.edu", "{\"Time\":\"2020-03-10T06:27:40\", \"StartText\":\"home\", " +
            "\"StartLat\": 123.022, \"StartLng\": 243.7654, \"DestText\":\"college\", " +
            "\"DestLat\": 235.345, \"DestLng\": 765.732234}", user02Token)]
        public void PostWalk_Conflict(string email, string walk, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), "/api/Walks");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Content = new StringContent(walk, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }

        // PutWalk Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow("usertest01@wisc.edu", "true", "{\"Status\": 2}", walk01Id, user01Token)]
        [DataRow("usertest01@wisc.edu", "true", "{\"Status\": 1}", walk01Id, user01Token)]
        [DataRow("usertest02@wisc.edu", "true", "{\"Status\": 2}", walk02Id, user02Token)]
        [DataRow("usertest02@wisc.edu", "true", "{\"Status\": 1}", walk02Id, user02Token)]
        [DataRow("walkertest02@wisc.edu", "false", "{\"Status\": -2}", walk02Id, walker02Token)]
        [DataRow("walkertest02@wisc.edu", "false", "{\"Status\": 1}", walk02Id, walker02Token)]
        public void PutWalk_Ok(string email, string isUser, string walk, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/Walks/{id}");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);
            request.Content = new StringContent(walk, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow(dummyEmail, "true", "{\"Status\": 2}", walk01Id, user01Token)]
        [DataRow("usertest02@wisc.edu", "false", "{\"Status\": 2}", walk02Id, user02Token)]
        [DataRow("walkertest02@wisc.edu", "false", "{\"Status\": 1}", walk02Id, dummyToken)]
        public void PutWalk_Unauthorized(string email, string isUser, string walk, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/Walks/{id}");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);
            request.Content = new StringContent(walk, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        [DataRow("usertest02@wisc.edu", "true", "{\"Status\": 2}", dummyWalkId, user02Token)]
        [DataRow("walkertest02@wisc.edu", "false", "{\"Status\": -2}", dummyWalkId, walker02Token)]
        public void PutWalk_NotFound(string email, string isUser, string walk, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/Walks/{id}");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);
            request.Content = new StringContent(walk, Encoding.UTF8, "application/json");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}

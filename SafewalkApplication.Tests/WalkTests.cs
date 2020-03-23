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

        public WalkTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        // GetWalks Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow("shimura@wisc.edu", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
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
        [DataRow("shimura@wisc.edu", "34567")]
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
        [DataRow("shimura@wisc.edu", "false", "v8hf93483r293r98", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
        [DataRow("jztan2@wisc.edu", "true", "v8hf93483r293r98", "34y3g6h65")]
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
        [DataRow("shimura@wisc.edu", "true", "v8hf93483r293r98", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
        [DataRow("jztan2@wisc.edu", "false", "v8hf93483r293r98", "34y3g6h65")]
        [DataRow("tan@wisc.edu", "true", "v8hf93483r293r98", "34y3g6h65")]
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
        [DataRow("shimura@wisc.edu", "false", "iusdf87w", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
        [DataRow("jztan2@wisc.edu", "true", "c737874f93483r293r98", "34y3g6h65")]
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
        [DataRow("shimura@wisc.edu", "false", "v8hf93483r293r98", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
        [DataRow("jztan2@wisc.edu", "true", "v8hf93483r293r98", "34y3g6h65")]
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
        [DataRow("shimura@wisc.edu", "true", "v8hf93483r293r98", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
        [DataRow("jztan2@wisc.edu", "false", "v8hf93483r293r98", "34y3g6h65")]
        [DataRow("tan@wisc.edu", "true", "v8hf93483r293r98", "34y3g6h65")]
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
        [DataRow("shimura@wisc.edu", "false", "iusdf87w", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
        [DataRow("jztan2@wisc.edu", "true", "c737874f93483r293r98", "34y3g6h65")]
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

        // GetWalkLocation Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow("shimura@wisc.edu", "false", "v8hf93483r293r98", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
        [DataRow("jztan2@wisc.edu", "true", "v8hf93483r293r98", "34y3g6h65")]
        public void GetWalkLocation_Ok(string email, string isUser, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Walks/{id}/location");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow("shimura@wisc.edu", "true", "v8hf93483r293r98", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
        [DataRow("jztan2@wisc.edu", "false", "v8hf93483r293r98", "34y3g6h65")]
        [DataRow("tan@wisc.edu", "true", "v8hf93483r293r98", "34y3g6h65")]
        public void GetWalkLocation_Unauthorized(string email, string isUser, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Walks/{id}/location");
            request.Headers.Add("email", email);
            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        [DataRow("shimura@wisc.edu", "false", "iusdf87w", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
        [DataRow("jztan2@wisc.edu", "true", "c737874f93483r293r98", "34y3g6h65")]
        public void GetWalkLocation_NotFound(string email, string isUser, string id, string token)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Walks/{id}/location");
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
        ////[DataRow("ycho@wisc.edu", "{\"Time\":\"2020-03-10T06:27:40\", \"StartText\":\"home\", \"StartLat\": 123.022, \"StartLng\": 243.7654, \"DestText\":\"college\", \"DestLat\": 235.345, \"DestLng\": 765.732234}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjcyaDBmNzg0ZzhoZiIsIm5iZiI6MTU4MzU0NDk5MiwiZXhwIjoxNTgzNjMxMzkyLCJpYXQiOjE1ODM1NDQ5OTJ9.58tRXuJDWU7g24YMQnFZEwPxDB18rRcug7x2yKQV1B8")]
        ////[DataRow("deuman@wisc.edu", "{\"StartText\":\"brandy street\",\"DestText\":\"memorial library\"}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE")]
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
        [DataRow("ycho@wisc.edu", "{\"Time\":\"2020-03-10T06:27:40\", \"StartText\":\"home\", \"StartLat\": 123.022, \"StartLng\": 243.7654, \"DestText\":\"college\", \"DestLat\": 235.345, \"DestLng\": 765.732234}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjcyaDBmNzg0ZzhoZiIsIm5iZiI6MTU4MzU0NDk5MiwiZXhwIjoxNTgzNjMxMzkyLCJpYXQV1B8")]
        [DataRow("deu@wisc.edu", "{\"StartText\":\"brandy street\",\"DestText\":\"memorial library\"}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE")]
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
        [DataRow("ycho@wisc.edu", "{\"Time\":\"2020-03-10T06:27:40\", \"StartText\":\"home\", \"StartLat\": 123.022, \"StartLng\": 243.7654, \"DestText\":\"college\", \"DestLat\": 235.345, \"DestLng\": 765.732234}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjcyaDBmNzg0ZzhoZiIsIm5iZiI6MTU4NDk5MzEzNCwiZXhwIjoxNTg1MDc5NTM0LCJpYXQiOjE1ODQ5OTMxMzR9.waK8Eag-1rVbyDny5t_06qT-eG6Ham5n-hTHJ6ztQ6E")]
        [DataRow("deuman@wisc.edu", "{\"StartText\":\"brandy street\",\"DestText\":\"memorial library\"}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE")]
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
        [DataRow("ycho@wisc.edu", "true", "{\"Status\": 2}", "9955520d-b3f3-4e28-8a39-a9afabf1f1ff", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjcyaDBmNzg0ZzhoZiIsIm5iZiI6MTU4NDk5MzEzNCwiZXhwIjoxNTg1MDc5NTM0LCJpYXQiOjE1ODQ5OTMxMzR9.waK8Eag-1rVbyDny5t_06qT-eG6Ham5n-hTHJ6ztQ6E")]
        [DataRow("deuman@wisc.edu", "true", "{\"Status\": -1}", "33b93e7f-a8d8-441a-84d2-3e74bbc07b95", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE")]
        [DataRow("shimura@wisc.edu", "false", "{\"Status\": 1}", "v8hf93483r293r98", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
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
        [DataRow("ycho@wisc.edu", "true", "{\"Status\": 2}", "33b93e7f-a8d8-441a-84d2-3e74bbc07b95", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjcyaDBmNzg0ZzhoZiIsIm5iZiI6MTU4NDk5MzEzNCwiZXhwIjoxNTg1MDc5NTM0LCJpYXQiOjE1ODQ5OTMxMzR9.waK8Eag-1rVbyDny5t_06qT-eG6Ham5n-hTHJ6ztQ6E")]
        [DataRow("deu@wisc.edu", "true", "{\"Status\": -1}", "33b93e7f-a8d8-441a-84d2-3e74bbc07b95", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE")]
        [DataRow("shimura@wisc.edu", "false", "{\"Status\": 1}", "v8hf93483r293r98", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6ME1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
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
        [DataRow("ycho@wisc.edu", "true", "{\"Status\": 2}", "9955520d-b3f3-4e28-8af1ff", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjcyaDBmNzg0ZzhoZiIsIm5iZiI6MTU4NDk5MzEzNCwiZXhwIjoxNTg1MDc5NTM0LCJpYXQiOjE1ODQ5OTMxMzR9.waK8Eag-1rVbyDny5t_06qT-eG6Ham5n-hTHJ6ztQ6E")]
        [DataRow("deuman@wisc.edu", "true", "{\"Status\": -1}", "33b93e7f-3e74bbc07b95", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE")]
        [DataRow("shimura@wisc.edu", "false", "{\"Status\": 1}", "v8h93r98", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMzQ1Njc4OTg3NjVoYyIsIm5iZiI6MTU4Mzc2NTE1MCwiZXhwIjoxNTgzODUxNTUwLCJpYXQiOjE1ODM3NjUxNTB9.lIqN2RuvbOK79Succ98r3DnlDa59MfahHddfNMyArsA")]
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

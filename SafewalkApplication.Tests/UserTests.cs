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

        // GetUser Tests ----------------------------------------------------------------------

        [TestMethod]
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE", "deuman@wisc.edu", "true")]
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjcyaDBmNzg0ZzhoZiIsIm5iZiI6MTU4NDk5MzEzNCwiZXhwIjoxNTg1MDc5NTM0LCJpYXQiOjE1ODQ5OTMxMzR9.waK8Eag-1rVbyDny5t_06qT-eG6Ham5n-hTHJ6ztQ6E", "ycho@wisc.edu", "true")]

        public void GetUser_Ok(string token, string email, string isUser)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Users/{email}");

            request.Headers.Add("token", token);
            request.Headers.Add("isUser", isUser);
            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        [DataRow("dummytoken", "deuman@wisc.edu", "true")]
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjcyaDBmNzg0ZzhoZiIsIm5iZiI6MTU4NDAzNDkwNywiZXhwIjoxNTg0MTIxMzA3LCJpYXQiOjE1ODQwMzQ5MDd9.RaH-9VoBIJjAGMlIOUPraIdvOJCXebh5nmFmVF0VNcs", "Fakeycho@wisc.edu", "true")]
        [DataRow("dummytoken", "shimura@wisc.edu", "true")]
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

        // PutUser Tests ----------------------------------------------------------------------
        [TestMethod]
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE", "fakedeuman@wisc.edu", "{\"LastName\":\"Deuman\", \"FirstName\":\"Freakin\", \"Password\":\"crap11\", \"PhoneNumber\":\"4567890\", \"HomeAddress\":\"lakeshore\"}")]
        [DataRow("dummytoken", "ycho@wisc.edu", "{\"LastName\":\"Cho\", \"FirstName\":\"Yoon\", \"Password\":\"cat11\", \"PhoneNumber\":\"8342532244\", \"HomeAddress\":\"9384 street\"}")]
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
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5MTg0MzkxLTczZDEtNGJjOC05NjQ3LTlkYmVmZjcxMTAyYiIsIm5iZiI6MTU4Mzc2Mzg1MiwiZXhwIjoxNTgzODUwMjUyLCJpYXQiOjE1ODM3NjM4NTJ9.ihcQU9HSMgs78yCaWs4RRV4Fkx1_Bsj2C2EZNi-9cjE", "deuman@wisc.edu", "{\"LastName\":\"Deuman\", \"FirstName\":\"Freakin\", \"Password\":\"crap11\", \"PhoneNumber\":\"4567890\", \"HomeAddress\":\"lakeshore\"}")]
        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjcyaDBmNzg0ZzhoZiIsIm5iZiI6MTU4NDk5MzEzNCwiZXhwIjoxNTg1MDc5NTM0LCJpYXQiOjE1ODQ5OTMxMzR9.waK8Eag-1rVbyDny5t_06qT-eG6Ham5n-hTHJ6ztQ6E", "ycho@wisc.edu", "{\"LastName\":\"Cho\", \"FirstName\":\"Yoon\", \"Password\":\"cat11\", \"PhoneNumber\":\"8342532244\", \"HomeAddress\":\"9384 street\"}")]
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

/*        // PostUser Tests ----------------------------------------------------------------------


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
        }*/
    }
}

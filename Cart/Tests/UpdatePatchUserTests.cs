using System;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using CartAPI_Testing.Models;

namespace CartAPI_Testing.Tests
{
    public class UpdatePatchUserTests : BaseTest
    {
        [Test]
        public void PATCH_User_Should_Update_Username()
        {
            var client = new RestClient("https://fakestoreapi.com");
            var request = new RestRequest("/users/1", Method.Patch);

            request.AddJsonBody(new
            {
                username = "patcheduser"
            });

            var response = client.Execute(request);

            Console.WriteLine("PATCH Status: " + (int)response.StatusCode);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));

            var user = JsonConvert.DeserializeObject<User>(response.Content);
            Assert.That(user.username, Is.EqualTo("patcheduser"));
        }
    }
}

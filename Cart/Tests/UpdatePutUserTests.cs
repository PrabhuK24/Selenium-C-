using System;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using CartAPI_Testing.Models;

namespace CartAPI_Testing.Tests
{
    public class UpdatePutUserTests : BaseTest
    {
        [Test]
        public void PUT_User_Should_Update()
        {
            var client = new RestClient("https://fakestoreapi.com");
            var request = new RestRequest("/users/1", Method.Put);

            request.AddJsonBody(new
            {
                email = "updated@gmail.com",
                username = "updateduser",
                password = "newpass",
                name = new { firstname = "Updated", lastname = "User" },
                address = new { city = "LA", street = "New Street", number = 20, zipcode = "90001" },
                phone = "9876543210"
            });

            var response = client.Execute(request);

            Console.WriteLine("PUT Status: " + (int)response.StatusCode);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));

            var user = JsonConvert.DeserializeObject<User>(response.Content);
            Assert.That(user.username, Is.EqualTo("updateduser"));
        }
    }
}

using System;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using CartAPI_Testing.Models;

namespace CartAPI_Testing.Tests
{
    public class CreateUserTests : BaseTest
    {
        private const string BaseUrl = "https://fakestoreapi.com";

        [Test]
        public void POST_User_Should_Create()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/users", Method.Post);

            request.AddJsonBody(new
            {
                email = "test@example.com",
                username = "testuser",
                password = "testpass",
                name = new { firstname = "John", lastname = "Doe" },
                address = new
                {
                    city = "Delhi",
                    street = "Main Road",
                    number = 10,
                    zipcode = "12345",
                    geolocation = new { lat = "40.0", longi = "20.0" }
                },
                phone = "9999999999"
            });

            var response = client.Execute(request);

            Console.WriteLine("POST Status: " + (int)response.StatusCode);
            Console.WriteLine("Response: " + response.Content);

            Assert.That((int)response.StatusCode, Is.EqualTo(201));

            var result = JsonConvert.DeserializeObject<CreateUserResponse>(response.Content);

            Assert.That(result.id, Is.GreaterThan(0));
        }
    }

    public class CreateUserResponse
    {
        public int id { get; set; }
    }
}

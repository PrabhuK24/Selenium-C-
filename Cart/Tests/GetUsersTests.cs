using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using CartAPI_Testing.Models;

namespace CartAPI_Testing.Tests
{
    public class GetUsersTests : BaseTest
    {
        [Test]
        public void GET_All_Users_Should_Return_List()
        {
            var client = new RestClient("https://fakestoreapi.com");
            var request = new RestRequest("/users", Method.Get);

            var response = client.Execute(request);

            Console.WriteLine("GET Status: " + (int)response.StatusCode);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));

            var users = JsonConvert.DeserializeObject<List<User>>(response.Content);
            Assert.That(users, Is.Not.Null.And.Not.Empty);

            Console.WriteLine($"Total users: {users.Count}");
        }
    }
}

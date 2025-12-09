using System;
using NUnit.Framework;
using RestSharp;

namespace CartAPI_Testing.Tests
{
    public class DeleteUserTests : BaseTest
    {
        [Test]
        public void DELETE_User_Should_Return_200()
        {
            var client = new RestClient("https://fakestoreapi.com");
            var request = new RestRequest("/users/1", Method.Delete);

            var response = client.Execute(request);

            Console.WriteLine("DELETE Status: " + (int)response.StatusCode);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
        }
    }
}

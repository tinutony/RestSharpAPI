
using System;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using API_Testing.Models;

namespace API_Testing.Tests
{
    public class CreateProductTests
    {
        private const string BaseUrl = "https://fakestoreapi.com";

        [Test]
        public void POST_Product_Should_Create_And_Echo()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/products", Method.Post);

            request.AddJsonBody(new
            {
                title = "API Automation Product",
                price = 29.99m,
                description = "Created via NUnit + RestSharp",
                image = "https://i.pravatar.cc",
                category = "electronics"
            });

            var response = client.Execute(request);
            Console.WriteLine("POST Status: " + (int)response.StatusCode);
            Console.WriteLine("Response: " + response.Content);

            Assert.That(new[] { 200, 201 }, Does.Contain((int)response.StatusCode), "POST status mismatch!");

            var product = JsonConvert.DeserializeObject<Product>(response.Content);
            Assert.That(product.title, Is.EqualTo("API Automation Product"));
        }
    }
}

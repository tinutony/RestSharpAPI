
using System;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using API_Testing.Models;

namespace API_Testing.Tests
{
    public class UpdatePutProductTests
    {
        [Test]
        public void PUT_Product_Should_Update_All_Fields()
        {
            var client = new RestClient("https://fakestoreapi.com");
            var request = new RestRequest("/products/1", Method.Put);

            request.AddJsonBody(new
            {
                title = "Updated Title",
                price = 49.5m,
                description = "Updated via PUT",
                image = "https://i.pravatar.cc",
                category = "jewelery"
            });

            var response = client.Execute(request);
            Console.WriteLine("PUT Status: " + (int)response.StatusCode);
            Console.WriteLine("Response: " + response.Content);

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "PUT status mismatch!");

            var product = JsonConvert.DeserializeObject<Product>(response.Content);
            Assert.That(product, Is.Not.Null);
            Assert.That(product.id, Is.EqualTo(1));
            Assert.That(product.title, Is.EqualTo("Updated Title"));
            Assert.That(product.description, Is.EqualTo("Updated via PUT"));
            Assert.That(product.category, Is.EqualTo("jewelery"));
        }
    }
}


using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using API_Testing.Models;

namespace API_Testing.Tests
{
    public class GetProductsTests
    {
        private const string BaseUrl = "https://fakestoreapi.com";

        [Test]
        public void GET_All_Products_Should_Return_List()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/products", Method.Get);

            var response = client.Execute(request);
            Console.WriteLine("GET Status: " + (int)response.StatusCode);

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "GET status mismatch!");

            var products = JsonConvert.DeserializeObject<List<Product>>(response.Content);
            Assert.That(products, Is.Not.Null.And.Not.Empty, "Products list is empty!");
            Console.WriteLine($"Total products: {products.Count}");
        }
    }
}

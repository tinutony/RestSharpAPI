
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using AventStack.ExtentReports;

namespace AmazonAutomation.Tests
{
    public class GetProductsTests : TestBase
    {
        private const string BaseUrl = "https://fakestoreapi.com";

        [Test]
        public void GET_All_Products_Should_Return_List()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/products", Method.Get);

            Test.Info("GET /products");

            var response = client.Execute(request);
            Test.Info($"Status: {(int)response.StatusCode}");

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "GET status mismatch!");

            var products = JsonConvert.DeserializeObject<List<API_Testing.Models.Product>>(response.Content);
            Assert.That(products, Is.Not.Null.And.Not.Empty, "Products list is empty!");

            Test.Info($"Total products: {products.Count}");
            Test.Pass("Products fetched successfully");
        }
    }
}

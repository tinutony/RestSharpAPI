
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using AventStack.ExtentReports;

namespace AmazonAutomation.Tests
{
    public class UpdatePutProductTests : TestBase
    {
        [Test]
        public void PUT_Product_Should_Update_All_Fields()
        {
            var client = new RestClient("https://fakestoreapi.com");
            var request = new RestRequest("/products/1", Method.Put);

            var putBody = new
            {
                title = "Updated Title",
                price = 49.5m,
                description = "Updated via PUT",
                image = "https://i.pravatar.cc",
                category = "jewelery"
            };

            request.AddJsonBody(putBody);

            Test.Info("PUT /products/1");
            Test.Info("Request Body: " + JsonConvert.SerializeObject(putBody));

            var response = client.Execute(request);
            Test.Info($"Status: {(int)response.StatusCode}");
            Test.Info("Response: " + response.Content);

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "PUT status mismatch!");

            var product = JsonConvert.DeserializeObject<API_Testing.Models.Product>(response.Content);
            Assert.That(product, Is.Not.Null);
            Assert.That(product.id, Is.EqualTo(1));
            Assert.That(product.title, Is.EqualTo("Updated Title"));
            Assert.That(product.description, Is.EqualTo("Updated via PUT"));
            Assert.That(product.category, Is.EqualTo("jewelery"));

            Test.Pass("Product updated via PUT successfully");
        }
    }
}

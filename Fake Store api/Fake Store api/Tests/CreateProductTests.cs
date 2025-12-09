
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using AventStack.ExtentReports;

namespace AmazonAutomation.Tests
{
    public class CreateProductTests : TestBase
    {
        private const string BaseUrl = "https://fakestoreapi.com";

        [Test]
        public void POST_Product_Should_Create_And_Echo()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/products", Method.Post);

            var body = new
            {
                title = "API Automation Product",
                price = 29.99m,
                description = "Created via NUnit + RestSharp",
                image = "https://i.pravatar.cc",
                category = "electronics"
            };

            request.AddJsonBody(body);

            Test.Info("POST /products");
            Test.Info(JsonConvert.SerializeObject(body));

            var response = client.Execute(request);

            Test.Info(((int)response.StatusCode).ToString());
            Test.Info(response.Content);

            Assert.That(new[] { 200, 201 }, Does.Contain((int)response.StatusCode));
            var product = JsonConvert.DeserializeObject<API_Testing.Models.Product>(response.Content);
            Assert.That(product.title, Is.EqualTo("API Automation Product"));

            Test.Pass("OK");
        }
    }
}

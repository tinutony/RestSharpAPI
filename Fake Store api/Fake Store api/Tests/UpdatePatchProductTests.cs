
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using AventStack.ExtentReports;

namespace AmazonAutomation.Tests
{
    public class UpdatePatchProductTests : TestBase
    {
        [Test]
        public void PATCH_Product_Should_Update_Title()
        {
            var client = new RestClient("https://fakestoreapi.com");
            var request = new RestRequest("/products/1", Method.Patch);

            var patchBody = new { title = "Patched Title" };
            request.AddJsonBody(patchBody);

            Test.Info("PATCH /products/1");
            Test.Info("Request Body: " + JsonConvert.SerializeObject(patchBody));

            var response = client.Execute(request);
            Test.Info($"Status: {(int)response.StatusCode}");
            Test.Info("Response: " + response.Content);

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "PATCH status mismatch!");

            var product = JsonConvert.DeserializeObject<API_Testing.Models.Product>(response.Content);
            Assert.That(product, Is.Not.Null, "Deserialization returned null");
            Assert.That(product.id, Is.EqualTo(1), "Id mismatch");
            Assert.That(product.title, Is.EqualTo("Patched Title"));

            Test.Pass("Product patched successfully");
        }
    }
}

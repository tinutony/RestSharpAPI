
using System;
using NUnit.Framework;               
using RestSharp;
using Newtonsoft.Json;
using API_Testing.Models;

namespace API_Testing.Tests
{
    public class UpdatePatchProductTests
    {
        [Test]
        public void PATCH_Product_Should_Update_Title()
        {
            var client = new RestClient("https://fakestoreapi.com");
            var request = new RestRequest("/products/1", Method.Patch);

            request.AddJsonBody(new { title = "Patched Title" });

            var response = client.Execute(request);
            Console.WriteLine("PATCH Status: " + (int)response.StatusCode);
            Console.WriteLine("Response: " + response.Content);

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "PATCH status mismatch!");

            var product = JsonConvert.DeserializeObject<Product>(response.Content); // <-- real < >, not &lt; &gt;
            Assert.That(product, Is.Not.Null, "Deserialization returned null");
            Assert.That(product.id, Is.EqualTo(1), "Id mismatch");
            Assert.That(product.title, Is.EqualTo("Patched Title"));
        }
    }
}


using System;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using API_Testing.Models;

namespace API_Testing.Tests
{
    public class UpdatePatchTests
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";

        [Test]
        public void PATCH_Posts_Id_Should_Update_Partial_Fields()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/posts/1", Method.Patch);

            request.AddJsonBody(new
            {
                title = "Patched Title"
            });

            var response = client.Execute(request);
            Console.WriteLine("PATCH Status code: " + (int)response.StatusCode);
            Console.WriteLine("PATCH Raw response: " + response.Content);

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "PATCH status code mismatch!");

            var post = JsonConvert.DeserializeObject<Post>(response.Content);
            Assert.That(post, Is.Not.Null, "PATCH: deserialization returned null.");

            Console.WriteLine("PATCH Title: " + post.title);
            Assert.That(post.id, Is.EqualTo(1), "PATCH id mismatch!");
            Assert.That(post.title, Is.EqualTo("Patched Title"), "PATCH title mismatch!");
        }
    }
}

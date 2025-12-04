
using System;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using API_Testing.Models;

namespace API_Testing.Tests
{
    public class CreatePostTests
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";

        [Test]
        public void POST_Posts_Should_Create_And_Echo_Title()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/posts", Method.Post);

            request.AddJsonBody(new
            {
                title = "API Automation",
                body = "RestSharp Post Test",
                userId = 1
            });

            var response = client.Execute(request);
            Console.WriteLine("POST Status code: " + (int)response.StatusCode);
            Console.WriteLine("POST Raw response: " + response.Content);

            Assert.That((int)response.StatusCode, Is.EqualTo(201), "POST status code mismatch!");

            var post = JsonConvert.DeserializeObject<Post>(response.Content);
            Assert.That(post, Is.Not.Null, "POST: deserialization returned null.");

            Console.WriteLine("POST Title: " + post.title);
            Assert.That(post.title, Is.EqualTo("API Automation"), "POST title mismatch!");
            Assert.That(post.body, Is.EqualTo("RestSharp Post Test"), "POST body mismatch!");
            Assert.That(post.userId, Is.EqualTo(1), "POST userId mismatch!");
            Assert.That(post.id, Is.GreaterThan(0), "POST id not set.");
        }
    }
}

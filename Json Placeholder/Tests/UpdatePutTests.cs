
using System;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using API_Testing.Models;

namespace API_Testing.Tests
{
    public class UpdatePutTests
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";

        [Test]
        public void PUT_Posts_Id_Should_Update_All_Fields()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/posts/1", Method.Put);

            request.AddJsonBody(new
            {
                id = 1,
                title = "Updated Title",
                body = "RestSharp Put Test",
                userId = 1
            });

            var response = client.Execute(request);
            Console.WriteLine("PUT Status code: " + (int)response.StatusCode);
            Console.WriteLine("PUT Raw response: " + response.Content);

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "PUT status code mismatch!");

            var post = JsonConvert.DeserializeObject<Post>(response.Content);
            Assert.That(post, Is.Not.Null, "PUT: deserialization returned null.");

            Console.WriteLine("PUT Title: " + post.title);
            Assert.That(post.id, Is.EqualTo(1), "PUT id mismatch!");
            Assert.That(post.title, Is.EqualTo("Updated Title"), "PUT title mismatch!");
            Assert.That(post.body, Is.EqualTo("RestSharp Put Test"), "PUT body mismatch!");
            Assert.That(post.userId, Is.EqualTo(1), "PUT userId mismatch!");
        }
    }
}

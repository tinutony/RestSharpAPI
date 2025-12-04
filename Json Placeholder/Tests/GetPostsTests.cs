
using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using API_Testing.Models;

namespace API_Testing.Tests
{
    public class GetPostsTests
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";

        [Test]
        public void GET_Posts_Should_Return_List_And_Check_First()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/posts", Method.Get);

            var response = client.Execute(request);
            Console.WriteLine("GET Status code: " + (int)response.StatusCode);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "GET status code mismatch!");

            var posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);
            Assert.That(posts, Is.Not.Null.And.Not.Empty, "GET: posts list is null or empty.");

            Console.WriteLine("GET First Post Title: " + posts[0].title);
            Assert.That(
                posts[0].title,
                Is.EqualTo("sunt aut facere repellat provident occaecati excepturi optio reprehenderit"),
                "GET first title mismatch!"
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tier2_BusinessAPI.Models;

namespace Tier2_BusinessAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task AddUserAsync(User user)
        {
            using HttpClient client = new HttpClient();

            string UserAsJson = JsonSerializer.Serialize(user, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            HttpContent content = new StringContent(UserAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage =
                await client.PostAsync("http://localhost:8080/user", content);
        }

        [HttpGet]
        public async Task<List<User>> GetUsersAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:8080/users");

            if (!message.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {message.StatusCode}");
            }

            string result = await message.Content.ReadAsStringAsync();

            List<User> users = JsonSerializer.Deserialize<List<User>>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return users;
        }
    }
}
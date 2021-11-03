using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Tier2_BusinessAPI.Controllers;
using Tier2_BusinessAPI.Models;

namespace Tier2_BusinessAPI.Hubs
{
    public class UserHub : Hub
    {
        private readonly UserController _controller = new UserController();
        public async Task AddNewUserAsync(string username)
        {
            User newUser = new User() {Username = username};

            await _controller.AddUserAsync(newUser);
        }

        public async Task<string> GetUsersAsync()
        {
            string usersAsJson = JsonSerializer.Serialize(await _controller.GetUsersAsync());
            return usersAsJson;
        }
    }
}
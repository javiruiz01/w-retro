// <copyright file="SignalHub.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace wRetroApi.Services
{
    public class SignalHub : Hub
    {
        public async Task AddToGroup(string session)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, session);
            await Clients.Group(session).SendAsync("Connected", $"{Context.ConnectionId} has joined the group {session}");
        }

        public async Task SendMessage(string group, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", message);
        }
    }
}
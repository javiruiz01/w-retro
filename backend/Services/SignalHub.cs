// <copyright file="SignalHub.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using wRetroApi.Models;

namespace wRetroApi.Services
{
    public class SignalHub : Hub
    {
        private readonly ISessionService _sessionService;

        public SignalHub(ISessionService sessionService)
        {
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
        }

        public async Task AddToGroup(Guid rawSession)
        {
            var session = rawSession.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, session);
            await Clients.Group(session).SendAsync("Connected", $"{Context.ConnectionId} has joined the group {session}");
        }

        public async Task UpdateSession(Guid rawSession)
        {
            var state = await _sessionService.GetSession(rawSession);
            await Clients.Group(rawSession.ToString()).SendAsync("Update", state);
        }
    }
}
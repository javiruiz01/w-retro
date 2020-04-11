// <copyright file="ISessionService.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using wRetroApi.Models;

namespace wRetroApi.Services
{
    public interface ISessionService
    {
        Task<Session> GetSession(Guid id);

        Task<Guid> CreateSession();

        Task UpdateTitle(Guid id, string title);
    }
}
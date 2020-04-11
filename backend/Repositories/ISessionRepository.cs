// <copyright file="ISessionRepository.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using wRetroApi.Models;

namespace wRetroApi.Repositories
{
    public interface ISessionRepository
    {
        Task<Session> GetSession(Guid id);

        Task CreateSession(Guid id, string title);

        Task UpdateTitle(Guid id, string title);
    }
}
// <copyright file="ISessionRepository.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;

namespace wRetroApi.Repositories
{
    public interface ISessionRepository
    {
        Task CreateSession(Guid id);
    }
}
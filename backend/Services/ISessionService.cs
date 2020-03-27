// <copyright file="ICardService.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System.Collections.Generic;
using wRetroApi.Models;

namespace wRetroApi.Services
{
    public interface ISessionService
    {
        IEnumerable<Session> Get();
    }
}
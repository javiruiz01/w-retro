// <copyright file="ICardService.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System.Collections.Generic;
using wRetroApi.Models;

namespace wRetroApi.Services
{
    public interface ISessionService
    {
        IEnumerable<Card> Get(string id);

        void AddComment(string id, int idx, string comment);
    }
}
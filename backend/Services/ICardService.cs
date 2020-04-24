// <copyright file="ICardService.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wRetroApi.Models;

namespace wRetroApi.Services
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> GetCards(Guid sessionId);

        Task CreateCard(Card card, Guid sessionId);

        Task DeleteCard(Guid id);

        public Task UpdateCard(Guid id, Card card);
    }
}
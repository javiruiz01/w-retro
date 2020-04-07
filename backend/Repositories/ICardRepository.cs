// <copyright file="ICardRepository.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wRetroApi.Models;

namespace wRetroApi.Repositories
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetCards(Guid sessionId);

        Task CreateCard(Card card, Guid sessionId);

        Task UpdateTitle(Guid id, string title);

        Task DeleteCard(Guid id);
    }
}
// <copyright file="SessionService.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Linq;
using System.Threading.Tasks;
using wRetroApi.Models;
using wRetroApi.Repositories;

namespace wRetroApi.Services
{
    public class SessionService : ISessionService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICommentRepository _commentRepository;

        public SessionService(ICardRepository cardRepository, ICommentRepository commentRepository)
        {
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        public async Task<Session> GetSession(Guid sessionId)
        {
            var cards = await _cardRepository.GetCards(sessionId);
            cards = await Task.WhenAll(cards.Select(async card =>
            {
                card.Comments = await _commentRepository.GetComments(card.Id);
                return card;
            }));

            return new Session {Id = sessionId, Cards = cards};
        }
    }
}
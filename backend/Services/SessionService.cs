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
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ICardRepository cardRepository, ICommentRepository commentRepository, ISessionRepository sessionRepository)
        {
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
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

        public async Task<Guid> CreateSession()
        {
            var cards = new[] {"Good", "Meh", "bad"}
                .Select((title, idx) => new Card
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Position = idx,
                    Comments = new[] {new Comment {Id = Guid.NewGuid(), Text = "Example Text", Likes = 0}}
                });

            var sessionId = Guid.NewGuid();
            await _sessionRepository.CreateSession(sessionId);
            foreach (Card card in cards)
            {
                await _cardRepository.CreateCard(card, sessionId);
            }

            return sessionId;
        }
    }
}
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
            var session = await _sessionRepository.GetSession(sessionId);
            if (session == null) return null;

            var cards = await _cardRepository.GetCards(sessionId);
            session.Cards = await Task.WhenAll(cards.Select(async card =>
            {
                card.Comments = await _commentRepository.GetComments(card.Id);
                return card;
            }));

            return session;
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

            var session = new Session {Id = Guid.NewGuid(), Title = "New title"};
            await _sessionRepository.CreateSession(session.Id, session.Title);
            foreach (Card card in cards)
            {
                await _cardRepository.CreateCard(card, session.Id);
            }

            return session.Id;
        }

        public async Task UpdateTitle(Guid id, string title) => await _sessionRepository.UpdateTitle(id, title);
    }
}
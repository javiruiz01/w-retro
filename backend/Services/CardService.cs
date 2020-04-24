// <copyright file="CardService.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wRetroApi.Models;
using wRetroApi.Repositories;

namespace wRetroApi.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICommentRepository _commentRepository;

        public CardService(ICardRepository cardRepository, ICommentRepository commentRepository)
        {
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        public async Task<IEnumerable<Card>> GetCards(Guid sessionId)
            => await _cardRepository.GetCards(sessionId);

        public async Task CreateCard(Card card, Guid sessionId)
            => await _cardRepository.CreateCard(card, sessionId);

        public async Task DeleteCard(Guid id)
            => await _cardRepository.DeleteCard(id);

        public async Task UpdateCard(Guid id, Card card)
        {
            await _cardRepository.UpdateCard(id, card);
            foreach (Comment cardComment in card.Comments)
            {
                await _commentRepository.UpdateComment(cardComment.Id, cardComment);
            }
        }
    }
}
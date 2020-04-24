// <copyright file="CardsController.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wRetroApi.Models;
using wRetroApi.Repositories;
using wRetroApi.Services;

namespace wRetroApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ICommentRepository _commentRepository;

        public CardsController(ICardService cardService, ICommentRepository commentRepository)
        {
            _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardDto cardInfo)
        {
            var card = new Card
            {
                Id = Guid.NewGuid(),
                Title = "Title here",
                Position = cardInfo.Position,
                Comments = new Comment[] { },
            };
            await _cardService.CreateCard(card, cardInfo.SessionId);
            return Ok(card);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            await _cardService.DeleteCard(id);
            return Ok();
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> CreateComment(Guid id, [FromBody] CreateCommentDto comment)
        {
            return Ok(await _commentRepository.CreateComment(id, comment.Text, comment.Position));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCard(Guid id, [FromBody] Card card)
        {
            await _cardService.UpdateCard(id, card);
            return Ok();
        }
    }

    public class CreateCommentDto
    {
        public string Text { get; set; }

        public int Position { get; set; }
    }


    public class CreateCardDto
    {
        public int Position { get; set; }

        public Guid SessionId { get; set; }
    }
}
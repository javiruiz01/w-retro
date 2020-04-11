// <copyright file="CardsController.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wRetroApi.Models;
using wRetroApi.Repositories;

namespace wRetroApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICommentRepository _commentRepository;

        public CardsController(ICardRepository cardRepository, ICommentRepository commentRepository)
        {
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
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
            await _cardRepository.CreateCard(card, cardInfo.SessionId);
            return Ok(card);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            await _cardRepository.DeleteCard(id);
            return Ok();
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> CreateComment(Guid id, [FromBody] CreateCommentDto comment)
        {
            return Ok(await _commentRepository.CreateComment(id, comment.Text));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateTitle(Guid id, [FromBody] UpdateTitleDto title)
        {
            await _cardRepository.UpdateTitle(id, title.Text);
            return Ok();
        }
    }

    public class CreateCommentDto
    {
        public string Text { get; set; }
    }


    public class CreateCardDto
    {
        public int Position { get; set; }

        public Guid SessionId { get; set; }
    }
}
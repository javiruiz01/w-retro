// <copyright file="CardsController.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wRetroApi.Repositories;

namespace wRetroApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CardsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> CreateComment(Guid id, [FromBody] CreateCommentDto comment)
        {
            return Ok(await _commentRepository.CreateComment(id, comment.Text));
        }
    }

    public class CreateCommentDto
    {
        public string Text { get; set; }
    }
}
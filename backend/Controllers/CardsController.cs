// <copyright file="CardsController.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wRetroApi.Models;
using wRetroApi.Services;

namespace wRetroApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public CardsController(ISessionService sessionService)
        {
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCards(string id)
        {
            return Ok(_sessionService.Get(id));
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateComments(string id, [FromBody] CommentDto comment)
        {
            _sessionService.AddComment(id, comment.CardIdx, comment.Comment);
            return Ok();
        }
    }

    public class CommentDto
    {
        public string Comment { get; set; }

        public int CardIdx { get; set; }
    }
}
// <copyright file="CommentController.cs" company="Payvision">
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
    public class CommentController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public CommentController(ISessionService sessionService)
        {
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
        }

        [Route("{sessionId}/{cardIdx}")]
        [HttpPost]
        public async Task<IActionResult> AddComment(string sessionId, int cardIdx, [FromBody] Comment comment)
        {
            return Ok(_sessionService.AddComment(sessionId, cardIdx, comment));
        }

        [Route("{sessionId}/{cardIdx}/{commentIdx}")]
        [HttpPost]
        public async Task<IActionResult> UpdateComment(string sessionId, int cardIdx, int commentIdx, [FromBody] Comment comment)
        {
            return Ok(_sessionService.UpdateComment(sessionId, cardIdx, commentIdx, comment));
        }

        [Route("{sessionId}/{cardIdx}/{commentId}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveComment(string sessionId, int cardIdx, string commentId)
        {
            return Ok(_sessionService.RemoveComment(sessionId, cardIdx, commentId));
        }
    }
}
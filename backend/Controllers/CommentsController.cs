// <copyright file="CommentsController.cs" company="Payvision">
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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UdpateComment(Guid id, [FromBody] Comment comment)
        {
            await _commentRepository.UpdateComment(id, comment);
            return Accepted();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _commentRepository.DeleteComment(id);
            return NoContent();
        }
    }
}
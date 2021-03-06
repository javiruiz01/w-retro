// <copyright file="SessionController.cs" company="Payvision">
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
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSession(Guid id)
        {
            var session = await _sessionService.GetSession(id);
            if (session == null)
            {
                return NotFound();
            }

            return Ok(session);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> CreateSession()
        {
            var session = await _sessionService.CreateSession();
            return Ok(session);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateTitle(Guid id, [FromBody] UpdateTitleDto title)
        {
            await _sessionService.UpdateTitle(id, title.Text);
            return Ok();
        }
    }
}
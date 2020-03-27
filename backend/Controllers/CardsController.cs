// <copyright file="CardsController.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            return Ok(_sessionService.Get());
        }
    }
}
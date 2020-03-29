// <copyright file="ICardService.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System.Collections.Generic;
using wRetroApi.Models;

namespace wRetroApi.Services
{
    public interface ISessionService
    {
        IEnumerable<Card> Get(string id);

        Comment AddComment(string id, int idx, Comment comment);

        Comment UpdateComment(string sessionId, int cardIdx, int commentIdx, Comment comment);

        IEnumerable<Card> RemoveComment(string id, int cardIdx, string commentId);
    }
}
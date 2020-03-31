// <copyright file="ICommentRepository.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wRetroApi.Models;

namespace wRetroApi.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetComments(Guid cardId);

        Task<Comment> CreateComment(Guid cardId, string text);

        Task UpdateComment(Guid id, Comment comment);

        Task DeleteComment(Guid id);
    }
}
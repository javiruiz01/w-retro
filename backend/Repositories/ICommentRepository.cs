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
    }
}
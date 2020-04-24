// <copyright file="Comment.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;

namespace wRetroApi.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Likes { get; set; }

        public int Position { get; set; }
    }
}
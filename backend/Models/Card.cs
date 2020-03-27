// <copyright file="Card.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace wRetroApi.Models
{
    public class Card
    {
        public string Title { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
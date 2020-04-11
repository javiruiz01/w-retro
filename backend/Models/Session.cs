// <copyright file="Session.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace wRetroApi.Models
{
    public class Session
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<Card> Cards { get; set; }
    }
}
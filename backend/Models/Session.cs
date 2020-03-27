// <copyright file="Session.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace wRetroApi.Models
{
    public class Session
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public IEnumerable<Card> Cards { get; set; }
    }
}
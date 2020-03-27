// <copyright file="CardService.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using wRetroApi.Models;

namespace wRetroApi.Services
{
    public class SessionService : ISessionService
    {
        private readonly IMongoCollection<Session> _session;

        public SessionService(IRetroDatabaseSettings settings)
        {
            var pack = new ConventionPack {new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("My Custom Conventions", pack, t => true);

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _session = database.GetCollection<Session>(settings.SessionCollectionName);
        }

        public IEnumerable<Card> Get(string id)
        {
            return _session
                .AsQueryable()
                .Where(x => x.Id == id)
                .Select(x => x.Cards)
                .FirstOrDefault();
        }

        public void AddComment(string id, int idx, string comment)
        {
            var filter = Builders<Session>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<Session>.Update.Push($"cards.{idx}.comments", new Comment {Text = comment});

            _session.UpdateOne(filter, update, new UpdateOptions {IsUpsert = true,});
        }
    }
}
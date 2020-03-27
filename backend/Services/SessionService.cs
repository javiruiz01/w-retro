// <copyright file="CardService.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Card> Get()
        {
            return _session.AsQueryable().Where(x => true).Select(x => x.Cards).ToList().First();
        }
    }
}
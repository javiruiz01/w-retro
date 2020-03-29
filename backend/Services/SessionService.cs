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

        public Comment AddComment(string id, int cardIdx, Comment comment)
        {
            var newComment = new Comment {CommentId = Guid.NewGuid().ToString(), Text = comment.Text};
            var filter = Builders<Session>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<Session>.Update.Push($"cards.{cardIdx}.comments", newComment);

            _session.UpdateOne(filter, update, new UpdateOptions {IsUpsert = true,});
            return newComment;
        }

        public Comment UpdateComment(string sessionId, int cardIdx, int commentIdx, Comment comment)
        {
            var filter = Builders<Session>.Filter.Eq("_id", ObjectId.Parse(sessionId));
            var update = Builders<Session>.Update.Set($"cards.{cardIdx}.comments.{commentIdx}", comment);
            _session.UpdateOne(filter, update);
            return comment;
        }

        public IEnumerable<Card> RemoveComment(string id, int cardIdx, string commentId)
        {
            var filter = Builders<Session>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<Session>.Update.PullFilter<Comment>(
                $"cards.{cardIdx}.comments",
                Builders<Comment>.Filter.Eq("commentId", commentId));

            return _session.FindOneAndUpdate(filter, update).Cards;
        }
    }
}
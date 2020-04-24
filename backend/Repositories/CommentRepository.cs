// <copyright file="CommentRepository.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using wRetroApi.Models;

namespace wRetroApi.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string _connectionString;

        public CommentRepository(IDatabaseSettings databaseSettings)
        {
            _connectionString = databaseSettings?.ConnectionString ?? throw new ArgumentNullException(nameof(databaseSettings));
        }


        public async Task<IEnumerable<Comment>> GetComments(Guid cardId)
        {
            var query = new StringBuilder()
                .Append("SELECT Id, Text, Likes, Position FROM [wretro].[wretro].[Comment]").AppendLine()
                .Append("WHERE CardId = @cardId").AppendLine()
                .Append("ORDER BY Position ASC")
                .ToString();

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Comment>(query, new {cardId});
            }
        }

        public async Task<Comment> CreateComment(Guid cardId, string text, int position)
        {
            var query = new StringBuilder()
                .Append("INSERT INTO [wretro].[wretro].[Comment] (Id, Text, Likes, CardId, Position)").AppendLine()
                .Append("VALUES (@id, @text, @likes, @cardId, @position)")
                .ToString();
            var comment = new Comment {Id = Guid.NewGuid(), Text = text, Likes = 0, Position = position};

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new {id = comment.Id, text = comment.Text, likes = comment.Likes, cardId, position = comment.Position});
                return comment;
            }
        }

        public async Task UpdateComment(Guid id, Comment comment)
        {
            var query = new StringBuilder()
                .Append("UPDATE [wretro].[wretro].[Comment]").AppendLine()
                .Append("SET Likes = @likes, Position = @position").AppendLine()
                .Append("WHERE Id = @id")
                .ToString();

            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, new {likes = comment.Likes, id, position = comment.Position});
        }

        public async Task DeleteComment(Guid id)
        {
            var query = new StringBuilder()
                .Append("DELETE FROM [wretro].[wretro].[Comment] WHERE Id = @id")
                .ToString();

            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, new {id});
        }
    }
}
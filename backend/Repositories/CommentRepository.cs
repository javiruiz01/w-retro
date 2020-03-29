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
                .Append("SELECT Id, Text, Likes FROM [wretro].[wretro].[Comment]")
                .Append("WHERE CardId = @cardId")
                .ToString();

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Comment>(query, new {cardId});
            }
        }
    }
}
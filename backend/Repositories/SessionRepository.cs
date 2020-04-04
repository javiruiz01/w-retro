// <copyright file="SessionRepository.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using wRetroApi.Models;
using wRetroApi.Services;

namespace wRetroApi.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly string _connectionString;

        public SessionRepository(IDatabaseSettings databaseSettings)
        {
            _connectionString = databaseSettings?.ConnectionString ?? throw new ArgumentNullException(nameof(databaseSettings));
        }

        public async Task CreateSession(Guid id)
        {
            var query = new StringBuilder()
                .Append("INSERT INTO [wretro].[wretro].[Session] (Id)")
                .AppendLine("VALUES (@id)")
                .ToString();
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, new {id});
        }
    }
}
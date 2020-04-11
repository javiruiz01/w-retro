// <copyright file="SessionRepository.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Data.SqlClient;
using System.Linq;
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

        public async Task<Session> GetSession(Guid id)
        {
            var query = new StringBuilder()
                .Append("SELECT Id, Title from [wretro].[wretro].[Session]").AppendLine()
                .Append("WHERE Id = @id")
                .ToString();

            using var connection = new SqlConnection(_connectionString);
            return (await connection.QueryAsync<Session>(query, new {id})).FirstOrDefault();
        }

        public async Task CreateSession(Guid id, string title)
        {
            var query = new StringBuilder()
                .Append("INSERT INTO [wretro].[wretro].[Session] (Id, Title)")
                .AppendLine("VALUES (@id, @title)")
                .ToString();
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, new {id, title});
        }

        public async Task UpdateTitle(Guid id, string title)
        {
            var query = new StringBuilder()
                .Append("UPDATE [wretro].[wretro].[Session] SET Title = @title").AppendLine()
                .Append("WHERE Id = @id")
                .ToString();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new {title, id});
            }
        }
    }
}
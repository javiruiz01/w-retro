// <copyright file="CardRepository.cs" company="Payvision">
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
    public class CardRepository : ICardRepository
    {
        private readonly string _connectionString;

        public CardRepository(IDatabaseSettings databaseSettings)
        {
            _connectionString = databaseSettings?.ConnectionString ?? throw new ArgumentNullException(nameof(databaseSettings));
        }

        public async Task<IEnumerable<Card>> GetCards(Guid id)
        {
            var query = new StringBuilder()
                .Append("SELECT Id, Title, Position FROM [wretro].[wretro].[Card]").AppendLine()
                .Append("WHERE SessionId = @sessionId").AppendLine()
                .Append("ORDER By Position ASC")
                .ToString();

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Card>(query, new {sessionId = id}).ConfigureAwait(false);
            }
        }

        public async Task CreateCard(Card card, Guid sessionId)
        {
            var query = new StringBuilder()
                .Append("INSERT INTO [wretro].[wretro].[Card] (Id, Title, Position, SessionId)")
                .AppendLine("VALUES (@id, @title, @position, @sessionId)")
                .ToString();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new
                {
                    id = card.Id,
                    title = card.Title,
                    position = card.Position,
                    sessionId,
                }).ConfigureAwait(false);
            }
        }

        public async Task UpdateTitle(Guid id, string title)
        {
            var query = new StringBuilder()
                .Append("UPDATE [wretro].[wretro].[Card] SET Title = @title").AppendLine()
                .Append("WHERE Id = @id")
                .ToString();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new {title, id});
            }
        }

        public async Task DeleteCard(Guid id)
        {
            var query = new StringBuilder()
                .Append("DELETE FROM [wretro].[wretro].[Card]").AppendLine()
                .Append("WHERE Id = @id")
                .ToString();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new {id});
            }

            {
            }
        }
    }
}
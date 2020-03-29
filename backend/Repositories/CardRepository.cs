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
                .Append("SELECT Id, Title FROM [wretro].[wretro].[Card]")
                .Append("WHERE SessionId = @sessionId")
                .ToString();

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Card>(query, new {sessionId = id}).ConfigureAwait(false);
            }
        }
    }
}
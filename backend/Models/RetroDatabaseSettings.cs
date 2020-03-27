// <copyright file="RetroDatabaseSettings.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace wRetroApi.Models
{
    public class RetroDatabaseSettings : IRetroDatabaseSettings
    {
        public string SessionCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IRetroDatabaseSettings
    {
        string SessionCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
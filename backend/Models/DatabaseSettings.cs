// <copyright file="DatabaseSettings.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace wRetroApi.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
    }
}
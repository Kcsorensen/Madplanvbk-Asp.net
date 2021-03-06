﻿using MadplanVbkAsp.Extensions;
using MongoDB.Driver;
using SharedLib.Models;
using System;

namespace MadplanVbkAsp.Data
{
    public class MongoDbContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        public IMongoDatabase _database { get; }

        public MongoDbContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access MongoDb-server.", ex);
            }
        }

        public IMongoCollection<Food> Foods
        {
            get
            {
                return _database.GetCollection<Food>("Foods");
            }
        }

        public IMongoCollection<Recipe> Recipes
        {
            get
            {
                return _database.GetCollection<Recipe>("Recipes");
            }
        }
    }
}

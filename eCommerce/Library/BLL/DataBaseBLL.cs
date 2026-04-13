using Library.DTO;
using Library.Interface;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Library.BLL
{
    public class DataBaseBLL
    {
        string connectionString;
        string catalog = "sample_mflix";
        const int GET_LIMIT = 10;
        MongoClient db;
        public DataBaseBLL(IConfiguration config)
        {
            connectionString = config["MongoDB:ConnectionString"];
            db = new MongoClient(connectionString);
        }

        public async Task<bool> Insert<T>(T entity, string collectionName)
        {
            var database = db.GetDatabase(catalog);

            var collection = database.GetCollection<T>(collectionName);

            await collection.InsertOneAsync(entity);

            return true;
        }

        public async Task<bool> DeleteByName<T>(string collectionName, string name) where T : IHasName
        {
            var database = db.GetDatabase(catalog);

            var collection = database.GetCollection<T>(collectionName);

            var filter = Builders<T>.Filter.Eq(x => x.Name, name);

            var result = await collection.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }

        public async Task<List<ProductStore>> Get(string? query)
        {
            var database = db.GetDatabase(catalog);
            var collection = database.GetCollection<ProductStore>("Product");
            List<ProductStore> result;

            if (query != null && query.Length > 0)
            {
                var filter = Builders<ProductStore>.Filter.Or(
                    Builders<ProductStore>.Filter.Regex(
                        "Tags",
                        new MongoDB.Bson.BsonRegularExpression(query, "i")
                    ),
                    Builders<ProductStore>.Filter.Regex(
                        "Type",
                        new MongoDB.Bson.BsonRegularExpression(query, "i")
                    )
                );

                result = await collection
                .Find(filter)
                .Limit(GET_LIMIT)
                .ToListAsync();             
            }
            else 
            {
                result = await collection
                .Find(Builders<ProductStore>.Filter.Empty)
                .Limit(GET_LIMIT)
                .ToListAsync();
            }

            return result;
        }
     
    }
}

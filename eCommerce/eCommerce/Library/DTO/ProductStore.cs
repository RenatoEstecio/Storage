using Library.Interface;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DTO
{
    
    [BsonIgnoreExtraElements]    
    public class ProductStore : IHasName
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<string> Colors { get; set; } = new();
        public string? Observation { get; set; }
        public string? ImgUrl { get; set; }
        public string? Type { get; set; }
    }
}

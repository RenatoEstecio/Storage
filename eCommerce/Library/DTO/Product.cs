using Library.Interface;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DTO
{
    public class Product
    {     
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Img64 { get; set; }
        public string? ImgLink { get; set; }
    }

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

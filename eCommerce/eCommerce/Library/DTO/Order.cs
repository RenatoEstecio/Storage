using Library.Interface;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DTO
{

    [BsonIgnoreExtraElements]
    public class Order : IHasName
    {
        public string Name { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;       
        public List<ProductItem> items { get; set; } = new();
        
        public string? Observation { get; set; }
        public string? Address { get; set; }
        public ClientOrder? Client { get; set; }
    }
}

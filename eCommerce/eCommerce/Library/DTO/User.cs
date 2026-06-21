using MongoDB.Bson.Serialization.Attributes;

namespace Library.DTO
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public string Email { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Church.Core.Entities
{
    public class Usermongo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string username { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public List<string> permission { get; set; } = new List<string>(); 
    }
}

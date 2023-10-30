using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
    }
}

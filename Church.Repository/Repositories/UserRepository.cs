using Church.Core.Entities;
using Church.Core.Interfaces.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        internal MondoDbRepository _repository = new MondoDbRepository();
        private IMongoCollection<Usermongo> colletion;


        public UserRepository() {

            colletion = _repository.db.GetCollection<Usermongo>("users");
        }

        public async Task<List<Usermongo>> GetAllMongos()
        {
            return await colletion.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}

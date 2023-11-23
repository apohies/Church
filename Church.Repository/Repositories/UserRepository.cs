using Amazon.Runtime;
using Church.Core.Dtos.InfoUser;
using Church.Core.Entities;
using Church.Core.Interfaces.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Church.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        internal MondoDbRepository _repository = new MondoDbRepository();
        private readonly IMongoCollection<BsonDocument> _usersCollection;
        private IMongoCollection<Usermongo> colletion;


        public UserRepository() {

            colletion = _repository.db.GetCollection<Usermongo>("users");

            _usersCollection = _repository.db.GetCollection<BsonDocument>("users");
        }

        public async Task<List<Usermongo>> GetAllMongos()
        {
            return await colletion.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Usermongo> GetUserbiName(string name) {

            return await colletion.FindAsync(new BsonDocument { { "username", name } }).Result.FirstAsync();
        }

        // metodo para buscar usuario por nombre  y contraseña
        public async Task<Usermongo> GetUserbiNameAndPassword(string name, string password)
        {

            return await colletion.FindAsync(new BsonDocument { { "username", name }, { "password", password } }).Result.FirstOrDefaultAsync();
        }

        public async Task<InfoBasicUserDto> GetUserbyEmail(string email)
        {
            var projection = Builders<BsonDocument>.Projection
           .Include("username")
            .Include("email")
            .Include("password");

            var filter = Builders<BsonDocument>.Filter.Eq("email", email);

            var result = await _usersCollection.Find(filter).Project(projection).FirstOrDefaultAsync();

            InfoBasicUserDto salida = new InfoBasicUserDto() { username = result["username"].AsString , email = result["email"].AsString , password = result["password"].AsString };

            return salida;
        }
    }
}

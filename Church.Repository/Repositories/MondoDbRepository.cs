using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Repository.Repositories
{
    public class MondoDbRepository
    {

        public MongoClient client;

        public IMongoDatabase db;

        public MondoDbRepository()
        {


            client = new MongoClient("mongodb+srv://apohies:b64EXr4RYm0pojls@cluster0.ykyig.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            db = client.GetDatabase("pymongo");
        }
    }
}

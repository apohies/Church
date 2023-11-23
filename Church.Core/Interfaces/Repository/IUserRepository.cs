using Church.Core.Dtos.InfoUser;
using Church.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Core.Interfaces.Repository
{
    public interface IUserRepository
    {

        public  Task<List<Usermongo>> GetAllMongos();

        public Task<Usermongo> GetUserbiName(string name);

        public Task<Usermongo> GetUserbiNameAndPassword(string name, string password);

        public  Task<InfoBasicUserDto> GetUserbyEmail(string email);
    }
}

using Church.Core.Dtos.Security;
using Church.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Core.Interfaces.Service
{
    public interface IUserService
    {
        public Task<List<Usermongo>> GetAllMongos();

        public  Task<Usermongo> GetUserbiName(string name);

        public Task<UserDto> GetUserbiNameAndPassword(string name, string password);

        public Task<UserDto> GetUserbyEmail(string email);
    }
}

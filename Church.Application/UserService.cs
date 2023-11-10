using Church.Core.Entities;
using Church.Core.Interfaces.Repository;
using Church.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository) {
            this.userRepository = userRepository;   
        
        }

        public async Task<List<Usermongo>> GetAllMongos() { 
        
        return await userRepository.GetAllMongos();
        }

        public async Task<Usermongo> GetUserbiName(string name)
        {
            return await userRepository.GetUserbiName(name);
        }
    }
}

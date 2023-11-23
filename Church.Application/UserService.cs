using Church.Core.Dtos.InfoUser;
using Church.Core.Dtos.Security;
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

        public async Task<UserDto> GetUserbiNameAndPassword(string name, string password)
        {
            UserDto userDto = new UserDto();

           Usermongo user = await userRepository.GetUserbiNameAndPassword(name, password);

            if (user != null)
            {
                userDto.username = user.username;
                userDto.email = user.email;

                return userDto;
            }

            return userDto;


        }

        public async Task<UserDto> GetUserbyEmail(string email)
        {
            UserDto userDto = new UserDto();

            InfoBasicUserDto user = await userRepository.GetUserbyEmail(email);

            if (user != null)
            {
                userDto.username = user.username;
                userDto.email = user.email;

                return userDto;
            }

            return userDto;
        }
    }
}

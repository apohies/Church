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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Security
{
    public interface ICommand
    {
        public string GenerateToken(string name, string user, List<string> permision);

        public string RefreshToken(string usuario);

        Tuple<bool, object> ValidateToken(string token);

        string GenerateToken1(string email);
    }
}

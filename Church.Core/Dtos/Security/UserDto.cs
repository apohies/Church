using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Core.Dtos.Security
{
    public class UserDto
    {
        public string username { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public List<string> permision { get; set; } = new List<string>();
    }
}

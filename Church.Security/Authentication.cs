using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Security
{
    public class Authentication
    {


        public  string SecretKey { get; set; }  
        public  string Issuer { get; set; } 
        public  string Audience { get; set; }

        public  string MinutesToken { get; set; }
    }
}

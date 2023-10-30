using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Security
{
    public class DatosUsuarioToken
    {

        public string usuario { get; set; } = string.Empty; 
        public DateTime fecha_creacion { get; set; }

        public DateTime fecha_expiracion { get; set; }
    }
}

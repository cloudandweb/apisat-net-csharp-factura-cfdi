using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Modelos
{
    public partial class Emisor
    {
        public void Valida()
        {
            if (string.IsNullOrEmpty(llave_privada))
                throw new Exception("La llave privada es obligatoria.");
            if (string.IsNullOrEmpty(llave_publica))
                throw new Exception("La llave publica es obligatoria.");
           
        }
    }
}

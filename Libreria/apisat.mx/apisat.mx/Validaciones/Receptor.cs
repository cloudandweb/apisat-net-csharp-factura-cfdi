using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Modelos
{
    public partial class Receptor
    {
        public void Valida()
        {
            if (string.IsNullOrEmpty(razon_social))
                throw new Exception("La razon social es obligatoria");
            if (string.IsNullOrEmpty(rfc))
                throw new Exception("El RFC es obligatorio.");
            if (string.IsNullOrEmpty(telefono))
                throw new Exception("El Telefono es obligatorio.");

            this.direccion.Valida();
        }
    }
}

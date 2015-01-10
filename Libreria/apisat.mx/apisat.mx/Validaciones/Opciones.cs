using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Modelos
{
    public partial class Opciones
    {
        public void Valida()
        {
            if (string.IsNullOrEmpty(forma_pago))
                throw new Exception("La forma de pago es obligatoria.");
        }
    }
}

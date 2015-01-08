using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public partial class Direccion
    {
        public void Valida()
        {
            if (string.IsNullOrEmpty(calle))
                throw new Exception("El nombre de la calle es obligatorio");
            if (string.IsNullOrEmpty(exterior))
                throw new Exception("El numero exterior es obligatorio");
            if (string.IsNullOrEmpty(estado))
                throw new Exception("El estado es obligatorio");
            if (string.IsNullOrEmpty(ciudad))
                throw new Exception("La ciudad es obligatorio");
            if (string.IsNullOrEmpty(codigo_postal))
                throw new Exception("El codigo postal es obligatorio");
            if (string.IsNullOrEmpty(colonia))
                throw new Exception("La colonia es obligatoria");
        }
    }
}

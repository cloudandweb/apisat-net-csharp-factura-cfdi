using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public partial class FacturaDetalle
    {
        private void Valida()
        {
            if (string.IsNullOrEmpty(llaves.llave_publica))
                throw new Exception("La llave publica es obligatoria");
            if (string.IsNullOrEmpty(llaves.llave_privada))
                throw new Exception("La llave privada es obligatoria");
            if (string.IsNullOrEmpty(uuid))
                throw new Exception("El folio fiscal es obligatorio");
        }

        public bool ValidaObjeto()
        {
            Valida();
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Modelos
{
    public partial class FacturaDetalle
    {
        private void Valida()
        {
           
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

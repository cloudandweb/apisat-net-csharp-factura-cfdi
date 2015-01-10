using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Modelos
{
    public partial class Cancelacion
    {
        private void Valida()
        {
            if (string.IsNullOrEmpty(this.llaves.llave_privada))
                throw new Exception("La llave privada es obligatoria");
            if (string.IsNullOrEmpty(this.llaves.llave_publica))
                throw new Exception("La llave publica es obligatoria");
            if (string.IsNullOrEmpty(this.rfc))
                throw new Exception("El RFC es obligatorio");
            if (string.IsNullOrEmpty(this.uuid))
                throw new Exception("El uuid folio fiscal es obligatorio");
        }

        public bool ValidaObjeto()
        {
            Valida();
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Cliente
{
    public partial class Apisat
    {
        private void Valida()
        {
            if (string.IsNullOrEmpty(llave_privada))
                throw new Exception("La llave privada es obligatoria.");
            if (string.IsNullOrEmpty(llave_publica))
                throw new Exception("La llave publica es obligatoria.");
            if(string.IsNullOrEmpty(url))
                throw new Exception("El url del API es obligatorio.");
            if(string.IsNullOrEmpty(CFDIUrn))
                throw new Exception("El nombre del recurso es obligatorio.");
        }

        public bool Validar()
        {
            Valida();
            return true;
        }
    }
}

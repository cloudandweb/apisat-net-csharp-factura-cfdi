using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Respuestas
{
    public abstract class Respuesta
    {
        public Respuesta()
        {

        }

        public string codigo { get; set; }

        public string mensaje { get; set; }

    }
}

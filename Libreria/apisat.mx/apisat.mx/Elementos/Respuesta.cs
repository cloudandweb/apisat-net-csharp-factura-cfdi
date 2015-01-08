using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public class Respuesta
    {
        public Respuesta()
        {

        }

        public string codigo { get; set; }

        public string mensaje { get; set; }

        public Archivos archivos { get; set; }

        public string uuid { get; set; }
    }
}

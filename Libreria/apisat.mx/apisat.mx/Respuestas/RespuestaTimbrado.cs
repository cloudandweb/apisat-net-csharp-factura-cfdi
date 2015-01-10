using apisat.mx.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Respuestas
{
    public class RespuestaTimbrado : Respuesta
    {
        public RespuestaTimbrado() 
        { 

        }

        public Archivos archivos { get; set; }

        public string uuid { get; set; }
    }
}

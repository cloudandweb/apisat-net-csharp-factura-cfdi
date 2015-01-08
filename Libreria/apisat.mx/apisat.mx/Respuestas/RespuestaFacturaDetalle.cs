using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public class RespuestaFacturaDetalle
    {
        public RespuestaFacturaDetalle()
        {
            this.detalle = new List<Articulo>();
        }

        public string rfc { get; set; }

        public List<Articulo> detalle { get; set; }
    }
}

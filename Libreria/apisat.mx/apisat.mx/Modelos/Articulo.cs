using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Modelos
{
    public partial class Articulo
    {
        public string id { get; set; }

        public string descripcion { get; set; }

        public decimal precio { get; set; }

        public int cantidad { get; set; }

        public decimal total { get; set; }
    }
}

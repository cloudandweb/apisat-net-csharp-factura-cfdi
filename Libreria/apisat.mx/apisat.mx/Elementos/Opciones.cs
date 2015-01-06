using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public class Opciones
    {
        /// <summary>
        /// Ingreso, Egreso
        /// </summary>
        public string tipo_factura { get; set; }

        /// <summary>
        /// MXN, USD, EUR
        /// </summary>
        public string moneda { get; set; }

        public string pago { get; set; }

        public string forma_pago { get; set; }
    }
}

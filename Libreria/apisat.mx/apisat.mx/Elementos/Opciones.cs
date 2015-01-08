using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public partial class Opciones
    {
        public Opciones()
        {
            
        }
        
        [JsonIgnore]
        public tipos_factura tipo_factura { get; set; }

        [JsonIgnore]
        public Monedas moneda { get; set; }

        [JsonIgnore]
        public Pagos pago { get; set; }

        
        public string forma_pago { get; set; }

        [JsonProperty("tipo_factura")]
        public string tipo_de_factura
        {
            get { return tipo_factura.ToString(); }
        }

        [JsonProperty("moneda")]
        public string Moneda
        {
            get { return moneda.ToString(); }
        }

        [JsonProperty("pago")]
        public string Pago
        {
            get { return pago.ToString(); }
        }

        public enum tipos_factura
        {
            Ingreso = 0, Egreso = 1
        }

        public enum Monedas
        {
            MXN = 0, USD = 1, EUR = 2
        }

        public enum Pagos
        {
            Efectivo = 0, 
            Cheque = 1,
            Transferencia = 2
        }
    }
}

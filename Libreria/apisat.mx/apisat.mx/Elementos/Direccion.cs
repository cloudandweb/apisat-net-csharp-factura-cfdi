using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public partial class Direccion
    {
        public Direccion() 
        { 
        
        }

        public string calle { get; set; }

        public string exterior { get; set; }

        public string estado { get; set; }

        public string ciudad { get; set; }

        public string codigo_postal { get; set; }

        [JsonProperty("correo", NullValueHandling = NullValueHandling.Ignore)]
        public string correo_electronico { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string nombre_contacto { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string interior { get; set; }


        public string colonia { get; set; }
    }
}

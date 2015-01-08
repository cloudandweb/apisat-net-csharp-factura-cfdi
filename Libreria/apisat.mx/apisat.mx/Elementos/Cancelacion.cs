using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public partial class Cancelacion
    {

        public Cancelacion() 
        {
            this.llaves = new Llaves();
        }

        public Llaves llaves { get; set; }

        public string rfc { get; set; }

        [JsonProperty("UUID")]
        public string uuid { get; set; }

    }

    public class Llaves
    {
        public Llaves() 
        { 
        
        }

        public string llave_publica { get; set; }

        public string llave_privada { get; set; }

    }
}

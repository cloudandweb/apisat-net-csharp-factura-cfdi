using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;



namespace apisat.mx.Elementos
{
    public partial class Receptor
    {

        public Receptor()
        {
            this.direccion = new Direccion();
        }

        [JsonProperty("nombre")]
        public string razon_social { get; set; }

        public string rfc { get; set; }

        public string telefono { get; set; }

        public Direccion direccion { get; set; }

        

    }
}

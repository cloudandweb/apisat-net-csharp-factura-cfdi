using System;
using System.ComponentModel.DataAnnotations;



namespace apisat.mx.Elementos
{
    public class Receptor
    {

        public Receptor()
        {
            this.direccion = new Direccion();
        }

        public string nombre { get; set; }

        public string rfc { get; set; }

        public string telefono { get; set; }

        public Direccion direccion { get; set; }

        

    }
}

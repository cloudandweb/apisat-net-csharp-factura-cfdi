using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public class Factura
    {
        public Factura()
        {
            this.articulos = new List<Articulo>();
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.opciones = new Opciones();
            this.totales = new Totales();
        }

        public Emisor emisor { get; set; }

        public List<Articulo> articulos { get; set; }

        public Receptor receptor { get; set; }

        public Opciones opciones { get; set; }

        public Totales totales { get; set; }

        public bool Validar()
        {

            return true;
        }
    }
}

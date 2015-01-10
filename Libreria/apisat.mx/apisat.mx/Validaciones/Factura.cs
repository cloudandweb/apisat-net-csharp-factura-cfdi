using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Modelos
{
    public partial class Factura
    {
        public bool ValidaObjeto()
        {
            emisor.Valida();
            receptor.Valida();
            this.Valida();
            opciones.Valida();
            totales.Valida();
            return true;
        }

        private void Valida()
        {
            if (this.articulos.Count <= 0)
                throw new Exception("La coleccion de articulos debe contener por lo menos 1 articulo/producto o servicio.");
            foreach (var a in this.articulos)
            {
                a.Valida();
            }

        }
    }
}

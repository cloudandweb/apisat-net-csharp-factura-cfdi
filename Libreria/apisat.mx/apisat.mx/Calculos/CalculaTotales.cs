using apisat.mx.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx
{
    public class CalculaTotales
    {
        public static Factura ProcesaCompleta(Factura factura)
        {
            foreach (var a in factura.articulos)
            {
                a.total = a.precio * a.cantidad;
            }

            factura.totales.monto = (double)factura.articulos.Sum(x => x.total);
            factura.totales.iva = factura.totales.monto * factura.totales.agregado;
            factura.totales.total = factura.totales.monto + factura.totales.iva + factura.totales.isr + factura.totales.riva;


            return factura;
        }
    }
}

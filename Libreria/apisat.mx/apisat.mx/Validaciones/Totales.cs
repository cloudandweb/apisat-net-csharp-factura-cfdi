using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public partial class Totales
    {
        public void Valida()
        {
            if (monto.HasValue == false)
                throw new Exception("El monto de la factura no puede ser nulo");
            if (agregado.HasValue ==  false)
                throw new Exception("El porcentaje de iva no puede ser nulo");
            if (iva.HasValue == false)
                throw new Exception("El IVA no puede ser nulo, puede especificar 0.00 si asi lo requiere.");
            if (isr.HasValue == false)
                throw new Exception("El ISR no puede ser nulo, puede especificar 0.00 si asi lo requiere.");
            if (riva.HasValue == false)
                throw new Exception("El RIVA no puede ser nulo, puede especificar 0.00 si asi lo requiere.");
            if (total.HasValue == false)
                throw new Exception("El Total no puede ser nulo.");

        }
    }
}

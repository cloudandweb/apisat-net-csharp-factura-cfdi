using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Modelos
{
    public partial class FacturaDetalle
    {
        public FacturaDetalle() 
        {
            this.llaves = new ApiLlaves();
        }

        public ApiLlaves llaves { get; set; }

        public string uuid { get; set; }
    }
}

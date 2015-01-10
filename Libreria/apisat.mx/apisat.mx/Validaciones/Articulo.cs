using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace apisat.mx.Modelos
{
    public partial class Articulo
    {
        public void Valida()
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("Es necesario especificar el id del articulo");
            if (string.IsNullOrEmpty(descripcion))
                throw new Exception("Es necesario especificar la descripcion del articulo");
            if (precio < 0)
                throw new Exception("El precio del articulo debe ser igual o mayor a 0");
            if (cantidad <= 0)
                throw new Exception("La cantidad de articulos debe ser mayor a 0");
            if (total < 0)
                throw new Exception("El total debe ser mayor o igual a 0");
            
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using apisat.mx;
using apisat.mx.Elementos;

namespace UnitTestapisat
{
    [TestClass]
    public class UnitTestConsultas
    {
        [TestMethod]
        public void ConsultaFactura()
        {
            FacturaDetalle detalle = new FacturaDetalle();
            detalle.llaves.llave_privada = "key_6b305bf82216f505d826e4c1cf8df5b2";
            detalle.llaves.llave_publica = "key_f7f99088d457278fa1b059c34f01df5d";
            detalle.uuid = "21321321321321";

            Apisat api = new Apisat("http://sandbox.apisat.mx/");
            api.detalle = detalle;
            var respuesta = api.Consultar();

            Assert.IsFalse(string.IsNullOrEmpty(respuesta.rfc));
        }
    }
}

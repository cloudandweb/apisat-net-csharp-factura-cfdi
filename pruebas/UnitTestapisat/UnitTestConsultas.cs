using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using apisat.mx;
using apisat.mx.Modelos;
using apisat.mx.Cliente;

namespace UnitTestapisat
{
    [TestClass]
    public class UnitTestConsultas
    {
        [TestMethod]
        public void ConsultaFactura()
        {
            FacturaDetalle detalle = new FacturaDetalle();
            detalle.uuid = "21321321321321";

            Apisat api = new Apisat("http://sandbox.apisat.mx/");
            //reemplaza por tus propias llaves 
            api.llave_publica = "key_f7f99088d457278fa1b059c34f01df5d"; 
            api.llave_privada = "key_6b305bf82216f505d826e4c1cf8df5b2";
            api.detalle = detalle;
            var respuesta = api.Consultar();

            Assert.IsFalse(string.IsNullOrEmpty(respuesta.rfc));
        }
    }
}

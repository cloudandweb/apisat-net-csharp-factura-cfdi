﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using apisat.mx;
using apisat.mx.Modelos;
using apisat.mx.Respuestas;
using apisat.mx.Cliente;

namespace UnitTestapisat
{
    [TestClass]
    public class UnitTestpeticion
    {
        //reemplaza por tus llaves
        public const string mi_public_key = "key_f7f99088d457278fa1b059c34f01df5d";
        public const string mi_private_key = "key_6b305bf82216f505d826e4c1cf8df5b2"; 

        #region Timbrado
        [TestMethod]
        public void Timbrado()
        {
            
            Factura factura = new Factura();
            
            factura.receptor.direccion.calle = "Olas altas";
            factura.receptor.direccion.ciudad = "Tijuana";
            factura.receptor.direccion.codigo_postal = "22440";
            factura.receptor.direccion.colonia = "La escondida";
            factura.receptor.direccion.correo_electronico = "contacto@empresa.com";
     
            factura.receptor.direccion.estado = "Baja California";
            factura.receptor.direccion.exterior = "13251";
            factura.receptor.direccion.interior = "B10";
            factura.receptor.razon_social = "Cafe Expresso Delicioso S.A de C.V";
            factura.receptor.direccion.nombre_contacto = "Leonardo Xavier";
            factura.receptor.rfc = "CAFE140101L83";
            factura.receptor.telefono = "01800001001";

            factura.opciones.forma_pago = "Pago en una sola exhibicion";
            factura.opciones.moneda = Opciones.Monedas.MXN;
            factura.opciones.pago = Opciones.Pagos.Efectivo;
            factura.opciones.tipo_factura = Opciones.tipos_factura.Ingreso;
          
            factura.articulos.Add(
                    new Articulo() { id="1", descripcion="Hora de soporte", cantidad=5, precio=800, total=4000 });
            factura.articulos.Add(
                    new Articulo() { id="2", descripcion="restauracion de base de datos", cantidad=1, precio=2000, total=2000 });

            factura.totales.agregado = .16;
            factura.totales.isr = 0;
            factura.totales.riva = 0;
            CalculaTotales.ProcesaCompleta(factura);



            Apisat cfdi = new Apisat("http://sandbox.apisat.mx/");
            cfdi.factura = factura;
            cfdi.llave_publica = mi_public_key;
            cfdi.llave_privada = mi_private_key;
            RespuestaTimbrado respuesta = cfdi.Timbrar();
            Assert.AreEqual("200", respuesta.codigo);



        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TimbraConDatosIncompletos()
        {
           
                Factura factura = new Factura();
               
                factura.receptor.direccion.calle = "Olas altas";
                factura.receptor.direccion.ciudad = "Tijuana";
                factura.receptor.direccion.codigo_postal = "22440";
                factura.receptor.direccion.colonia = "La escondida";
                factura.receptor.direccion.correo_electronico = "contacto@empresa.com";

                factura.receptor.direccion.estado = "Baja California";
                factura.receptor.direccion.exterior = "13251";
                factura.receptor.direccion.interior = "B10";
                factura.receptor.razon_social = "Cafe Expresso Delicioso S.A de C.V";
                factura.receptor.direccion.nombre_contacto = "Leonardo Xavier";
                factura.receptor.rfc = "CAFE140101L83";
                factura.receptor.telefono = "01800001001";

                factura.opciones.forma_pago = "Pago en una sola exhibicion";
                factura.opciones.moneda = Opciones.Monedas.MXN;
                factura.opciones.pago = Opciones.Pagos.Efectivo;
                factura.opciones.tipo_factura = Opciones.tipos_factura.Ingreso;

               /* factura.articulos.Add(
                        new Articulo() { id = "1", descripcion = "Hora de soporte", cantidad = 5, precio = 800, total = 4000 });
                factura.articulos.Add(
                        new Articulo() { id = "2", descripcion = "restauracion de base de datos", cantidad = 1, precio = 2000, total = 2000 });
                */
                factura.totales.agregado = .16;
                factura.totales.isr = 0;
                factura.totales.riva = 0;
                CalculaTotales.ProcesaCompleta(factura);



                Apisat cfdi = new Apisat("http://sandbox.apisat.mx/");
                cfdi.factura = factura;
                Respuesta respuesta = cfdi.Timbrar();
               
        }

        [TestMethod]
        public void CalculaTotalFactura()
        {
            Factura factura = new Factura();
          
            factura.articulos.Add(
                    new Articulo() { id = "1", descripcion = "Hora de soporte", cantidad = 5, precio = 800, total = 0 });
            factura.articulos.Add(
                    new Articulo() { id = "2", descripcion = "restauracion de base de datos", cantidad = 1, precio = 2000, total = 0 });

            
            factura.totales.agregado = .16;
            factura.totales.isr = -60;
            factura.totales.iva = 0;
            factura.totales.monto = 0;
            factura.totales.riva = 200;
            //factura.totales.total = 6960;

            CalculaTotales.ProcesaCompleta(factura);

            Assert.AreEqual(7100, factura.totales.total);
        }

        #endregion

        #region Cancelacion

        [TestMethod]
        public void CancelaFactura() 
        {
            Cancelacion cancelacion = new Cancelacion();
            //El RFC debe coincidir con el UUID por seguridad. En sandbox no importa que datos se pongan
            cancelacion.rfc = "RFC28289191";
            cancelacion.uuid = "342343242324242442";

            Apisat cfdi = new Apisat("http://sandbox.apisat.mx");
            cfdi.llave_publica = mi_public_key;
            cfdi.llave_privada = mi_private_key;
            cfdi.cancelacion = cancelacion;
            Respuesta respuesta = cfdi.Cancelar();
            Assert.AreEqual("200", respuesta.codigo);
        }

        #endregion
    }
}

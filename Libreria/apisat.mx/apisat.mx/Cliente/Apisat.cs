using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apisat.mx.Elementos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;

namespace apisat.mx
{
    public class Apisat
    {
        public Apisat()
        {
            this.factura = new Factura();
            this.cancelacion = new Cancelacion();
            this.detalle = new FacturaDetalle();
        }

        public Apisat(string rutabase)
        {
            this.factura = new Factura();
            this.cancelacion = new Cancelacion();
            this.detalle = new FacturaDetalle();
            this.url = rutabase;
            this.CFDIUrn = "/api/1.0/factura";
        }

        public string url { get; set; }

        public string CFDIUrn
        {
            get;
            set;
        }

        public Factura factura { get; set; }

        public Cancelacion cancelacion { get; set; }

        public FacturaDetalle detalle { get; set; }



        private Peticion peticionTimbre { 
            get 
            {
                Peticion p = new Peticion();
                p.factura = this.factura;
                return p;
            } 
        }

        private Peticion peticionCancelacion
        {
            get 
            {
                Peticion p = new Peticion();
                p.cancelacion = this.cancelacion;
                return p;
            }
        }

        private Peticion peticionDetalle
        {
            get 
            {
                Peticion p = new Peticion();
                p.facturaDetalle = this.detalle;
                return p;
            }
        }

        public Respuesta Timbrar()
        {
            Respuesta respuesta = new Respuesta();
            if (this.factura.ValidaObjeto())
                respuesta = creaCFDI();
            else
                throw new Exception("Faltan algunos atributos");
            return respuesta;
        }

        public Respuesta Cancelar() 
        {
            Respuesta respuesta = new Respuesta();
            if (this.cancelacion.ValidaObjeto())
                respuesta = cancelaCFDI();
            else
                throw new Exception("El objeto cancelacion contiene datos incorrectos");
            

            return respuesta;
        }

        public RespuestaFacturaDetalle Consultar()
        {
            RespuestaFacturaDetalle respuesta = new RespuestaFacturaDetalle();
            if (this.detalle.ValidaObjeto())
                respuesta = detalleCFDI();
            else
                throw new Exception("El objeto para detalle no esta completo.");

            return respuesta;
        }

        private Respuesta creaCFDI()
        {
            string json = JsonConvert.SerializeObject(this.peticionTimbre);
            Respuesta respuesta = new Respuesta();
            using (var cliente = new WebClient())
            {
                cliente.Headers[HttpRequestHeader.ContentType] = "application/json";
                string json_respuesta = cliente.UploadString(new Uri(string.Format("{0}{1}", this.url, this.CFDIUrn)) , json);
                respuesta = JsonConvert.DeserializeObject<Respuesta>(json_respuesta);
            }

            return respuesta;
        }

        private Respuesta cancelaCFDI()
        {
            string json = JsonConvert.SerializeObject(this.cancelacion);
            Respuesta respuesta = new Respuesta();
            using (var cliente = new WebClient()) 
            {
                cliente.Headers[HttpRequestHeader.ContentType] = "application/json";
                string json_respuesta = cliente.UploadString(new Uri(string.Format("{0}{1}", this.url, this.CFDIUrn)), "DELETE", json);
                respuesta = JsonConvert.DeserializeObject<Respuesta>(json_respuesta);
            }
            return respuesta;
        }

        private RespuestaFacturaDetalle detalleCFDI()
        {
            RespuestaFacturaDetalle respuesta = new RespuestaFacturaDetalle();
            using(var cliente = new WebClient()) {
              string consulta = cliente.DownloadString(url +
              string.Format("/api/1.0/factura?folio={0}&llave_privada={1}&llave_publica={2}", this.detalle.uuid, this.detalle.llaves.llave_privada, this.detalle.llaves.llave_publica));

              respuesta = JsonConvert.DeserializeObject<RespuestaFacturaDetalle>(consulta);
            }
            

            return respuesta;
        }

        private string EnviaPeticion(string url, string json)
        {
            //todo este codigo actualmente funciona solo es cuestion de convertir los objetos correctamente.
            //adicionalmente es necesario usar metodos async
            //json = "{\"factura\":{\"emisor\":{\"llave_publica\":\"key_f7f99088d457278fa1b059c34f01df5d\",\"llave_privada\":\"key_6b305bf82216f505d826e4c1cf8df5b2\"},\"articulos\":[{\"id\":\"M090\",\"descripcion\":\"Monitor X 70\",\"precio\":\"9000.00\",\"cantidad\":\"4\",\"total\":\"36000.00\"}],\"receptor\":{\"nombre\":\"Paramount Permanent Mountain\",\"rfc\":\"PAPM930101816\",\"telefono\":\"+52 (664) 2183049\",\"direccion\":{\"calle\":\"Homero\",\"exterior\":\"480\",\"estado\":\"Distrito Federal\",\"ciudad\":\"Milpa Alta\",\"codigo_postal\":\"11560\",\"correo\":\"entradas\",\"nombre_contacto\":\"Miguez Hernadez\",\"interior\":\"string\",\"colonia\":\"Chapultepec Morales\"}},\"opciones\":{\"tipo_factura\":\"ingreso\",\"moneda\":\"MXN\",\"pago\":\"Efectivo\",\"forma_pago\":\"Pago en una sola exhibicion\"},\"totales\":{\"monto\":\"36000.00\",\"agregado\":\"16.00\",\"iva\":\"5760.00\",\"isr\":\"0.00\",\"riva\":\"0.00\",\"total\":\"14760.00\"}}}";
            string jsoncancelacion = "{\"llaves\":{\"llave_publica\":\"key_f7f99088d457278fa1b059c34f01df5d\",\"llave_privada\":\"key_6b305bf82216f505d826e4c1cf8df5b2\"},\"rfc\":\"PAPM930101816\",\"UUID\":\"d9e27da0-e539-4dd4-a0e8-93e1bd64d07f\"}";
            var cliente = new WebClient();
            cliente.Headers[HttpRequestHeader.ContentType] = "application/json";
            string respuesta = cliente.UploadString(url + "/api/1.0/factura", json);
            string cancelacion = cliente.UploadString(url + "/api/1.0/factura", "DELETE", jsoncancelacion);
           
            string consulta = cliente.DownloadString(url +
                string.Format("/api/1.0/factura?folio={0}&llave_privada={1}&llave_publica={2}", "21212112", "key_6b305bf82216f505d826e4c1cf8df5b2", "key_f7f99088d457278fa1b059c34f01df5d"));
            return string.Empty;
        }

        private static async Task RealizaPeticion(string url, Peticion peticion)
        {
            //http://stackoverflow.com/questions/6178918/silverlight-4-0-using-webclient-uploadstringasync-post-request-to-net-4-webser
            string json = JsonConvert.SerializeObject(peticion);
            //json = "{\"factura\":{\"emisor\":{\"llave_publica\":\"key_f7f99088d457278fa1b059c34f01df5d\",\"llave_privada\":\"key_6b305bf82216f505d826e4c1cf8df5b2\"},\"articulos\":[{\"id\":\"M090\",\"descripcion\":\"Monitor X 70\",\"precio\":\"9000.00\",\"cantidad\":\"4\",\"total\":\"36000.00\"}],\"receptor\":{\"nombre\":\"Paramount Permanent Mountain\",\"rfc\":\"PAPM930101816\",\"telefono\":\"+52 (664) 2183049\",\"direccion\":{\"calle\":\"Homero\",\"exterior\":\"480\",\"estado\":\"Distrito Federal\",\"ciudad\":\"Milpa Alta\",\"codigo_postal\":\"11560\",\"correo\":\"entradas\",\"nombre_contacto\":\"Miguez Hernadez\",\"interior\":\"string\",\"colonia\":\"Chapultepec Morales\"}},\"opciones\":{\"tipo_factura\":\"ingreso\",\"moneda\":\"MXN\",\"pago\":\"Efectivo\",\"forma_pago\":\"Pago en una sola exhibicion\"},\"totales\":{\"monto\":\"36000.00\",\"agregado\":\"16.00\",\"iva\":\"5760.00\",\"isr\":\"0.00\",\"riva\":\"0.00\",\"total\":\"14760.00\"}}}";
            //string json = "{\"factura\":{\"emisor\":{\"llave_publica\":\"key_f7f99088d457278fa1b059c34f01df5d\",\"llave_privada\":\"key_6b305bf82216f505d826e4c1cf8df5b2\"},\"articulos\":[{\"id\":\"M090\",\"descripcion\":\"Monitor X 70\",\"precio\":\"9000.00\",\"cantidad\":\"4\",\"total\":\"36000.00\"}],\"receptor\":{\"nombre\":\"Paramount Permanent Mountain\",\"rfc\":\"PAPM930101816\",\"telefono\":\"+52 (664) 2183049\",\"direccion\":{\"calle\":\"Homero\",\"exterior\":\"480\",\"estado\":\"Distrito Federal\",\"ciudad\":\"Milpa Alta\",\"codigo_postal\":\"11560\",\"correo\":\"entradas\",\"nombre_contacto\":\"Miguez Hernadez\",\"interior\":\"string\",\"colonia\":\"Chapultepec Morales\"}},\"opciones\":{\"tipo_factura\":\"ingreso\",\"moneda\":\"MXN\",\"pago\":\"Efectivo\",\"forma_pago\":\"Pago en una sola exhibicion\"},\"totales\":{\"monto\":\"36000.00\",\"agregado\":\"16.00\",\"iva\":\"5760.00\",\"isr\":\"0.00\",\"riva\":\"0.00\",\"total\":\"14760.00\"}}}";
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(url);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                

                HttpResponseMessage res = await cliente.PostAsJsonAsync("/api/1.0/factura", json);
               // cliente.pa
                var respuesta_server = res.Content.ReadAsStringAsync();
                string respuestafinal = respuesta_server.Result;
                
            
           
                if (res.IsSuccessStatusCode)
                {
                    var respuesta = res.RequestMessage;
                    int x = 0;
                }
                
            }
        }

        private class Peticion
        {
            public Peticion()
            {

            }

            public Factura factura { get; set; }

            public Cancelacion cancelacion { get; set; }

            public FacturaDetalle facturaDetalle { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apisat.mx.Elementos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace apisat.mx
{
    public class Apisat
    {
        public Apisat()
        {
            this.factura = new Factura();
        }

        public Apisat(string rutabase)
        {
            this.factura = new Factura();
            this.url = rutabase;
        }

        public string url { get; set; }

        public Factura factura { get; set; }

        public Respuesta Timbrar()
        {
            Respuesta respuesta = new Respuesta();
            if (this.factura.Validar())
            {
                Peticion peticion = new Peticion();
                peticion.factura = this.factura;
                //probar con un diccionario a ver si le pone la palabra factura al inicio para que sea aceptado
                //o crear un objeto aqui adentro para que lo haga
                string json = JsonConvert.SerializeObject(peticion);

                //RealizaPeticion(this.url, json).Wait();
                EnviaPeticion(this.url, json);
            }
            else
            {
                throw new Exception("Faltan algunos atributos");
            }
            
            
            return respuesta;
        }

        private string EnviaPeticion(string url, string json)
        {
            //string para hacerlo funcionar 
            //json = "{\"factura\":{\"emisor\":{\"llave_publica\":\"key_f7f99088d457278fa1b059c34f01df5d\",\"llave_privada\":\"key_6b305bf82216f505d826e4c1cf8df5b2\"},\"articulos\":[{\"id\":\"M090\",\"descripcion\":\"Monitor X 70\",\"precio\":\"9000.00\",\"cantidad\":\"4\",\"total\":\"36000.00\"}],\"receptor\":{\"nombre\":\"Paramount Permanent Mountain\",\"rfc\":\"PAPM930101816\",\"telefono\":\"+52 (664) 2183049\",\"direccion\":{\"calle\":\"Homero\",\"exterior\":\"480\",\"estado\":\"Distrito Federal\",\"ciudad\":\"Milpa Alta\",\"codigo_postal\":\"11560\",\"correo\":\"entradas\",\"nombre_contacto\":\"Miguez Hernadez\",\"interior\":\"string\",\"colonia\":\"Chapultepec Morales\"}},\"opciones\":{\"tipo_factura\":\"ingreso\",\"moneda\":\"MXN\",\"pago\":\"Efectivo\",\"forma_pago\":\"Pago en una sola exhibicion\"},\"totales\":{\"monto\":\"36000.00\",\"agregado\":\"16.00\",\"iva\":\"5760.00\",\"isr\":\"0.00\",\"riva\":\"0.00\",\"total\":\"14760.00\"}}}";
            var cliente = new WebClient();
            cliente.Headers[HttpRequestHeader.ContentType] = "application/json";
            string respuesta = cliente.UploadString(url + "/api/1.0/factura", json);
            return string.Empty;
        }

        private static async Task RealizaPeticion(string url, string json)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(url);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await cliente.PostAsJsonAsync("/api/1.0/factura/timbrar", json);
                var jaja =  cliente.PostAsJsonAsync("/api/1.0/factura/timbrar", json).Result;
                var z = res.Content;
                
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
        }
    }
}

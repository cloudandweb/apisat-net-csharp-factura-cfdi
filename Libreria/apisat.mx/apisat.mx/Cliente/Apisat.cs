using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apisat.mx.Modelos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;
using apisat.mx.Respuestas;

namespace apisat.mx.Cliente
{
    public partial class Apisat : ApiLlaves
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

        public RespuestaTimbrado Timbrar()
        {
            //Estas lineas son temporales mientras se estandariza todo;
            this.factura.emisor.llave_publica = this.llave_publica;
            this.factura.emisor.llave_privada = this.llave_privada;
            //=======================================================

            RespuestaTimbrado respuesta = new RespuestaTimbrado();
            if (this.factura.ValidaObjeto() && Validar())
                respuesta = creaCFDI();
            else
                throw new Exception("Faltan algunos atributos");
            return respuesta;
        }

        public RespuestaCancelacion Cancelar() 
        {
            RespuestaCancelacion respuesta = new RespuestaCancelacion();
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

        private RespuestaTimbrado creaCFDI()
        {
            string json = JsonConvert.SerializeObject(this.peticionTimbre);
            RespuestaTimbrado respuesta = new RespuestaTimbrado();
            using (var cliente = new WebClient())
            {
                cliente.Headers[HttpRequestHeader.ContentType] = "application/json";
                cliente.Headers["datos"] = json;
                
                string json_respuesta = cliente.UploadString(new Uri(string.Format("{0}{1}", this.url, this.CFDIUrn)) , json);
                respuesta = JsonConvert.DeserializeObject<RespuestaTimbrado>(json_respuesta);
            }

            return respuesta;
        }

        private RespuestaCancelacion cancelaCFDI()
        {
            RespuestaCancelacion respuesta = new RespuestaCancelacion();
            using (var cliente = new WebClient()) 
            {
                cliente.Headers[HttpRequestHeader.ContentType] = "application/json";
                cliente.Headers.Add("llave_publica", this.cancelacion.llaves.llave_publica);
                cliente.Headers.Add("llave_privada", this.cancelacion.llaves.llave_privada);
                cliente.Headers.Add("rfc", this.cancelacion.rfc);

                string json_respuesta = cliente.UploadString(new Uri(string.Format("{0}{1}/{2}", this.url, this.CFDIUrn, this.cancelacion.uuid)), "DELETE", string.Empty);
                respuesta = JsonConvert.DeserializeObject<RespuestaCancelacion>(json_respuesta);
            }
            return respuesta;
        }

        private RespuestaFacturaDetalle detalleCFDI()
        {
            RespuestaFacturaDetalle respuesta = new RespuestaFacturaDetalle();
            using(var cliente = new WebClient()) {
                cliente.Headers.Add("llave_publica", this.llave_publica);
                cliente.Headers.Add("llave_privada", this.llave_privada);
              string consulta = cliente.DownloadString(url +
              string.Format("/api/1.0/factura/{0}", this.detalle.uuid));

              respuesta = JsonConvert.DeserializeObject<RespuestaFacturaDetalle>(consulta);
            }
            

            return respuesta;
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

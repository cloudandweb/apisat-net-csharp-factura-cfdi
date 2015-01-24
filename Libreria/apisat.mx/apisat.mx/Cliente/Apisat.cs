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
using System.Security.Cryptography;
using System.Text;
using System.IO;

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
            this.TimbrarFormatoHash = "post@sandbox.apisat.mx/api/1.0/factura@http@{0}@{1}";
            this.CancelarFormatoHash = "delete@sandbox.apisat.mx/api/1.0/factura/{uuid}@http@{0}@{1}";
            this.ConsultaHash = "get@sandbox.apisat.mx/api/1.0/factura/{uuid}@http@{0}@{1}";
           
        }

        public string url { get; set; }

        public string CFDIUrn
        {
            get;
            set;
        }

        public string TimbrarFormatoHash { get; set; }

        public string CancelarFormatoHash { get; set; }

        public string ConsultaHash { get; set; }
         
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
                cliente.Headers["llave_publica"] = this.llave_publica;
                string ts = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss+00:00");
                cliente.Headers["timestamp"] = ts; 
                string hashString = string.Format(this.TimbrarFormatoHash, ts, llave_publica);
                cliente.Headers["llave_privada"] = HMAC256SELLO(hashString, ts, this.llave_publica, this.llave_privada);
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
                cliente.Headers["llave_publica"] = this.llave_publica;
                string ts = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss+00:00");
                cliente.Headers["timestamp"] = ts;
                string cancelhash = this.CancelarFormatoHash.Replace("{0}", ts).Replace("{1}", this.llave_publica);
                cliente.Headers["llave_privada"] = HMAC256SELLO(cancelhash, ts, this.llave_publica, this.llave_privada);
                cliente.Headers["rfc"] = this.cancelacion.rfc;
                string otro = string.Format(this.TimbrarFormatoHash, ts, llave_publica);
                string algo = string.Format("{0}{1}/{2}", this.url, this.CFDIUrn, this.cancelacion.uuid);

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
                string ts = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss+00:00");
                cliente.Headers["timestamp"] = ts;
                string hashString = this.ConsultaHash.Replace("{0}", ts).Replace("{1}", this.llave_publica);
                cliente.Headers["llave_privada"] = HMAC256SELLO(hashString, ts, this.llave_publica, this.llave_privada);
              string consulta = cliente.DownloadString(url +
              string.Format("/api/1.0/factura/{0}", this.detalle.uuid));

              respuesta = JsonConvert.DeserializeObject<RespuestaFacturaDetalle>(consulta);
            }
            

            return respuesta;
        }


        private string HMAC256SELLO(string FormatoHash, string TimeStamp, string llave_publica, string llave_privada)
        {
            HMACSHA256 hmac = new HMACSHA256();
            hmac.Key = Encoding.ASCII.GetBytes(llave_privada);
            byte[] byteArray = Encoding.ASCII.GetBytes(FormatoHash);
            byte[] hashValue = hmac.ComputeHash(byteArray);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                sb.Append(hashValue[i].ToString("x2"));
            }
            string hvalue = sb.ToString();
            byte[] nuevo = Encoding.UTF8.GetBytes(hvalue);

            return Convert.ToBase64String(nuevo);
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

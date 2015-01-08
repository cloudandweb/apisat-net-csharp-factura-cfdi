﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Elementos
{
    public  class Totales
    {

        public double monto { get; set; }

        public double agregado { get; set; }

        [JsonIgnore]
        public double iva { get; set; }

        [JsonIgnore]
        public double isr { get; set; }

        [JsonIgnore]
        public double riva { get; set; }

        public double total { get; set; }

        [JsonProperty("isr")]
        public string isr_serializacion {
            get { return isr.ToString("F2"); } 
        }

        public void Calcula()
        {
            
        }

        [JsonProperty("iva")]
        public string iva_serializacion
        {
            get { return iva.ToString("F2"); }
        }

        [JsonProperty("riva")]
        public string riva_serializacion
        {
            get { return riva.ToString("F2"); }
        }



       
    }
}

﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisat.mx.Modelos
{
    public partial class Cancelacion
    {

        public Cancelacion() 
        {

        }

        public string rfc { get; set; }

        [JsonProperty("UUID")]
        public string uuid { get; set; }

    }
}

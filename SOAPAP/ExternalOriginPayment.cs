﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class ExternalOriginPayment
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isBank { get; set; }
        public bool isActive { get; set; }
    }
}

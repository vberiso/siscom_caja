﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class TransactionVM
    {
        public Transaction transaction { get; set; }
        public SOAPAP.Model.Payment payment { get; set; }
    }
}
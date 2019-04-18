using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SOAPAP.Model
{
    public class ServiceParam
    {
        public int Id { get; set; }
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public string UnitMeasurement { get; set; }
        public bool IsActive { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}

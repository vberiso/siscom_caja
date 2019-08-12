using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class AgreementComent
    {
        public int Id { get; set; }

        public DateTime DateIn { get; set; }

        public string Observation { get; set; }

        public bool IsVisible { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime DateOut { get; set; }

        public int AgreementId { get; set; }

        public Agreement Agreement { get; set; }
    }
}

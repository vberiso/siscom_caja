using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    class ClientFinding
    {
        public int Id_Client { get; set; }
        public int Id_TaxUser { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string Cuenta { get; set; }
        public string Street { get; set; }
        public string Outdoor { get; set; }
        public string Indoor { get; set; }
        public string Zip { get; set; }
        public string Referencia { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string type_address { get; set;}
        public string DisplayData
        {
            get
            {
                return string.Format("Cuenta: {0}, Usuario: {6}, Dirección: {1} {2}, {3} {4}, {5}", Cuenta, Street, Outdoor, Colonia, Zip, Municipio, Nombre);
            }
        }
    }
}

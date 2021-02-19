using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DebtCampaign
    {        
        public int Id { get; set; }       
        public int Ruta { get; set; }       
        public int AgreementId { get; set; }        
        public string Account { get; set; }        
        public string StartYearDebt { get; set; }       
        public string EndYearDebt { get; set; }       
        public decimal Importe { get; set; }       
        public decimal Iva { get; set; }       
        public decimal Total { get; set; }       
        public decimal TotalAgua { get; set; }       
        public decimal TotalDrenaje { get; set; }        
        public decimal TotalSaneamiento { get; set; }       
        public string Status { get; set; }       
        public string Folio { get; set; }       
        public DateTime DateSubscription { get; set; }       
        public string DebtId { get; set; }       
        public string Servicios { get; set; }       
        public string Consumo { get; set; }       
        public decimal ImporteMultas { get; set; }       
        public decimal ImporteRecargo { get; set; }        
        public decimal ImporteNotificaciones { get; set; }        
        public decimal DescuentoMulta { get; set; }        
        public decimal DescuentoRecargo { get; set; }        
        public decimal DescuentoNotificaciones { get; set; }       
        public decimal TaotalDescuentoServicios { get; set; }        
        public decimal ServiciosAdeltados { get; set; }
        public Agreement Agreement { get; set; }
    }
}

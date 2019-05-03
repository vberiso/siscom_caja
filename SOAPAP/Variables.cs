using SOAPAP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Variables
    {
        public static LoginModel LoginModel { get; set; }
        public static Configuration Configuration {get; set;}
        public static Model.Agreement Agreement { get; set; }
        public static OrderSale OrderSale { get; set; }
        public static string titleprincipal { get; set; } = "SISCOM";
        public static string cuenta { get; set; } = string.Empty;
        public static int  metododepago { get; set; } = 0;
        public static bool sitieneiva { get; set; } = false;
        public static List<Deb> debst { get; set; } = new List<Deb>();
        public static List<string> keys = new List<string>();
        public static string foliotransaccion = string.Empty;
        public static string foliocaja = string.Empty;
        public static decimal subtotalp = 0;
        public static decimal ivap = 0;
        public static decimal redondeop = 0;
        public static decimal totalp = 0;
        public static string metododepagop = string.Empty;
        public static string idagrement = string.Empty;
        public static decimal efectivo = 0;
        public static decimal cheques = 0;
        public static decimal tarjetas = 0;
        public static decimal otros = 0;
        public static int anticipo = 0;
        public static int idtransaction = 0;
        public static string imagen = string.Empty;
        public static string IsMunicipal = string.Empty;
        public static DataTable pagos = new DataTable();
        public static DataTable datospadron = new DataTable();
        public static DataTable totals = new DataTable();
        public static DataTable datosgenerales = new DataTable();
        public static DataTable ImagenData = new DataTable();
        public static DataTable Foliotiket = new DataTable();
        public static DataTable Productos = new DataTable();
        public static string divition { get; set; } = string.Empty;
        public static bool oprtions { get; set; } = false;
        public static int optionvistaimpresion { get; set; } = 0;
    }
}
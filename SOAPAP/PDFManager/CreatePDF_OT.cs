using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.PDFManager
{
    public class CreatePDF_OT
    {
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        DialogResult result = new DialogResult();
        Form mensaje;

        public CreatePDF_OT()
        {

        }

        //public async Task<string> CreateReconexion(int idOrderWork)
        //{

        //}
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Model.Discounts;
using SOAPAP.Services;
using SOAPAP.UI.FacturacionAnticipada;

namespace SOAPAP.UI.Promos
{
    public partial class BtnPromotion : UserControl
    {
        public Promotions Promocion { get; set; }
        //private Model.Agreement Agreement;

        Form mensaje;
        Form loading;
        private RequestsAPI Requests = null;
        DialogResult result = new DialogResult();

        public BtnPromotion()
        {            
            InitializeComponent();
            Requests = new RequestsAPI(Properties.Settings.Default.URL);
        }

        public BtnPromotion(Promotions promotions)
        {            
            InitializeComponent();
            Requests = new RequestsAPI(Properties.Settings.Default.URL);
            this.Promocion = promotions;
            //this.Agreement = Agree;
        }

        private void BtnPromotion_Load(object sender, EventArgs e)
        {
            lblTextoPublico.Text = Promocion != null ? Promocion.NombrePublico : "Promoción";
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (Promocion != null)
            {
                string prefijo = this.Promocion.Nombre.Substring(0, 3);
                switch (prefijo)
                {
                    //case "ADD":
                    //    break;
                    case "CDN":
                    case "DSC":
                        AplicarDescuentos();
                        break;
                    case "ANL":
                        AplicarAnual();
                        break;
                    case "MXT":
                        AplicarMixto();
                        break;
                }
            }
        }

        private void agregarCintllo()
        {

        }

        private async void AplicarDescuentos()
        {
            loading = new Loading();
            loading.Show(this);
            string ruta = "";

            if (Promocion.Nombre.Contains("CDN"))     //Condonacion
            {
                ruta = string.Format("/api/CondonationCampaing/CondonationPromotion/{0}/{1}?us={2}&usName={3}", Variables.Agreement.Id, Promocion.Id, Variables.LoginModel.User, Variables.LoginModel.FullName);
            }
            else if (Promocion.Nombre.Contains("DSC"))   //Descuento
            {
                ruta = string.Format("/api/CondonationCampaing/DiscountPromotion/{0}/{1}", Variables.Agreement.Id, Promocion.Id);
            }

            var resultCampaign = await Requests.SendURIAsync(ruta, HttpMethod.Post, Variables.LoginModel.Token);
            if (resultCampaign.Contains("error\":"))
            {
                try
                {
                    mensaje = new MessageBoxForm("Promocion NO aplicada", JsonConvert.DeserializeObject<Error>(resultCampaign).error, TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    loading.Close();
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    loading.Close();
                }
            }
            else
            {

                mensaje = new MessageBoxForm(Variables.titleprincipal, "La condonación de recargos se ha realizado con exito", TypeIcon.Icon.Success);
                result = mensaje.ShowDialog();
                loading.Close();
                OnChanged(null);
            }
        }

        private async void AplicarAnual()
        {
            if (Variables.Agreement != null)
            {
                if (Variables.Configuration.IsMunicipal)
                {
                    var Uiperiodos = new PagosAnualesAyuntamiento(Variables.Agreement);
                    var result = Uiperiodos.ShowDialog(this);
                    Uiperiodos.Close();
                }
                else
                {
                    Variables.Configuration.Anual = true;
                    int ultimoPeriodo = await ValidaUltimoPeriodoDePago(Promocion.PromocionAño);
                    var Uiperiodos = new PeriodosAnticipados(Variables.Agreement, true, (ultimoPeriodo > 0 ? ultimoPeriodo : 0));
                    var result = Uiperiodos.ShowDialog(this);
                    Uiperiodos.Close();

                    if (result == DialogResult.OK)
                    {
                        OnChanged(null);
                    }
                }
            }
        }

        private async Task<int> ValidaUltimoPeriodoDePago(int año)
        {
            var resultDebt = await Requests.SendURIAsync(string.Format("/api/Debts/debtPaid?idAgreement={0}&year={1}", Variables.Agreement.Id, año), HttpMethod.Get, Variables.LoginModel.Token);

            if(loading != null)
                loading.Close();

            if (resultDebt.Contains("error\\"))
            {
                return 0;
            }
            else
            {
                List<Debt> debts = JsonConvert.DeserializeObject<List<Debt>>(resultDebt);
                if (debts != null && debts.Count > 0)
                {
                    return debts.Count();
                }
                return 0;
            }
        }

        private async void AplicarMixto()
        {
            try
            {
                loading = new Loading();
                loading.Show(this);

                var definitionCondonacion = new { condonado = 0.0M };
                var definitionDescuento = new { descuento = 0.0M };
                decimal condonacion = 0;
                decimal descuento = 0;
                
                string ruta = "";
                //Condonacion
                ruta = string.Format("/api/CondonationCampaing/CondonationPromotion/{0}/{1}?us={2}&usName={3}", Variables.Agreement.Id, Promocion.Id, Variables.LoginModel.User, Variables.LoginModel.FullName);
                var resultCondonacion = await Requests.SendURIAsync(ruta, HttpMethod.Post, Variables.LoginModel.Token);
                if (resultCondonacion.Contains("error\":"))
                {
                    try
                    {
                        mensaje = new MessageBoxForm("Promocion NO aplicada", JsonConvert.DeserializeObject<Error>(resultCondonacion).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        loading.Close();
                        return;
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        loading.Close();
                        return;
                    }
                }
                else
                {
                    var tmpCondonacion = JsonConvert.DeserializeAnonymousType(resultCondonacion, definitionCondonacion);
                    condonacion = tmpCondonacion.condonado;
                }

                //Descuento
                ruta = string.Format("/api/CondonationCampaing/DiscountPromotion/{0}/{1}", Variables.Agreement.Id, Promocion.Id);
                var resultDescuento = await Requests.SendURIAsync(ruta, HttpMethod.Post, Variables.LoginModel.Token);
                if (resultDescuento.Contains("error\":"))
                {
                    try
                    {
                        mensaje = new MessageBoxForm("Promocion NO aplicada", JsonConvert.DeserializeObject<Error>(resultDescuento).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        loading.Close();
                        return;
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        loading.Close();
                        return;
                    }
                }
                else
                {
                    var tmpDescuento = JsonConvert.DeserializeAnonymousType(resultDescuento, definitionDescuento);
                    descuento = tmpDescuento.descuento;
                }

                //Anual
                if (Variables.Agreement != null)
                {
                    if (Variables.Configuration.IsMunicipal)
                    {
                        var Uiperiodos = new PagosAnualesAyuntamiento(Variables.Agreement);
                        var result = Uiperiodos.ShowDialog(this);
                        Uiperiodos.Close();
                    }
                    else
                    {
                        Variables.Configuration.Anual = true;
                        Promotions tmpPromocion = Variables.Configuration.anualDiscount;
                        Variables.Configuration.anualDiscount = Promocion;

                        int ultimoPeriodo = await ValidaUltimoPeriodoDePago(Promocion.PromocionAño);
                        var Uiperiodos = new PeriodosAnticipados(Variables.Agreement, true, (ultimoPeriodo > 0 ? ultimoPeriodo : 0));
                        var result = await Uiperiodos.generarDeudaAdelantada();
                        Uiperiodos.Close();

                        Variables.Configuration.anualDiscount = tmpPromocion;
                    }
                }

                //Se registra la cuenta beneficiada
                ruta = string.Format("/api/CondonationCampaing/RegisterBenefitedAccount/{0}/{1}/{2}", Variables.Agreement.Id, Promocion.Id, (condonacion + descuento));
                var resultRegister = await Requests.SendURIAsync(ruta, HttpMethod.Post, Variables.LoginModel.Token);
                if (resultDescuento.Contains("error\":"))
                {
                    mensaje = new MessageBoxForm("Promocion NO aplicada", JsonConvert.DeserializeObject<Error>(resultDescuento).error, TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    loading.Close();
                }
                

                loading.Close();
                OnChanged(null);
            }
            catch (Exception e)
            {
                loading.Close();                
            }
        }


        public event EventHandler Changed;
        protected virtual void OnChanged(EventArgs e)
        {
            EventHandler eh = Changed;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        //Al dar click en el icono del boton se muestra la descripcion de la promoción.
        private void button1_Click(object sender, EventArgs e)
        {
            mensaje = new MessageBoxForm(this.Promocion.NombrePublico, this.Promocion.DescripcionPublico, TypeIcon.Icon.Info);
            var resultMensaje = mensaje.ShowDialog();
        }
    }
}

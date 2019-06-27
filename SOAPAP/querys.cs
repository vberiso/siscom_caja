using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Services;
using SOAPAP.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SOAPAP
{
    class querys 
    {

        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
     

        public querys()
        {
            Requests = new RequestsAPI(UrlBase);
        }
        ////////////////////////////////////////////ENDPOINT///////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        
        public async Task<DataTable> GETBranchOffice(string url)
        {

            DataTable dt = new DataTable();
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
                dt = (DataTable)JsonConvert.DeserializeObject(resultado, (typeof(DataTable)));
            }

            catch (Exception)
            {
                
            }
            
            return dt;
        }

        public async Task<string> POSTTerminal(string url, string macadress, string cashbox, string isactive, string branchoffice)
        {
            string retorno = string.Empty;
            HttpContent content;
            string json = "{\"macAdress\": \"" + macadress + "\", \"branchOffice\": \"" + branchoffice + "\"}";
            content = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);
            if (resultado.Contains("error"))
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                retorno = "error/" + m.error;
            }
            else
            {
                retorno = "Exito";
            }

                
            return retorno;

        }

        public async Task<string> POSTTransaction(string url, string folio, string sign, string amounts, string aplications, string typeTransactionId,string payMethodId,string terminalUserId,string codeConcept,string description,string amount)
        {
            string retorno = string.Empty;
            string WEBSERVICE_URL = url;
            string json = string.Empty;
            HttpContent content;
            json = "{\"sign\": \"" + sign + "\", \"amount\": \"" + amounts + "\",\"total\": \"" + amounts + "\", \"aplication\": \"" + aplications + "\", \"typeTransactionId\": \"" + typeTransactionId + "\", \"payMethodId\": \"" + payMethodId + "\", \"terminalUserId\": \"" + terminalUserId + "\"}";
            content = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);
            if (resultado.Contains("error"))
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                retorno = "error/" + m.error;
            }
            else
            {
                retorno = "Exito";
            }
            return retorno;
        }

        public async Task<DataTable> GETPaymentHistory(string url)
        {
            DataTable dt = new DataTable();
            List<pyment> ms = new List<pyment>();
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "debitDate";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "amount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "typeIntake";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "typeService";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "fromDate";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "status";
            dt.Columns.Add(column);



            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
                 ms = JsonConvert.DeserializeObject<List<pyment>>(resultado);
                 foreach (var r in ms.ToList())
                {
                    DataRow row = dt.NewRow();
                    row["debitDate"] = r.debitDate;
                    row["amount"] = r.amount;
                    row["typeIntake"] = r.typeIntake;
                    row["typeService"] = r.typeService;
                    row["fromDate"] = r.fromDate;
                    row["status"] = r.status;
                    


                    dt.Rows.Add(row);
                }

                
            }
            catch (Exception es)
            {

                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);

            }

            return dt;
        }

        public async Task<DataTable> GETAgreementsbyaccount(string url)
        {
            DataTable dt = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Cuenta";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Names";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "RFC";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Direccion";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "numDerivatives";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "typeService";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "typeConsume";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "typeRegime";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "typePeriod";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "typeStateService";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "typeIntake";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "taxableBase";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ground";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "built";
            dt.Columns.Add(column);

            string id = string.Empty;
            string cuenta = string.Empty;
            string names = string.Empty;
            string direccion = string.Empty;
            string rfc = string.Empty;
            string numDerivatives = string.Empty;
            string typeService = string.Empty;
            string typeConsume = string.Empty;
            string typeRegime = string.Empty;
            string typePeriod = string.Empty;
            string typeStateService = string.Empty;
            string typeIntake = string.Empty;
            string taxableBase = string.Empty;
            string ground = string.Empty;
            string built = string.Empty;

            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
                Agreement ms = JsonConvert.DeserializeObject<Agreement>(resultado);
                id = ms.id;
                cuenta = ms.account;

                try
                {
                    names = ms.Clients.FirstOrDefault(x => x.typeUser == "CLI01").name + " " + ms.Clients.FirstOrDefault(x => x.typeUser == "CLI01").lastName + " " + ms.Clients.FirstOrDefault(x => x.typeUser == "CLI01").secondLastName;
                }
                catch (Exception)
                {
                    names = "";
                }

                try
                {
                    rfc = ms.Clients.FirstOrDefault(x => x.typeUser == "CLI01").rfc;
                }
                catch (Exception)
                {
                    rfc = "";
                }

                try
                {
                    direccion = ms.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Street + " " + ms.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Name + " " + ms.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.name + " " + ms.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.name + " " + ms.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.countries.name;
                }
                catch (Exception)
                {

                    direccion = "";
                }

                numDerivatives = ms.numDerivatives.ToString();
                typeService = ms.TypeService.Name.ToString();
                typeConsume = ms.TypeConsume.name.ToString();
                typeRegime = ms.TypeRegime.name.ToString();
                typePeriod = ms.TypePeriod.name.ToString();
                typeStateService = ms.TypeStateService.name.ToString();
                typeIntake = ms.TypeIntake.name.ToString();

                foreach (var r in ms.agreementDetails)
                {

                    taxableBase = r.taxableBase.ToString();
                    ground = r.ground.ToString();
                    built = r.built.ToString();

                }

                DataRow row = dt.NewRow();
                row["ID"] = id;
                row["Cuenta"] = cuenta;
                row["Names"] = names;
                row["RFC"] = rfc;
                row["Direccion"] = direccion;
                row["numDerivatives"] = numDerivatives;
                row["typeService"] = typeService;
                row["typeConsume"] = typeConsume;
                row["typeRegime"] = typeRegime;
                row["typePeriod"] = typePeriod;
                row["typeStateService"] = typeStateService;
                row["typeIntake"] = typeIntake;
                row["taxableBase"] = taxableBase;
                row["ground"] = ground;
                row["built"] = built;
                dt.Rows.Add(row);
            }
            catch (Exception)
            {

                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr["ID"] = "error/" + m.error;
                dt.Rows.Add(dr);

            }

            return dt;  
        }
  
        public async Task<DataTable> GETAgreementsFindAgreement(string url)
        {
            DataTable dt = new DataTable();
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
                dt = (DataTable)JsonConvert.DeserializeObject(resultado, (typeof(DataTable)));
            }
            catch (Exception)
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataColumn dc = new DataColumn("ID", typeof(String));
                dt.Columns.Add(dc);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);
            }
            
            return dt;

        }

        public async Task<DataTable> GETTransaction(string url)
        {
            DataTable dt = new DataTable();
            string json = string.Empty;

            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Id";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Folio";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "dateTransaction";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "sign";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "amount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "tax";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "rounding";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "total";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "aplication";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "cancellationFolio";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "authorizationOriginPayment";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "terminalUser";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idterminalUser";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nameterminalUser";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idpayMethod";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "namepayMethod";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "originPayment";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "externalOriginPayment";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "foliotrans";
            dt.Columns.Add(column);


            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "typeTransactionId";
            dt.Columns.Add(column);
            
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
            List<Transaction> m = JsonConvert.DeserializeObject<List<Transaction>>(resultado);

                            decimal postivoefec = 0;
                            decimal negativoefec = 0;
                            decimal postivocheques = 0;
                            decimal negativocheques = 0;
                            decimal postivotar = 0;
                            decimal negativotar = 0;
                            decimal postivootros = 0;
                            decimal negativootros = 0;


                            for (int i = 0; i < m.Count; i++)
                            {
                                
                                if (m[i].payMethod.id==1 && m[i].sign ==true && m[i].typeTransactionId != 1 && m[i].typeTransactionId != 2 && m[i].typeTransactionId != 5 && m[i].typeTransactionId != 6 && m[i].typeTransactionId != 7)
                                {

                                    postivoefec = postivoefec + m[i].total;
                                }

                                else if (m[i].payMethod.id == 1 && m[i].sign == false && m[i].typeTransactionId != 1 && m[i].typeTransactionId != 2 && m[i].typeTransactionId != 5  && m[i].typeTransactionId != 7 && m[i].typeTransactionId != 6)
                                {
                                    negativoefec = negativoefec + m[i].total;
                                }

                                else if (m[i].payMethod.id == 2 && m[i].sign == true && m[i].typeTransactionId != 1 && m[i].typeTransactionId != 2 && m[i].typeTransactionId != 5 && m[i].typeTransactionId != 6 && m[i].typeTransactionId != 7)
                                {
                                    postivocheques = postivocheques + m[i].total;
                                }

                                else if (m[i].payMethod.id == 2 && m[i].sign == false && m[i].typeTransactionId != 1 && m[i].typeTransactionId != 2 && m[i].typeTransactionId != 5  && m[i].typeTransactionId != 7 && m[i].typeTransactionId != 6) 
                                {
                                    negativocheques = negativocheques + m[i].total;
                                }

                                else if (m[i].payMethod.id == 4 && m[i].sign == true && m[i].typeTransactionId != 1 && m[i].typeTransactionId != 2 && m[i].typeTransactionId != 5 && m[i].typeTransactionId != 6 && m[i].typeTransactionId != 7)
                                {
                                    postivotar = postivotar + m[i].total;
                                }

                                else if (m[i].payMethod.id == 4 && m[i].sign == false && m[i].typeTransactionId != 1 && m[i].typeTransactionId != 2 && m[i].typeTransactionId != 5  && m[i].typeTransactionId != 7 && m[i].typeTransactionId != 6)
                                {
                                    negativotar = negativotar + m[i].total;
                                }

                                else if (m[i].payMethod.id == 3 && m[i].sign == true && m[i].typeTransactionId != 1 && m[i].typeTransactionId != 2 && m[i].typeTransactionId != 5 && m[i].typeTransactionId != 6 && m[i].typeTransactionId != 7)
                                {
                                    postivootros = postivootros + m[i].total;
                                }

                                else if (m[i].payMethod.id == 3 && m[i].sign == false && m[i].typeTransactionId != 1 && m[i].typeTransactionId != 2 && m[i].typeTransactionId != 5  && m[i].typeTransactionId != 7 && m[i].typeTransactionId != 6)
                                {
                                    negativootros = negativootros + m[i].total;
                                }

                                DataRow row = dt.NewRow();
                                row["Id"] = m[i].id.ToString();
                                try
                                {
                                    row["Folio"] = m[i].transactionFolios.FirstOrDefault().folio;
                                }
                                catch (Exception)
                                {
                                    
                                } 
                                                                
                            row["dateTransaction"] = m[i].dateTransaction.ToString();
                            row["sign"] = m[i].sign.ToString();
                            row["amount"] = m[i].amount.ToString();
                            row["tax"] = m[i].tax.ToString();
                            row["rounding"] = m[i].rounding;
                            row["total"] = m[i].total;
                            row["aplication"] = m[i].aplication;
                            row["cancellationFolio"] = m[i].cancellationFolio;
                            row["authorizationOriginPayment"] = m[i].authorizationOriginPayment;
                            row["idterminalUser"] = m[i].typeTransaction.id.ToString();
                            row["nameterminalUser"] = m[i].typeTransaction.name.ToString();
                            row["idpayMethod"] = m[i].payMethod.id;
                            row["namepayMethod"] = m[i].payMethod.name;
                            row["foliotrans"] = m[i].folio;
                            row["typeTransactionId"] = m[i].typeTransactionId;
                            dt.Rows.Add(row);
                            }

                            Variables.efectivo = postivoefec - negativoefec;
                            Variables.cheques = postivocheques - negativocheques;
                            Variables.tarjetas = postivotar - negativotar;
                            Variables.otros = postivootros - negativootros;
            }
            catch (Exception)
            {

                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public async Task<DataTable> GETDebts(string url)
        {
            DataTable dt = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Id";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "amount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "onAccount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "have_tax";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "code_concept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "name_concept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "debtId";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "deuda";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Year";
            dt.Columns.Add(column);


            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Debperiod";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "TaxResult";
            dt.Columns.Add(column);


            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);

            try
            {


                List<Deb> m = JsonConvert.DeserializeObject<List<Deb>>(resultado);
                List<Debst> ms = JsonConvert.DeserializeObject<List<Debst>>(resultado);
                Variables.debst = m;


                if (Variables.debst.Count != 0)
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        foreach (var item in m[i].debtdetails)
                        {
                            DataRow row = dt.NewRow();
                            row["Id"] = item.id;
                            row["amount"] = item.amount;
                            row["onAccount"] = item.onAccount;
                            row["have_tax"] = item.haveTax;
                            row["code_concept"] = item.codeConcept;
                            row["name_concept"] = item.nameConcept;
                            row["debtId"] = item.debtId.ToString();
                            row["deuda"] = (Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount));
                            row["Year"] = Variables.debst[i].year;
                            row["Debperiod"] = Variables.debst[i].fromDate.ToString("dd-MM-yyyy") + " a " + Variables.debst[i].untilDate.ToString("dd-MM-yyyy");
                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        
        public async  Task<DataTable> GETPayMethod(string url)
        {
            DataTable dt = new DataTable();
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
                dt = (DataTable)JsonConvert.DeserializeObject(resultado, (typeof(DataTable)));
            }
            catch (Exception)
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataColumn dc = new DataColumn("ID", typeof(String));
                dt.Columns.Add(dc);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);

            }
            return dt;
        }

        public async Task<DataTable> GETExternalOriginPayments(string url)
        {
            DataTable dt = new DataTable();
            string json = string.Empty;
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);

            try
            {
            dt = (DataTable)JsonConvert.DeserializeObject(resultado, (typeof(DataTable)));
            }
            catch (Exception)
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataColumn dc = new DataColumn("ID", typeof(String));
                dt.Columns.Add(dc);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public async Task<string> POSTTransactionDebT(string url, string sign, decimal amounts, decimal tax, string percentageTax, decimal rounding, decimal total, string aplications, string typeTransactionId, string payMethodId, string terminalUserId, string authorizationOriginPayment, string originPaymentId, string externalOriginPaymentId, string type, string debtStatus, decimal monto, string acount, DataTable agrupado, string cuenta)
        {
            string retorno = string.Empty;
            HttpContent content;
            string json = string.Empty;
            int t = 0;
            int t1 = 1;
            int q1 = 0;
            int q2 = 1;
            int x1 = 0;
            int x2 = 1;
            decimal redondeo = 0;
            bool sitienevia = false;
            decimal subtotal = 0;
            decimal resultado = 0;
            decimal porcentaje = 0;
            decimal iva = 0;

            
                    json = "{\"transaction\": ";
                    json = json + "{\"sign\": \"" + sign + "\", \"amount\": \"" + amounts + "\", \"account\": \"" + cuenta + "\", \"tax\": \"" + tax + "\", \"percentageTax\": \"" + percentageTax + "\", \"rounding\": \"" + rounding + "\",\"total\": \"" + total + "\", \"aplication\": \"" + aplications + "\", \"typeTransactionId\": \"" + typeTransactionId + "\", \"payMethodId\": \"" + payMethodId + "\", \"terminalUserId\": \"" + terminalUserId + "\", \"authorizationOriginPayment\": \"" + authorizationOriginPayment + "\", \"originPaymentId\": \"" + originPaymentId + "\", \"externalOriginPaymentId\": \"" + externalOriginPaymentId + "\", \"type\": \"" + type + "\", \"paytStatus\": \"" + debtStatus + "\", \"agreementId\": \"" + acount + "\",\"transactionDetails\": [";
                    x1 = agrupado.Rows.Count;

                    foreach (DataRow row in agrupado.Rows)
                    {
                        if (x1 != x2)
                        {
                            json = json + "{\"codeConcept\": \"" + row[4].ToString() + "\",\"description\": \"" + row[5].ToString() + "\",\"amount\": " + row[7].ToString() + "},";
                            x2 = x2 + 1;
                        }
                        else
                        {
                            json = json + "{\"codeConcept\": \"" + row[4].ToString() + "\",\"description\": \"" + row[5].ToString() + "\",\"amount\": " + row[7].ToString() + "}";
                            x2 = 1;
                        }
                    }

                    json = json + "]}";
                    json = json + ",\"debt\":[ ";

                    q1 = Variables.debst.Count;
                    redondeo = Convert.ToDecimal(rounding) / Variables.debst.Count;

                    for (int i = 0; i < Variables.debst.Count; i++)
                    {
                        foreach (var item in Variables.debst[i].debtdetails)
                        {
                            if (item.haveTax == true)
                            {
                                sitienevia = true;
                                resultado = resultado + Convert.ToDecimal(item.amount);
                            }

                            subtotal = subtotal + item.amount;

                        }

                        porcentaje = Convert.ToDecimal(Variables.Configuration.IVA);
                        iva = Convert.ToDecimal(Variables.Configuration.IVA);


                        if (sitienevia == true)
                        {
                            json = json + "{\"id\": \"" + Variables.debst[i].id + "\", \"debitDate\": \"" + Convert.ToDateTime(Variables.debst[i].debitDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"fromDate\": \"" + Convert.ToDateTime(Variables.debst[i].fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"untilDate\": \"" + Convert.ToDateTime(Variables.debst[i].untilDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"derivatives\": \"" + Variables.debst[i].derivatives + "\", \"typeIntake\": \"" + Variables.debst[i].typeIntake + "\", \"typeService\": \"" + Variables.debst[i].typeService + "\", \"consumption\": \"" + Variables.debst[i].consumption + "\", \"discount\": \"" + Variables.debst[i].discount + "\", \"amount\": \"" + Variables.debst[i].amount + "\", \"onAccount\": \"" + ((Convert.ToDecimal(Variables.debst[i].amount) - Convert.ToDecimal(Variables.debst[i].onAccount) * total / monto) + Convert.ToDecimal(Variables.debst[i].onAccount)) + "\", \"year\": \"" + Variables.debst[i].year + "\", \"type\": \"" + Variables.debst[i].type + "\",\"newStatus\": \"ED005\", \"status\": \"" + Variables.debst[i].status + "\", \"debtPeriodId\": \"" + Variables.debst[i].debtPeriodId + "\", \"agreementId\": \"" + Variables.debst[i].agreementId + "\", \"agreement\": \"" + Variables.debst[i].agreement + "\",\"debtDetails\":[";
                        }
                        else
                        {
                            json = json + "{\"id\": \"" + Variables.debst[i].id + "\", \"debitDate\": \"" + Convert.ToDateTime(Variables.debst[i].debitDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"fromDate\": \"" + Convert.ToDateTime(Variables.debst[i].fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"untilDate\": \"" + Convert.ToDateTime(Variables.debst[i].untilDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"derivatives\": \"" + Variables.debst[i].derivatives + "\", \"typeIntake\": \"" + Variables.debst[i].typeIntake + "\", \"typeService\": \"" + Variables.debst[i].typeService + "\", \"consumption\": \"" + Variables.debst[i].consumption + "\", \"discount\": \"" + Variables.debst[i].discount + "\", \"amount\": \"" + Variables.debst[i].amount + "\", \"onAccount\": \"" + (Convert.ToDecimal(Variables.debst[i].amount) - Convert.ToDecimal(Variables.debst[i].onAccount) * total / monto + Convert.ToDecimal(Variables.debst[i].onAccount)) + "\", \"year\": \"" + Variables.debst[i].year + "\", \"type\": \"" + Variables.debst[i].type + "\",\"newStatus\": \"ED005\", \"status\": \"" + Variables.debst[i].status + "\", \"debtPeriodId\": \"" + Variables.debst[i].debtPeriodId + "\", \"agreementId\": \"" + Variables.debst[i].agreementId + "\", \"agreement\": \"" + Variables.debst[i].agreement + "\",\"debtDetails\":[";
                        }

                        t = Variables.debst[i].debtdetails.Count;
                        foreach (var item in Variables.debst[i].debtdetails)
                        {
                            if (t != t1)
                            {

                        if (item.haveTax == true)
                        {
                            decimal s = ((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * iva / 100);  
                            json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto) + Convert.ToDecimal(item.onAccount)) + "\",\"onPayment\": \"" + (((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto)) + "\", \"haveTax\": \"" + item.haveTax + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\",\"tax\": \"" + Math.Round(((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * iva / 100),2) + "\"},";
                            t1 = t1 + 1;
                        }
                        else {
                            json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto) + Convert.ToDecimal(item.onAccount)) + "\",\"onPayment\": \"" + (((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto)) + "\", \"haveTax\": \"" + item.haveTax + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\"},";
                            t1 = t1 + 1;
                        }
                            


                            }
                            else
                            {
                        if (item.haveTax == true)
                        {
                            decimal s = ((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * iva / 100);
                            json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + ((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total) / monto) + Convert.ToDecimal(item.onAccount)) + "\", \"onPayment\": \"" + (((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto)) + "\",\"haveTax\": \"" + item.haveTax + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\",\"tax\": \"" + Math.Round(((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * iva / 100),2) + "\"}";
                            t1 = 1;
                        }
                        else
                        {
                            json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + ((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total) / monto) + Convert.ToDecimal(item.onAccount)) + "\", \"onPayment\": \"" + (((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto)) + "\",\"haveTax\": \"" + item.haveTax + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\"}";
                            t1 = 1;

                        }

                            
                            }
                        }

                        if (q2 != q1)
                        {
                            json = json + "],\"debtStatuses\":[]},";
                            q2 = q2 + 1;
                        }
                        else
                        {
                            json = json + "],\"debtStatuses\":[]}";
                        }

                        sitienevia = false;
                        subtotal = 0;
                        resultado = 0;
                        porcentaje = 0;
                        iva = 0;

                    }

                        json = json + "]}";

                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        var resultados = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);
                         if (resultados.Contains("error"))
                         {
                            Error m = JsonConvert.DeserializeObject<Error>(resultados);
                            retorno = "error/" + m.error;
                         }
                            else
                           
                            {
                                int res = Convert.ToInt32(resultados);
                                Variables.idtransaction = res;
                                //await GETTransactionID("/api/Transaction/" + res.ToString());
                                retorno = "Exito";
                            }

                                return retorno;
        }

        public async  Task<string> POSTTransactionDeb(string url, string sign, decimal amounts, decimal tax, string percentageTax, decimal rounding, decimal total, string aplications, string typeTransactionId, string payMethodId, string terminalUserId, string authorizationOriginPayment, string originPaymentId, string externalOriginPaymentId, string type, string paytStatus, decimal monto, string acount, DataTable agrupado,DataTable dt,int contar, string cuenta)
        {
            HttpContent content;
            string retorno = string.Empty;
            string json = string.Empty;
            int t = 0;
            int t1 = 1;
            int q1 = 0;
            int q2 = 1;
            int x1 = 0;
            int x2 = 1;
            decimal redondeo = 0;
            decimal subtotal = 0;
            decimal resultado = 0;
            decimal porcentaje = 0;
            decimal iva = 0;
            decimal resultadot = 0;
            decimal acumulado = 0;
            decimal totals = 0;
            decimal totalp = 0;
            decimal totalx = 0;
            decimal ivast = 0;
            decimal montosresultantes = 0;
            decimal saldoaplicar = 0;
            decimal acomulador2 = 0;
            decimal totals2 = 0;
            decimal res = 0;
            bool start = false;
            decimal checau = 0;
            decimal checaus = 0;
            

                    json = "{\"transaction\": ";
                    json = json + "{\"sign\": \"" + sign + "\", \"amount\": \"" + amounts + "\",\"account\": \"" + 22211 + "\", \"tax\": \"" + tax + "\", \"percentageTax\": \"" + percentageTax + "\", \"rounding\": \"" + rounding + "\",\"total\": \"" + total + "\", \"aplication\": \"" + aplications + "\", \"typeTransactionId\": \"" + typeTransactionId + "\", \"payMethodId\": \"" + payMethodId + "\", \"terminalUserId\": \"" + terminalUserId + "\", \"authorizationOriginPayment\": \"" + authorizationOriginPayment + "\", \"originPaymentId\": \"" + originPaymentId + "\", \"externalOriginPaymentId\": \"" + externalOriginPaymentId + "\", \"type\": \"" + type + "\", \"paytStatus\": \"" + paytStatus + "\", \"agreementId\": \"" + acount + "\",\"transactionDetails\": [";

                    x1 = agrupado.Rows.Count;
                    foreach (DataRow row in agrupado.Rows)
                    {
                        if (x1 != x2)
                        {
                            json = json + "{\"codeConcept\": \"" + row[4].ToString() + "\",\"description\": \"" + row[5].ToString() + "\",\"amount\": " + (decimal)row[7] + "},";
                            x2 = x2 + 1;
                            checau = checau + (decimal)row[7];
                        }
                        else
                        {
                            json = json + "{\"codeConcept\": \"" + row[4].ToString() + "\",\"description\": \"" + row[5].ToString() + "\",\"amount\": " + (decimal)row[7] + "}";
                            x2 = 1;
                            checau = checau + (decimal)row[7];
                        }
                    }

                    json = json + "]}";
                    json = json + ",\"debt\":[ ";

                    q1 = Variables.debst.Count;
                    redondeo = Math.Round(Convert.ToDecimal(rounding) / Variables.debst.Count,2);

                    for (int i = 0; i < Variables.debst.Count; i++)
                    {

                        foreach (var item in Variables.debst[i].debtdetails)
                        {
                            if (item.haveTax == true)
                            {
                                resultado = resultado + Convert.ToDecimal(item.amount);
                                ivast = ivast + (Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100;
                            }

                            subtotal = subtotal + item.amount;       
                        }

                        resultadot = Convert.ToDecimal(Variables.debst[i].amount) - Convert.ToDecimal(Variables.debst[i].onAccount) + ivast;
                        acumulado = acumulado + resultadot;
                        //redondeo = Math.Ceiling(acumulado) - acumulado;
                        totals = acumulado + redondeo;
                        totals = acumulado ;
                        montosresultantes = montosresultantes + resultadot;
                        totalx = total - totals;
                        totals2 = resultadot;

                        if (totalx <= 0)
                        {

                            saldoaplicar = resultadot - (resultadot - saldoaplicar);
                            totalp = saldoaplicar + acomulador2;
                            porcentaje = Convert.ToDecimal(Variables.Configuration.IVA);
                            iva = Convert.ToDecimal(Variables.Configuration.IVA);

                            if (start == false)
                            {

                                decimal c = 0;
                                decimal totalst = 0;
                                saldoaplicar = resultadot - (resultadot - total);
                                c = Convert.ToDecimal(Variables.debst[i].onAccount);
                                totalst = saldoaplicar + c;
                                if (totalx == 0)
                                {

                                    json = json + "{\"id\": \"" + Variables.debst[i].id + "\", \"debitDate\": \"" + Convert.ToDateTime(Variables.debst[i].debitDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"fromDate\": \"" + Convert.ToDateTime(Variables.debst[i].fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"untilDate\": \"" + Convert.ToDateTime(Variables.debst[i].untilDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"derivatives\": \"" + Variables.debst[i].derivatives + "\", \"typeIntake\": \"" + Variables.debst[i].typeIntake + "\", \"typeService\": \"" + Variables.debst[i].typeService + "\", \"consumption\": \"" + Variables.debst[i].consumption + "\", \"discount\": \"" + Variables.debst[i].discount + "\", \"amount\": \"" + Variables.debst[i].amount + "\", \"onAccount\": \"" + (Math.Round(totalst,2) - tax) + "\", \"year\": \"" + Variables.debst[i].year + "\", \"type\": \"" + Variables.debst[i].type + "\", \"status\": \"" + Variables.debst[i].status + "\",\"newStatus\": \"ED005\", \"debtPeriodId\": \"" + Variables.debst[i].debtPeriodId + "\", \"agreementId\": \"" + Variables.debst[i].agreementId + "\", \"agreement\": \"" + Variables.debst[i].agreement + "\",\"debtDetails\":[";

                                }
                                else
                                {
                                    json = json + "{\"id\": \"" + Variables.debst[i].id + "\", \"debitDate\": \"" + Convert.ToDateTime(Variables.debst[i].debitDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"fromDate\": \"" + Convert.ToDateTime(Variables.debst[i].fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"untilDate\": \"" + Convert.ToDateTime(Variables.debst[i].untilDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"derivatives\": \"" + Variables.debst[i].derivatives + "\", \"typeIntake\": \"" + Variables.debst[i].typeIntake + "\", \"typeService\": \"" + Variables.debst[i].typeService + "\", \"consumption\": \"" + Variables.debst[i].consumption + "\", \"discount\": \"" + Variables.debst[i].discount + "\", \"amount\": \"" + Variables.debst[i].amount + "\", \"onAccount\": \"" + (Math.Round(totalst,2) -tax) + "\", \"year\": \"" + Variables.debst[i].year + "\", \"type\": \"" + Variables.debst[i].type + "\", \"status\": \"" + Variables.debst[i].status + "\",\"newStatus\": \"ED004\", \"debtPeriodId\": \"" + Variables.debst[i].debtPeriodId + "\", \"agreementId\": \"" + Variables.debst[i].agreementId + "\", \"agreement\": \"" + Variables.debst[i].agreement + "\",\"debtDetails\":[";

                                }
                            }
                            else
                            {
                                if(totalx == 0)
                                {

                                    json = json + "{\"id\": \"" + Variables.debst[i].id + "\", \"debitDate\": \"" + Convert.ToDateTime(Variables.debst[i].debitDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"fromDate\": \"" + Convert.ToDateTime(Variables.debst[i].fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"untilDate\": \"" + Convert.ToDateTime(Variables.debst[i].untilDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"derivatives\": \"" + Variables.debst[i].derivatives + "\", \"typeIntake\": \"" + Variables.debst[i].typeIntake + "\", \"typeService\": \"" + Variables.debst[i].typeService + "\", \"consumption\": \"" + Variables.debst[i].consumption + "\", \"discount\": \"" + Variables.debst[i].discount + "\", \"amount\": \"" + Variables.debst[i].amount + "\", \"onAccount\": \"" + Math.Round(((saldoaplicar + (Convert.ToDecimal(Variables.debst[i].onAccount))) - tax),2) + "\", \"year\": \"" + Variables.debst[i].year + "\", \"type\": \"" + Variables.debst[i].type + "\", \"status\": \"" + Variables.debst[i].status + "\",\"newStatus\": \"ED005\", \"debtPeriodId\": \"" + Variables.debst[i].debtPeriodId + "\", \"agreementId\": \"" + Variables.debst[i].agreementId + "\", \"agreement\": \"" + Variables.debst[i].agreement + "\",\"debtDetails\":[";
                                }
                                else
                                {
                                    json = json + "{\"id\": \"" + Variables.debst[i].id + "\", \"debitDate\": \"" + Convert.ToDateTime(Variables.debst[i].debitDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"fromDate\": \"" + Convert.ToDateTime(Variables.debst[i].fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"untilDate\": \"" + Convert.ToDateTime(Variables.debst[i].untilDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"derivatives\": \"" + Variables.debst[i].derivatives + "\", \"typeIntake\": \"" + Variables.debst[i].typeIntake + "\", \"typeService\": \"" + Variables.debst[i].typeService + "\", \"consumption\": \"" + Variables.debst[i].consumption + "\", \"discount\": \"" + Variables.debst[i].discount + "\", \"amount\": \"" + Variables.debst[i].amount + "\", \"onAccount\": \"" + Math.Round(((saldoaplicar + (Convert.ToDecimal(Variables.debst[i].onAccount))) - tax),2) + "\", \"year\": \"" + Variables.debst[i].year + "\", \"type\": \"" + Variables.debst[i].type + "\", \"status\": \"" + Variables.debst[i].status + "\",\"newStatus\": \"ED004\", \"debtPeriodId\": \"" + Variables.debst[i].debtPeriodId + "\", \"agreementId\": \"" + Variables.debst[i].agreementId + "\", \"agreement\": \"" + Variables.debst[i].agreement + "\",\"debtDetails\":[";
                                }
                                

                            }

                            t = Variables.debst[i].debtdetails.Count;
                            int contador = contar;

                    

                            foreach (var item in Variables.debst[i].debtdetails)
                            {
                               

                                if (start == false)
                                {

                                    if (t != t1)
                                    {


                                if (item.haveTax == true)
                                {
                                   

                                    json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((decimal)dt.Rows[contador - 1][7]) + (Convert.ToDecimal(item.onAccount))) + "\", \"haveTax\": \"" + item.haveTax + "\",\"onPayment\": \"" + (decimal)dt.Rows[contador - 1][7] + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\",\"tax\": \"" + (decimal)dt.Rows[contador - 1]["TaxResult"] + "\"},";
                                    t1 = t1 + 1;
                                    contador = contador + 1;

                                }
                                else
                                {
                                    json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((decimal)dt.Rows[contador - 1][7]) + (Convert.ToDecimal(item.onAccount))) + "\", \"haveTax\": \"" + item.haveTax + "\",\"onPayment\": \"" + (decimal)dt.Rows[contador - 1][7] + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\"},";
                                    t1 = t1 + 1;
                                    contador = contador + 1;
                                }

                                




                                    }
                                    else
                                    {


                                if (item.haveTax == true)
                                {

                                    json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((decimal)dt.Rows[contador - 1][7]) + (Convert.ToDecimal(item.onAccount))) + "\", \"haveTax\": \"" + item.haveTax + "\",\"onPayment\": \"" + (decimal)dt.Rows[contador - 1][7] + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\",\"tax\": \"" + (decimal)dt.Rows[contador - 1]["TaxResult"] + "\"}";
                                    t1 = 1;

                                }
                                else
                                {

                                    json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((decimal)dt.Rows[contador - 1][7]) + (Convert.ToDecimal(item.onAccount))) + "\", \"haveTax\": \"" + item.haveTax + "\",\"onPayment\": \"" + (decimal)dt.Rows[contador - 1][7] + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\"}";
                                    t1 = 1;
                                    contador = 0;
                                }

                                }

                                }
                                else
                                {
                                    
                                    if (t != t1)
                                    {



                                if (item.haveTax == true)
                                {

                                    json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((decimal)dt.Rows[contador - 1][7]) + (Convert.ToDecimal(item.onAccount))) + "\", \"haveTax\": \"" + item.haveTax + "\",\"onPayment\": \"" + (decimal)dt.Rows[contador - 1][7] + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\",\"tax\": \"" + (decimal)dt.Rows[contador - 1]["TaxResult"] + "\"},";
                                    t1 = t1 + 1;
                                    contador = contador + 1;

                                }
                                else
                                {

                                    json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((decimal)dt.Rows[contador - 1][7]) + (Convert.ToDecimal(item.onAccount))) + "\", \"haveTax\": \"" + item.haveTax + "\",\"onPayment\": \"" + (decimal)dt.Rows[contador - 1][7] + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\"},";
                                    t1 = t1 + 1;
                                    contador = contador + 1;

                                }
                                    }
                                    else
                                    {


                                if (item.haveTax == true)
                                {
                                    json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((decimal)dt.Rows[contador - 1][7]) + (Convert.ToDecimal(item.onAccount))) + "\", \"haveTax\": \"" + item.haveTax + "\",\"onPayment\": \"" + (decimal)dt.Rows[contador - 1][7] + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\",\"tax\": \"" + (decimal)dt.Rows[contador - 1]["TaxResult"] + "\"}";
                                    t1 = 1;
                                    contador = 0;


                                }
                                else
                                {


                                    json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + (((decimal)dt.Rows[contador - 1][7]) + (Convert.ToDecimal(item.onAccount))) + "\", \"haveTax\": \"" + item.haveTax + "\",\"onPayment\": \"" + (decimal)dt.Rows[contador - 1][7] + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\"}";
                                    t1 = 1;
                                    contador = 0;

                                }

                                    }   
                                }

                            }

                            json = json + "],\"debtStatuses\":[]}";
                            subtotal = 0;
                            resultado = 0;
                            porcentaje = 0;
                            iva = 0;
                            break;

                        }
                        else
                        {
                            acomulador2 = acomulador2 + resultadot;
                            saldoaplicar = totalx;
                            porcentaje = Convert.ToDecimal(Variables.Configuration.IVA);
                            iva = Convert.ToDecimal(Variables.Configuration.IVA);                            
                            json = json + "{\"id\": \"" + Variables.debst[i].id + "\", \"debitDate\": \"" + Convert.ToDateTime(Variables.debst[i].debitDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"fromDate\": \"" + Convert.ToDateTime(Variables.debst[i].fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"untilDate\": \"" + Convert.ToDateTime(Variables.debst[i].untilDate).ToString("yyyy-MM-dd HH:mm:ss") + "\", \"derivatives\": \"" + Variables.debst[i].derivatives + "\", \"typeIntake\": \"" + Variables.debst[i].typeIntake + "\", \"typeService\": \"" + Variables.debst[i].typeService + "\", \"consumption\": \"" + Variables.debst[i].consumption + "\", \"discount\": \"" + Variables.debst[i].discount + "\", \"amount\": \"" + Variables.debst[i].amount + "\", \"onAccount\": \"" + Math.Round(((Convert.ToDecimal(Variables.debst[i].amount) - Convert.ToDecimal(Variables.debst[i].onAccount) * total / monto) + Convert.ToDecimal(Variables.debst[i].onAccount)),2) + "\", \"year\": \"" + Variables.debst[i].year + "\", \"type\": \"" + Variables.debst[i].type + "\", \"status\": \"" + Variables.debst[i].status + "\",\"newStatus\": \"ED005\", \"debtPeriodId\": \"" + Variables.debst[i].debtPeriodId + "\", \"agreementId\": \"" + Variables.debst[i].agreementId + "\", \"agreement\": \"" + Variables.debst[i].agreement + "\",\"debtDetails\":[";
                            t = Variables.debst[i].debtdetails.Count;
              
                            foreach (var item in Variables.debst[i].debtdetails)
                            {
                                if (t != t1)
                                {


                            if (item.haveTax == true)
                            {

                                json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + Math.Round((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto) + Convert.ToDecimal(item.onAccount)), 2) + "\", \"haveTax\": \"" + item.haveTax + "\", \"onPayment\": \"" + Math.Round((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto)), 2) + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\",\"tax\": \"" + Math.Round(((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * iva / 100), 2) + "\"},";
                                t1 = t1 + 1;

                            }
                            else
                            {

                                json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + Math.Round((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto) + Convert.ToDecimal(item.onAccount)), 2) + "\", \"haveTax\": \"" + item.haveTax + "\", \"onPayment\": \"" + Math.Round((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto)), 2) + "\", \"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\"},";
                                t1 = t1 + 1;

                            }
                            }
                                else
                                {

                            if (item.haveTax == true)
                            {

                                json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + Math.Round(((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total) / monto) + Convert.ToDecimal(item.onAccount)), 2) + "\", \"haveTax\": \"" + item.haveTax + "\",  \"onPayment\": \"" + Math.Round((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto)), 2) + "\",\"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\",\"tax\": \"" + Math.Round(((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * iva / 100), 2) + "\"}";
                                t1 = 1;

                            }
                            else
                            {

                                json = json + "{\"id\": \"" + item.id + "\", \"amount\": \"" + item.amount + "\", \"onAccount\": \"" + Math.Round(((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total) / monto) + Convert.ToDecimal(item.onAccount)), 2) + "\", \"haveTax\": \"" + item.haveTax + "\",  \"onPayment\": \"" + Math.Round((((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * total / monto)), 2) + "\",\"codeConcept\": \"" + item.codeConcept + "\", \"nameConcept\": \"" + item.nameConcept + "\", \"debtId\": \"" + item.debtId + "\"}";
                                t1 = 1;
                            }
                                }
                            }

                            if (q2 != q1)
                            {
                                json = json + "],\"debtStatuses\":[]},";
                                q2 = q2 + 1;
                            }
                            else
                            {
                                json = json + "],\"debtStatuses\":[]}";
                            }

                            start = true;
                            subtotal = 0;
                            resultado = 0;
                            porcentaje = 0;
                            iva = 0;
                        }
                    }

                        json = json + "]}";
                        content = new StringContent(json, Encoding.UTF8, "application/json");

            
                        var resultados = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);

                        if (resultados.Contains("error"))
                        {
                            Error m = JsonConvert.DeserializeObject<Error>(resultados);
                            retorno = "error/" + m.error;
                        }
                        else
                        {
                            int rest = Convert.ToInt32(resultados);
                            Variables.idtransaction = rest;
                            //await GETTransactionID("/api/Transaction/" + rest);
                            retorno = "Exito";
                        }

            
                        return retorno;
        }


        public async Task<DataTable> GETTransactionID(string url)
        {
            DataTable dt = new DataTable();
            string id = string.Empty;
            string cuenta = string.Empty;
            string names = string.Empty;
            string direccion = string.Empty;
            string rfc = string.Empty;
            string WEBSERVICE_URL = url;
            string json = string.Empty;

            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Id";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "amount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "onAccount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "have_tax";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "code_concept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "name_concept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "debtId";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "deuda";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Year";
            dt.Columns.Add(column);


            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Debperiod";
            dt.Columns.Add(column);
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);

            try
            {
                            TransactionVM m = JsonConvert.DeserializeObject<TransactionVM>(resultado);
                try {
                    Variables.foliocaja = m.transaction.transactionFolios.FirstOrDefault().folio.ToString();
                }  
                catch
                {

                }
                            Variables.foliotransaccion = m.transaction.folio;
                            Variables.subtotalp = m.transaction.amount;
                            Variables.ivap = m.transaction.tax;
                            Variables.redondeop = m.transaction.rounding;
                            Variables.totalp = m.transaction.total;
                            Variables.metododepagop = m.transaction.payMethod.name;
                            Variables.idagrement = m.payment.AgreementId.ToString();

                            
                                    foreach (var item in m.payment.PaymentDetails)
                                    {
                                        DataRow row = dt.NewRow();
                                        row["Id"] = item.Id;
                                        row["amount"] = item.Amount;
                                        row["onAccount"] = item.Amount;
                                        row["have_tax"] = true;
                                        row["code_concept"] = item.CodeConcept;
                                        row["name_concept"] = item.Description;
                                        row["debtId"] = item.DebtId.ToString();
                                        row["deuda"] = item.Amount;
                try
                {
                    row["Year"] = item.Debt.Year;
                    row["Debperiod"] = Convert.ToDateTime(item.Debt.FromDate).ToString("dd-MM-yyyy") + " a " + Convert.ToDateTime(item.Debt.UntilDate).ToString("dd-MM-yyyy");
                }
                
                catch (Exception)
                {

                 
                }
                dt.Rows.Add(row);

            }
            }
            catch (Exception)
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);

            }
            return dt;
        }

        public async Task<string> POSTTransactionAnt(string url, string folio, string sign, string amounts, decimal tax, string percentageTax, decimal rounding, decimal total, string aplications, string typeTransactionId, string payMethodId, string terminalUserId, string authorizationOriginPayment, string originPaymentId, string externalOriginPaymentId, string type, string debtStatus, decimal monto, string acount,  string codeConcept, string description, string amount)
        {
                        string retorno = string.Empty;
                        HttpContent content;
                        string json = string.Empty;
                        json = "{\"sign\": \"" + sign + "\", \"amount\": \"" + amounts + "\",\"account\": \"" + acount + "\", \"tax\": \"" + tax + "\", \"percentageTax\": \"" + percentageTax + "\", \"rounding\": \"" + rounding + "\",\"total\": \"" + total + "\", \"aplication\": \"" + aplications + "\", \"typeTransactionId\": \"" + typeTransactionId + "\", \"payMethodId\": \"" + payMethodId + "\", \"terminalUserId\": \"" + terminalUserId + "\", \"authorizationOriginPayment\": \"" + authorizationOriginPayment + "\", \"originPaymentId\": \"" + originPaymentId + "\", \"externalOriginPaymentId\": \"" + externalOriginPaymentId + "\", \"type\": \"" + type + "\", \"paytStatus\": \"" + debtStatus + "\", \"agreementId\": \"" + acount + "\",\"transactionDetails\": [{\"codeConcept\": \"08\",\"description\": \"Anticipo\",\"amount\": \"" + amounts + "\"}]}";
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        var jsonResponse = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);

                        if (jsonResponse.Contains("error"))
                        {
                            Error ms = JsonConvert.DeserializeObject<Error>(jsonResponse);
                            retorno = "error/" + ms.error;
                        }
                        else
                        {
                            Transaction m = JsonConvert.DeserializeObject<Transaction>(jsonResponse);
                            int rest = m.id;
                            Variables.idtransaction = rest;
                            //await GETTransactionID("/api/Transaction/" + rest);
                            retorno = "Exito";
                        }            

                        return retorno;
        }

        public async Task<DataTable> GETDiscountValidator(string url)
        {
            DataTable dt = new DataTable();
           
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Id";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "amount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "onAccount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "have_tax";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "code_concept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "name_concept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "debtId";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "deuda";
            dt.Columns.Add(column);


            
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);

            try
            {            
            List<Adeudos> m = JsonConvert.DeserializeObject<List<Adeudos>>(resultado);

                        for(int x = 0; x < m.Count; x++)
                            {
                                DataRow row = dt.NewRow();
                                row["Id"] = 1;
                                row["amount"] = m[x].amount;
                                row["onAccount"] = 0;
                                row["have_tax"] = m[x].have_tax;
                                row["code_concept"] = m[x].conde_concept;
                                row["name_concept"] = m[x].name_concept;
                                row["debtId"] = m[x].id_discount;
                                row["deuda"] = m[x].amount;
                                dt.Rows.Add(row);
                            }
            }
            catch (Exception)
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public async Task<string> POSTTransactionAntA(string url, string sign, decimal amounts, decimal tax, string percentageTax, decimal rounding, decimal total, string aplications, string typeTransactionId, string payMethodId, string terminalUserId, string authorizationOriginPayment, string originPaymentId, string externalOriginPaymentId, string type, string paytStatus, decimal monto, string acount, DataTable agrupado, DataTable dt,int porcentaje,string cuenta)
        {
                        string retorno = string.Empty;
                        HttpContent content;
                        string json = string.Empty;
                        json = "{\"sign\": \"" + sign + "\", \"amount\": \"" + amounts + "\",\"account\": \"" + cuenta + "\", \"tax\": \"" + tax + "\", \"percentageTax\": \"" + percentageTax + "\", \"rounding\": \"" + rounding + "\",\"total\": \"" + total + "\", \"aplication\": \"" + aplications + "\", \"typeTransactionId\": \"" + typeTransactionId + "\", \"payMethodId\": \"" + payMethodId + "\", \"terminalUserId\": \"" + terminalUserId + "\",\"percentage\": \"" + porcentaje + "\", \"authorizationOriginPayment\": \"" + authorizationOriginPayment + "\", \"originPaymentId\": \"" + originPaymentId + "\", \"externalOriginPaymentId\": \"" + externalOriginPaymentId + "\", \"type\": \"" + type + "\", \"paytStatus\": \"" + paytStatus + "\", \"agreementId\": \"" + acount + "\",\"transactionDetails\": [";
                        json = json + "{\"codeConcept\": \"ANT\",\"description\": \"Pago Anticipado\",\"amount\": " + amounts + "}";
                        json = json + "]}";
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        var resultados = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);

                        if (resultados.Contains("error"))
                        {
                            Error m = JsonConvert.DeserializeObject<Error>(resultados);
                            retorno = "error/" + m.error;
                        }
                        else
                        {
                            Transaction m = JsonConvert.DeserializeObject<Transaction>(resultados);
                            int rest = m.id;
                            Variables.idtransaction = rest;
                           // await GETTransactionID("/api/Transaction/" + rest);
                            retorno = "Exito";

                        }
                        return retorno;
        }

        public async Task<string> POSTTransactionCancel(string url, TransactionVM ms)
        {
            string retorno = string.Empty;
            string json = string.Empty;
            HttpContent content;
            json = JsonConvert.SerializeObject(ms);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            var jsonResponse = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);
            if (jsonResponse.Contains("error"))
            {
                Error m = JsonConvert.DeserializeObject<Error>(jsonResponse);
                retorno = "error/" + m.error;
            }
            else
            {
                retorno = "Exito";
            }

            return retorno;
        }

        public async Task<string> POSTTransactionCancelAnt(string url, TransactionVM ms)
        {
            string retorno = string.Empty;
            HttpContent content;
            string json = string.Empty;            
            json = JsonConvert.SerializeObject(ms);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            var resultados = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);
            if (resultados.Contains("error"))
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultados);
                retorno = "error/" + m.error;
            }
            else
            {
                retorno = "Exito";
            }

            return retorno;
        }

        public async Task<string> GETTransactionIDT(string url,string idcuenta,string folio)
        {
                            
                            string dt = string.Empty;
                            string cadena = string.Empty;
                            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {

            
                            TransactionVM m = JsonConvert.DeserializeObject<TransactionVM>(resultado);
                            m.transaction.sign = false;
                            m.transaction.cancellationFolio = folio;
                            m.transaction.typeTransactionId = 4;
                            m.transaction.agreementId = m.payment.AgreementId;
                            m.transaction.cancellation=folio;
                            string check = string.Empty;
                            string[] separadas;
                            cadena = m.payment.PaymentDetails.FirstOrDefault().CodeConcept;
                            
                            if (cadena == "ANT01")
                            {
                                

                                check = await POSTTransactionCancelAnt("/api/Transaction/Prepaid/Cancel/" + idcuenta + "", m);


                            }
                            else
                            {
                                check = await POSTTransactionCancel("/api/Transaction/Cancel/" + idcuenta + "", m);
                            }
                            
                            separadas = check.Split('/');
                            if (separadas[0].ToString() == "error")
                            {
                                dt = separadas[1].ToString();
                            }
                            else
                            {
                                dt = "Se  generado la cancelacion correctamente";
                            }

            }
            catch (Exception)
            {
                
            }
            return dt;
        }

        public async Task<string> POSTTransactionPL(string url,   string sign, decimal amounts, decimal tax, string percentageTax, decimal rounding, decimal total, string aplications, string typeTransactionId, string payMethodId, string terminalUserId, string authorizationOriginPayment, string originPaymentId, string externalOriginPaymentId, string type, string debtStatus, decimal monto, string acount, DataTable agrupado, Venta ms, string cuenta)
        {
                string retorno = string.Empty;
                string json = string.Empty;
                HttpContent content;
                ms.transaction.agreementId = Convert.ToInt32(acount);
                ms.transaction.sign = true;
                ms.transaction.amount = amounts;
                ms.transaction.tax = tax;
                ms.transaction.percentageTax = percentageTax;
                ms.transaction.rounding = rounding;
                ms.transaction.total = total;
            ms.transaction.account = cuenta;
                ms.transaction.aplication = aplications;
                ms.transaction.typeTransactionId = Convert.ToInt32(typeTransactionId);
                ms.transaction.payMethodId = Convert.ToInt32(payMethodId);
                ms.transaction.terminalUserId = Convert.ToInt32(terminalUserId);
                ms.transaction.authorizationOriginPayment = authorizationOriginPayment;
                ms.transaction.originPaymentId = Convert.ToInt32(originPaymentId);
                ms.transaction.externalOriginPaymentId = Convert.ToInt32(externalOriginPaymentId);
                ms.transaction.type = type;
                ms.transaction.paytStatus = debtStatus;
                ms.transaction.agreementId = Convert.ToInt32(acount);
                ms.transaction.cancellationFolio = "";

                foreach (var t in ms.debt)
                {

                t.onAccount = ((Convert.ToDecimal((t.amount) - Convert.ToDecimal(t.onAccount) * total / monto) + Convert.ToDecimal(t.onAccount))).ToString();
                t.newStatus = "ED005";

                foreach (var t1 in t.debtdetails)
                {
                    t1.onPayment = ((((Convert.ToDecimal(t1.amount) - Convert.ToDecimal(t1.onAccount)) * total / monto))).ToString();
                    t1.onAccount = ((((Convert.ToDecimal(t1.amount) - Convert.ToDecimal(t1.onAccount)) * total) / monto) + Convert.ToDecimal(t1.onAccount)).ToString(); 
                }

                }

                                    json = JsonConvert.SerializeObject(ms);
                                    content = new StringContent(json, Encoding.UTF8, "application/json");
                                    var jsonResponse = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);
            
                                    if (jsonResponse.Contains("error"))
                                    {
                                        Error m = JsonConvert.DeserializeObject<Error>(jsonResponse);
                                        retorno = "error/" + m.error;
                                    }
                                    else
                                    {
                                        var results = jsonResponse;
                                        int res = Convert.ToInt32(results);
                                        Variables.idtransaction = res;
                                       // await GETTransactionID("/api/Transaction/" + res.ToString());
                                        retorno = "Exito";
                                    }

                                    return retorno;
        }
        
        public async Task<DataTable> GETTProdcutos(string url)
        {
            DataTable dt = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ID";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Descripcion";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "parentID";
            dt.Columns.Add(column);

            
            string WEBSERVICE_URL = url;
            string json = string.Empty;
            string saber = string.Empty;
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
            List<Producto> m = JsonConvert.DeserializeObject<List<Producto>>(resultado);
            foreach (var item in m)
            {
                DataRow row = dt.NewRow();
                row["ID"] = item.id;
                row["Descripcion"] = item.name;
                row["parentID"] = item.parent;        
                dt.Rows.Add(row);
            }
            }
            catch (Exception)
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public async Task<Tariff> GETTariff(string url)
        {
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            Tariff m = JsonConvert.DeserializeObject<Tariff>(resultado);
            return m;
        }


        public async Task<string> POSTProduct(string url, Model.Debt ms)
        {
            string retorno = string.Empty;
            HttpContent content;
            string json = string.Empty;
            json = JsonConvert.SerializeObject(ms);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            var resultados = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);
            if (resultados.Contains("error"))
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultados);
                retorno = "error/" + m.error;
            }
            else
            {
                retorno = "Exito:" + resultados.ToString();
            }

            return retorno;
        }

        public async Task<string> POSTOrderSales(string url, Model.OrderSale ms)
        {
            string retorno = string.Empty;
            HttpContent content;
            string json = string.Empty;
            json = JsonConvert.SerializeObject(ms);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            var resultados = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, content);
            if (resultados.Contains("error"))
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultados);
                retorno = "error/" + m.error;
            }
            else
            {
                retorno = resultados;
            }

            return retorno;
        }


        public async Task<DataTable> GetRecibos(string url)
        {
            DataTable dt = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Id";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Periodo";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Monto";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Tipodecuenta";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Servicio";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Tipo";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Emision";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Totalapagar";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Descripciondeestado";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Array";
            dt.Columns.Add(column);

            Getdata gets1 = new Getdata();
            string k = string.Empty;

            gets1 = await GetDatas("/api/Agreements/GetData");

            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
                List<Deb> ms  = JsonConvert.DeserializeObject<List<Deb>>(resultado);
                for (int i = 0; i < ms.Count; i++) {

                    DataRow row = dt.NewRow();
                    row["Id"] = ms[i].id.ToString();
                    row["Periodo"] = ms[i].fromDate.ToString() + " a " + ms[i].untilDate.ToString();
                    row["Monto"] = ms[i].amount.ToString();
                    row["Tipodecuenta"] = ms[i].typeIntake.ToString();
                    row["Servicio"] = ms[i].typeService.ToString();
                    row["Tipo"] = gets1.typeDebts.FirstOrDefault(x => x.idType == ms[i].type).description;                   
                    row["Emision"] = ms[i].expirationDate.ToString();
                    row["Totalapagar"] = ms[i].onAccount.ToString();
                    row["Descripciondeestado"] = ms[i].descriptionStatus.ToString();
                    k = JsonConvert.SerializeObject(ms[i].debtdetails);
                    row["Array"] = k;

                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {

                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);

            }

            return dt;
        }

        public async Task<DataTable> GetNotify(string url)
        {
            DataTable dt = new DataTable();
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "prepaidDate";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "amount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "accredited";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "status";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Array";
      
            dt.Columns.Add(column);




           
            string k = string.Empty;

           
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
                List<Notify> ms = JsonConvert.DeserializeObject<List<Notify>>(resultado);
                for (int i = 0; i < ms.Count; i++)
                {

                    DataRow row = dt.NewRow();
                 
                     
                    row["prepaidDate"] = ms[i].prepaidDate.ToString();
                    row["amount"] = ms[i].amount.ToString();
                    row["accredited"] = ms[i].accredited.ToString();
                    row["status"] = ms[i].status.ToString();
                    row["status"] = ms[i].status.ToString();
                    k = JsonConvert.SerializeObject(ms[i].prepaidDetails);
                    row["Array"] = k;
                    
                    /* row["statusDescription"] = ms[i].statusDescription.ToString();
                    row["type"] =  ms[i].type.ToString();
                    row["percentage"] = ms[i].percentage.ToString();
                    row["agreementId"] = (ms[i].agreementId.ToString().Length>0)? ms[i].agreementId.ToString() : "";
                   */

                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {

                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);

            }

            return dt;
        }
        public async Task<DataTable> GetAnticipos(string url)
        {
            DataTable dt = new DataTable();
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "notificationDate";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "untilDate";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "subtotal";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "total";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "tax";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Array";
            dt.Columns.Add(column);



            Getdata gets1 = new Getdata();
            string k = string.Empty;

            gets1 = await GetDatas("/api/Agreements/GetData");

            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {
                List<Anticipos> ms = JsonConvert.DeserializeObject<List<Anticipos>>(resultado);
                for (int i = 0; i < ms.Count; i++)
                {

                    DataRow row = dt.NewRow();
                   // row["Id"] = ms[i].id.ToString();
                  //  row["folio"] = ms[i].folio.ToString();
                    row["notificationDate"] = ms[i].notificationDate.ToString();
                  //  row["fromDate"] = ms[i].fromDate.ToString();
                    row["untilDate"] = ms[i].untilDate.ToString();
                    row["subtotal"] = ms[i].subtotal.ToString();
                    row["tax"] = ms[i].tax.ToString();
                    //row["rounding"] = ms[i].rounding.ToString();
                    row["total"] = ms[i].total.ToString();
                    // row["status"] = ms[i].status.ToString();
                    //row["agreementId"] = (ms[i].agreementId.ToString().Length > 0) ? ms[i].agreementId.ToString() : "";
                    k = JsonConvert.SerializeObject(ms[i].notificationDetails);
                    row["Array"] = k;
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {

                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);

            }

            return dt;
        }
        
        public async Task<Getdata> GetDatas(string url)
        {
            Getdata gets = null;
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);
            gets = JsonConvert.DeserializeObject<Getdata>(resultado);
            return gets;
        }

        public async Task<DataTable> GETTransactionIDCR(string url, string concpeto)
        {
            DataTable dt = new DataTable();
            string id = string.Empty;
            string cuenta = string.Empty;
            string names = string.Empty;
            string direccion = string.Empty;
            string rfc = string.Empty;
            string WEBSERVICE_URL = url;
            string json = string.Empty;

            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Id";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "amount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "onAccount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "have_tax";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "code_concept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "name_concept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "debtId";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "deuda";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Year";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Debperiod";
            dt.Columns.Add(column);
            var resultado = await Requests.SendURIAsync(string.Format(url), HttpMethod.Get, Variables.LoginModel.Token);

            try
            {
                TransactionVM m = JsonConvert.DeserializeObject<TransactionVM>(resultado);
                Variables.foliotransaccion = m.transaction.folio;
                Variables.subtotalp = m.transaction.amount;
                Variables.ivap = m.transaction.tax;
                Variables.redondeop = m.transaction.rounding;
                Variables.totalp = m.transaction.total;
                Variables.metododepagop = m.transaction.payMethod.name;

                DataRow row = dt.NewRow();
                row["Id"] = m.transaction.id;
                row["amount"] = m.transaction.amount;
                row["onAccount"] = m.transaction.amount;
                row["have_tax"] = true;
                row["code_concept"] = "";
                row["name_concept"] = concpeto;
                row["debtId"] = "1";
                row["deuda"] = m.transaction.amount;
                try
                {
                    row["Year"] = "";
                    row["Debperiod"] = "";
                }

                catch (Exception)
                {


                }
                dt.Rows.Add(row);
            }
            catch (Exception)
            {
                Error m = JsonConvert.DeserializeObject<Error>(resultado);
                DataRow dr = dt.NewRow();
                dr[0] = "error/" + m.error;
                dt.Rows.Add(dr);
            }
            return dt;
        }




        ////////////////////////////////////////FUNCIONES//////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        public string GETMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }

        public string sacarcaja(string impresora,string anssi)
        {
            string k = string.Empty;
            string[] separadas;
            try
            {
                separadas = anssi.Split('.');
                byte[] byteOut = new byte[separadas.Length];
                for (int i = 0; i < separadas.Length; i++)
                {
                    byteOut[i] = Convert.ToByte(separadas[i]);
                }
                IntPtr pUnmanagedBytes = new IntPtr(0);
                pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
                Marshal.Copy(byteOut, 0, pUnmanagedBytes, 5);
                RawPrinterHelper.SendBytesToPrinter(impresora, pUnmanagedBytes, 5);
                Marshal.FreeCoTaskMem(pUnmanagedBytes);
                k = "Exito";
            }
            catch {

                k = "error/El cajon de dinero no se encuentra configurada";
            }
            return k;
            
        }

        public string ImpresoraPredeterminada()
        {
            for (Int32 i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                PrinterSettings a = new PrinterSettings();
                a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                if (a.IsDefaultPrinter)
                {
                    return PrinterSettings.InstalledPrinters[i].ToString();
                }
            }
            return string.Empty;
        }

        public System.Drawing.Image Imagen()
        {
            System.Drawing.Image im = null;
            var request = WebRequest.Create(Variables.Configuration.Image);

            try
            {

            
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                im = Bitmap.FromStream(stream);
            }
            }
            catch (Exception)
            {


            }

            return im;
        }
        
        public bool EstaEnLineaLaImpresora(string printerName)
        {
            string str = "";
            bool online = false;

            ManagementScope scope = new ManagementScope(ManagementPath.DefaultPath);

            scope.Connect();

            //Consulta para obtener las impresoras, en la API Win32
            SelectQuery query = new SelectQuery("select * from Win32_Printer");

            ManagementClass m = new ManagementClass("Win32_Printer");

            ManagementObjectSearcher obj = new ManagementObjectSearcher(scope, query);

            //Obtenemos cada instancia del objeto ManagementObjectSearcher
            using (ManagementObjectCollection printers = m.GetInstances())
                foreach (ManagementObject printer in printers)
                {
                    if (printer != null)
                    {
                        //Obtenemos la primera impresora en el bucle
                        str = printer["Name"].ToString().ToLower();

                        if (str.Equals(printerName.ToLower()))
                        {
                            //Una vez encontrada verificamos el estado de ésta
                            if (printer["WorkOffline"].ToString().ToLower().Equals("true") || printer["PrinterStatus"].Equals(7))
                                //Fuera de línea
                                online = false;
                            else
                                //En línea
                                online = true;
                        }
                    }
                    else
                        throw new Exception("No fueron encontradas impresoras instaladas en el equipo");
                }
            return online;
        }   
    }
}
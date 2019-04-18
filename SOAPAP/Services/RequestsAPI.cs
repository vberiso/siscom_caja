﻿using Newtonsoft.Json;
using SOAPAP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOAPAP.Services
{
    public class RequestsAPI
    {
        private string UrlBase;
        //public static LoginModel LoginModel { get; set; }

        public RequestsAPI(string UrlBase)
        {
            this.UrlBase = UrlBase;
        }

     
        public async Task<string> Login(string userName, string password)
        {
            Error error = new Error();
            string EndPoint = "/api/Auth/login";
            HttpContent content = new StringContent("{\"UserName\": \"" + userName + "\", \"Password\": \"" + password + "\"}", Encoding.UTF8, "application/json");

            var request = await SendURIAsync(EndPoint, HttpMethod.Post, content);

            try
            {
                Variables.LoginModel = JsonConvert.DeserializeObject<LoginModel>(request);
                if (string.IsNullOrEmpty(Variables.LoginModel.User))
                {
                    if (request.Contains("message"))
                        return "error/" + JsonConvert.DeserializeObject<MessageData>(request).Message;
                    else
                        return "error/" + JsonConvert.DeserializeObject<Error>(request).error;

                }
                else
                {
                    //Variables.LoginModel = LoginModel;
                    return "Success";
                }
            }
            catch (Exception e)
            {
                return "error/" + e;
            }
        }

        public async Task<string> SendURIAsync(string endPoint, HttpMethod method, HttpContent httpContent = null)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage httpResponse = null;
                try
                {
                    var time = client.Timeout;
                    if (method.Method == "GET")
                    {
                        HttpRequestMessage httpRequest = new HttpRequestMessage
                        {
                            Method = method,
                            RequestUri = new Uri(UrlBase + endPoint),
                        };
                        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpResponse = await client.SendAsync(httpRequest);
                    }
                    else
                    {
                        HttpRequestMessage httpRequest = new HttpRequestMessage
                        {
                            Method = method,
                            RequestUri = new Uri(UrlBase + endPoint),
                            Content = httpContent,
                        };
                        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpResponse = await client.SendAsync(httpRequest);
                    }

                    switch (httpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            return await httpResponse.Content.ReadAsStringAsync();
                        case System.Net.HttpStatusCode.InternalServerError:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias\"}";
                        case System.Net.HttpStatusCode.ServiceUnavailable:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias\"}";
                        case System.Net.HttpStatusCode.Unauthorized:
                            return "{\"error\": \"Sesión expírada o no cuenta con la autorización \"}";
                        default:
                            return await httpResponse.Content.ReadAsStringAsync();
                    }
                   
                }
                catch (Exception e)
                {
                    return "{\"error\": \"Servicio no disponible contacte al Administrador\"}";
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
        }

        public async Task<string> SendURIAsync(string endPoint, HttpMethod method, string Token, HttpContent httpContent = null)
        {

            using (var client = new HttpClient())
            {
                HttpResponseMessage httpResponse = null;
                try
                {
                   

                    if (method.Method == "GET")
                    {
                        HttpRequestMessage httpRequest = new HttpRequestMessage
                        {
                            Method = method,
                            RequestUri = new Uri(UrlBase + endPoint),
                    };
                        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                      

                     httpResponse = await client.SendAsync(httpRequest);
                    }
                    else
                    {
                        HttpRequestMessage httpRequest = new HttpRequestMessage
                        {
                            Method = method,
                            RequestUri = new Uri(UrlBase + endPoint),
                            Content = httpContent,
                        };
                        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        httpResponse = await client.SendAsync(httpRequest);
                    }

                    switch (httpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            return await httpResponse.Content.ReadAsStringAsync();
                        case System.Net.HttpStatusCode.InternalServerError:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias\"}";
                        case System.Net.HttpStatusCode.ServiceUnavailable:
                            return "{\"error\": \"Servicio temporalmente no disponible contacte al Administrador, disculpe las molestias\"}";
                        case System.Net.HttpStatusCode.Unauthorized:
                            return "{\"error\": \"Sesión expírada o no cuenta con la autorización \"}";
                        default:
                            return await httpResponse.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception e)
                {
                    return "{\"error\": \"Servicio no disponible contacte al Administrador\"}";
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }
                }
            }
        }

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
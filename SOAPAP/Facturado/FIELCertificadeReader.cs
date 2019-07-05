using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using OpenSSL;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;


namespace SOAPAP.Facturado
{
    public class FIELCertificadeReader
    {
        public byte[] GetCertificateSerial(string CertificatePath)
        {
            X509Certificate2 objCert = new X509Certificate2(CertificatePath);
            return objCert.GetSerialNumber();
        }
        public Dictionary<string, string> GetCertificateData(string CertificatePath)
        {
            X509Certificate2 objCert = new X509Certificate2(CertificatePath);
            Dictionary<string, string> Data = new Dictionary<string, string>();

            Data.Add("Subject", objCert.Subject);
            Data.Add("Issuer", objCert.Issuer);
            Data.Add("NotBefore", objCert.NotBefore.ToString());
            Data.Add("NotAfter", objCert.NotAfter.ToString());
            Data.Add("KeySize", objCert.PublicKey.Key.KeySize.ToString());
            Data.Add("SerialNumber", objCert.SerialNumber);
            Data.Add("SerialNumberString", objCert.GetSerialNumberString());
            Data.Add("Thumbprint", objCert.Thumbprint);
            Data.Add("Type", objCert.GetType().ToString());

            int i = 1;
            foreach (X509Extension objExt in objCert.Extensions)
            {
                Data.Add("Ext" + i.ToString() + "Oid.FriendlyName", objExt.Oid.FriendlyName);
                Data.Add("Ext" + i.ToString() + "Oid.Value", objExt.Oid.Value);

                if (objExt.Oid.FriendlyName == "Key Usage")
                {
                    X509KeyUsageExtension ext = (X509KeyUsageExtension)objExt;
                    Data.Add("Ext" + i.ToString() + "KeyUsages", ext.KeyUsages.ToString());
                }

                if (objExt.Oid.FriendlyName == "Basic Constraints")
                {
                    X509BasicConstraintsExtension ext = (X509BasicConstraintsExtension)objExt;
                    Data.Add("Ext" + i.ToString() + "CertificateAuthority", ext.CertificateAuthority.ToString());
                    Data.Add("Ext" + i.ToString() + "HasPathLengthConstraint", ext.HasPathLengthConstraint.ToString());
                    Data.Add("Ext" + i.ToString() + "PathLengthConstraint", ext.PathLengthConstraint.ToString());
                }

                if (objExt.Oid.FriendlyName == "Subject Key Identifier")
                {
                    X509SubjectKeyIdentifierExtension ext = (X509SubjectKeyIdentifierExtension)objExt;
                    Data.Add("Ext" + i.ToString() + "SubjectKeyIdentifier", ext.SubjectKeyIdentifier.ToString());
                }

                int j = 1;
                if (objExt.Oid.FriendlyName == "Enhanced Key Usage") //2.5.29.37
                {
                    X509EnhancedKeyUsageExtension ext = (X509EnhancedKeyUsageExtension)objExt;
                    OidCollection objOids = ext.EnhancedKeyUsages;
                    foreach (Oid oid in objOids)
                    {
                        Data.Add("Ext" + i.ToString() + "Oid" + j.ToString() + "FriendlyName", oid.FriendlyName.ToString());
                        Data.Add("Ext" + i.ToString() + "Oid" + j.ToString() + "Value", oid.Value.ToString());
                        j++;
                    }
                }
                i++;
            }
            return Data;
        }
        public Dictionary<string, string> ValidateCertificate(string CertificatePath)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            X509Certificate2 objCert = new X509Certificate2(CertificatePath);

            X509Chain objChain = new X509Chain();

            //Verifico toda la cadena de revocación
            objChain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
            objChain.ChainPolicy.RevocationMode = X509RevocationMode.Online;

            //Timeout para las listas de revocación
            objChain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 0, 30);

            //Verificar todo
            objChain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

            //Se puede cambiar la fecha de verificación
            //objChain.ChainPolicy.VerificationTime = new DateTime(1999, 1, 1);

            objChain.Build(objCert);

            if (objChain.ChainStatus.Length != 0)
                foreach (X509ChainStatus objChainStatus in objChain.ChainStatus)
                    data.Add(objChainStatus.StatusInformation, objChainStatus.Status.ToString());
            else
                data.Add("200", "Success");
            return data;
        }
        public bool IsValidByDate(string CertificatePath)
        {
            X509Certificate2 objCert = new X509Certificate2(CertificatePath);
            if (DateTime.Now >= objCert.NotBefore && DateTime.Now <= objCert.NotAfter)
                return true;
            return false;
        }
        public bool IsPasswordValid(string KeyFilePath, string Password)
        {
            opensslkey _opensslkey = new opensslkey();
            RSACryptoServiceProvider _RSACryptoServiceProvider = _opensslkey.OpenKeyFile(KeyFilePath, Password);
            return _RSACryptoServiceProvider != null;
        }
        public byte[] Sign(string Contract, string KeyPath, string Code)
        {
            try
            {
                RSACryptoServiceProvider _RSACryptoServiceProvider = (new opensslkey()).OpenKeyFile(KeyPath, Code); //PKCS8Key pkcs8
                SHA1Managed sha1 = new SHA1Managed();
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(Contract));
                return _RSACryptoServiceProvider.SignHash(hash, CryptoConfig.MapNameToOID("SHA1withRSA"));

            }
            catch (CryptographicException cex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public byte[] Sign256(string Contract, string KeyPath, string Code)
        {
            try
            {
                RSACryptoServiceProvider _RSACryptoServiceProvider = (new opensslkey()).OpenKeyFile(KeyPath, Code); //PKCS8Key pkcs8
                SHA256Managed sha256 = new SHA256Managed();
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(Contract));
                return _RSACryptoServiceProvider.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));

            }
            catch (CryptographicException cex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool VerifySign(string Contract, byte[] Signature, string CertificatePath)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(CertificatePath);
                RSACryptoServiceProvider _RSACryptoServiceProvider = (RSACryptoServiceProvider)cert.PublicKey.Key;
                SHA1Managed sha1 = new SHA1Managed();
                byte[] data = Encoding.UTF8.GetBytes(Contract);
                byte[] hash = sha1.ComputeHash(data);
                return _RSACryptoServiceProvider.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1withRSA"), Signature);
            }
            catch (CryptographicException cex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool VerifySign256(string Contract, byte[] Signature, string CertificatePath)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(CertificatePath);
                RSACryptoServiceProvider _RSACryptoServiceProvider = (RSACryptoServiceProvider)cert.PublicKey.Key;
                SHA256Managed sha256 = new SHA256Managed();
                byte[] data = Encoding.UTF8.GetBytes(Contract);
                byte[] hash = sha256.ComputeHash(data);
                return _RSACryptoServiceProvider.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), Signature);
            }
            catch (CryptographicException cex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #region Private
        private string FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return Encoding.ASCII.GetString(raw);
        }
        private byte[] ReadFile(string FileName)
        {
            FileStream f = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }
        #endregion
    }
}

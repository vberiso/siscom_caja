using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace SOAPAP.Facturado
{
    class Signer
    {
        public void SignXMLFile(string P12CertificatePath, string Secure, string InputXML, string OutputXML)
        {
            try
            {
                // Create a new XML document.
                XmlDocument xmlDoc = new XmlDocument();

                X509Certificate2 uidCert = new X509Certificate2(P12CertificatePath, Secure, X509KeyStorageFlags.DefaultKeySet);

                // Load an XML file into the XmlDocument object.
                xmlDoc.Load(InputXML);
                xmlDoc.PreserveWhitespace = true;

                // Sign the XML document. 
                SignXml(xmlDoc, uidCert);

                Console.WriteLine("XML file signed.");

                // Save the document.
                xmlDoc.Save(OutputXML);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                System.Console.ReadLine();
            }
        }

        // Sign an XML file.  
        // This document cannot be verified unless the verifying  
        // code has the key with which it was signed. 
        private void SignXml(XmlDocument xmlDoc, X509Certificate2 uidCert)
        {

            RSACryptoServiceProvider rsaKey = (RSACryptoServiceProvider)uidCert.PrivateKey;


            // Check arguments. 
            if (xmlDoc == null)
                throw new ArgumentException("Missing xmlDoc to sing");
            if (rsaKey == null)
                throw new ArgumentException("Missing Private Key");

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc);

            // Add the key to the SignedXml document.
            signedXml.SigningKey = rsaKey;


            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);


            // Add an RSAKeyValue KeyInfo (optional; helps recipient find key to validate).
            KeyInfo keyInfo = new KeyInfo();

            KeyInfoX509Data clause = new KeyInfoX509Data();
            clause.AddSubjectName(uidCert.Subject);
            clause.AddCertificate(uidCert);
            keyInfo.AddClause(clause);
            signedXml.KeyInfo = keyInfo;

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save 
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            System.Console.WriteLine(signedXml.GetXml().InnerXml);

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        }
    }
}

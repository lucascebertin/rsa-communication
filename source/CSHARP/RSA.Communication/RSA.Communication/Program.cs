using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RSA.Communication
{
    public class Program
    {
        public const string CERTIFICATES_PATH = "Certificates\\";
        public const string CRT_FILE = "certificate.crt";
        public const string PFX_FILE = "certificate.pfx";
        public const string PASSWORD_PRIVATE_KEY = "123456";
        public const string PUB_KEY_PATH_EXPORT = "Certificates\\publickey.net.xml";
        public const string PRV_KEY_PATH_EXPORT = "Certificates\\privatekey.net.xml";

        static void CreateXmlKeyPair()
        {
            var certificatePath = Path.Combine(CERTIFICATES_PATH, CRT_FILE);
            var pfxPath = Path.Combine(CERTIFICATES_PATH, PFX_FILE);

            var pubCertificate = new X509Certificate2(certificatePath, PASSWORD_PRIVATE_KEY, X509KeyStorageFlags.Exportable);
            var publicKey = (RSACryptoServiceProvider)pubCertificate.PublicKey.Key;

            var privCertificate = new X509Certificate2(pfxPath, PASSWORD_PRIVATE_KEY, X509KeyStorageFlags.Exportable);
            var privateKey = (RSACryptoServiceProvider)privCertificate.PrivateKey;

            File.WriteAllText(PUB_KEY_PATH_EXPORT, publicKey.ToXmlString(false));
            Console.WriteLine("PUBLIC KEY GENERATED...");

            File.WriteAllText(PRV_KEY_PATH_EXPORT, privateKey.ToXmlString(true));
            Console.WriteLine("PRIVATE KEY GENERATED...");
        }

        static RSACryptoServiceProvider InitializeProvider(string xmlFile)
        {
            var rsaProvider = new RSACryptoServiceProvider();
            var keyContent = File.ReadAllText(xmlFile);
            rsaProvider.FromXmlString(keyContent);
            return rsaProvider;
        }

        static string Encrypt(string xmlFile, string message)
        {
            var rsaProvider = InitializeProvider(xmlFile);

            var encrypted = rsaProvider.Encrypt(
                Encoding.Default.GetBytes(message),
                true
            );

            return Convert.ToBase64String(encrypted);
        }

        static string Decrypt(string xmlFile, string message)
        {
            var rsaProvider = InitializeProvider(xmlFile);

            var decrypted = rsaProvider.Decrypt(
                Convert.FromBase64String(message),
                true
            );

            return Encoding.Default.GetString(decrypted);
        }

        static void Main(string[] args)
        {
            if (!File.Exists(PUB_KEY_PATH_EXPORT) || !File.Exists(PRV_KEY_PATH_EXPORT))
            {
                Console.WriteLine("PUB/PRV FILES NOT FOUND... THEY WILL BE CREATEAD RIGHT NOW.");
                CreateXmlKeyPair();
                Console.WriteLine("COPY ALL XMLS TO THE PHP FOLDER!");
            }

            var encrypted = Encrypt(PUB_KEY_PATH_EXPORT, "TEST");
            Console.Write("ENCRYPTED MESSAGE: {0}{1}", encrypted, Environment.NewLine);

            var decrypted = Decrypt(PRV_KEY_PATH_EXPORT, encrypted);
            Console.Write("DECRYPTED MESSAGE: {0}{1}", decrypted, Environment.NewLine);

            
        }
    }
}

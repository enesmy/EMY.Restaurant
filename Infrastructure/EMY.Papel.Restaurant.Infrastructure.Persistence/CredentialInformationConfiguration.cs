using EMY.Papel.Restaurant.Core.Domain.Common;
using Microsoft.Extensions.Configuration;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence
{
    public static partial class CredentialInformationConfiguration
    {

        static ConfigurationManager configuration
        {
            get
            {
                ConfigurationManager conf = new();
                conf.SetBasePath(Directory.GetCurrentDirectory());
                conf.AddJsonFile(ConfigFileLocation);
                return conf;
            }
        }

        public static string DisplayName
        {
            get
            {
                Crypt(CryptType.Decrypt);
                string result = configuration["DisplayName"];
                Crypt(CryptType.Encrypt);
                return result;
            }
        }
        public static string CredentialPassword
        {
            get
            {
                Crypt(CryptType.Decrypt);
                string result = configuration["CredentialPassword"];
                Crypt(CryptType.Encrypt);
                return result;
            }
        }
        public static string MailAdress
        {
            get
            {
                Crypt(CryptType.Decrypt);
                string result = configuration["MailAdress"];
                Crypt(CryptType.Encrypt);
                return result;
            }
        }
        public static string SmtpServer
        {
            get
            {
                Crypt(CryptType.Decrypt);
                string result = configuration["SmtpServer"];
                Crypt(CryptType.Encrypt);
                return result;
            }
        }
        public static int SmtpPort
        {
            get
            {
                Crypt(CryptType.Decrypt);
                int result = int.Parse(configuration["SmtpPort"]);
                Crypt(CryptType.Encrypt);
                return result;
            }
        }
        public static bool RequireSSL
        {
            get
            {
                Crypt(CryptType.Decrypt);
                bool result = bool.Parse(configuration["RequireSSL"]);
                Crypt(CryptType.Encrypt);
                return result;
            }
        }




        static string ConfigFileLocation
        {
            get
            {
                return Directory.GetCurrentDirectory() + "/MailConfig.json";
            }
        }
        static void Crypt(CryptType type)
        {
            if (System.IO.File.Exists(ConfigFileLocation))
                if (type == CryptType.Encrypt)
                    System.IO.File.Encrypt(ConfigFileLocation);
                else
                    System.IO.File.Decrypt(ConfigFileLocation);
        }
    }

}

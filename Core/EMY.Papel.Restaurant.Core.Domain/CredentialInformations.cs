using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMY.Papel.Restaurant.Core.Domain
{
    public class CredentialInformations
    {
        public string CredentialPassword { get; set; }
        public string DisplayName { get; set; }
        public string MailAdress { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool RequireSSL { get; set; }
    }
}

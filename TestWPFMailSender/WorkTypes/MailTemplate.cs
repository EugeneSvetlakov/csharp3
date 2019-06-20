using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPFMailSender.WorkTypes
{
    

    class MailTemplate
    {
        string id;
        string subject;
        string message;

        public MailTemplate(string id, string subject, string message)
        {
            this.id = id;
            this.subject = subject;
            this.message = message;
        }
    }
}

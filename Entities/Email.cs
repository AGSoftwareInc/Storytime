using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Entities
{
    public class Email
    {
        // constants
        private const string HtmlEmailHeader = "<html><head><title></title></head><body style='font-family:arial; font-size:14px;'>";
        private const string HtmlEmailFooter = "</body></html>";

        // properties
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        private string EmailHost { get; set; }

        // constructor
        public Email(string EmailHost)
        {
            this.EmailHost = EmailHost;
            To = new List<string>();
            CC = new List<string>();
            BCC = new List<string>();
        }

        // send
        public void Send()
        {
            MailMessage message = new MailMessage();

            foreach (var x in To)
            {
                message.To.Add(x);
            }
            foreach (var x in CC)
            {
                message.CC.Add(x);
            }
            foreach (var x in BCC)
            {
                message.Bcc.Add(x);
            }

            message.Subject = Subject;
            message.Body = string.Concat(HtmlEmailHeader, Body, HtmlEmailFooter);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.From = new MailAddress(From);
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient(this.EmailHost);

            client.Send(message);
        }
    }
}

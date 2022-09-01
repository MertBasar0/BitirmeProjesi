using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dto_s
{
    public class MailDataDTO 
    {
        //alıcı

        public List<string> To { get; set; }

        public List<string>? Bcc { get; set; }

        public List<string>? Cc { get; set; }

        //gönderen

        public string? From { get; set; }

        public string? DisplayName { get; set; }

        public string? ReplyTo { get; set; }

        public string? ReplyToName { get; set; }

        //İçerik

        public string Subject { get; set; }

        public string? Body { get; set; }

        public MailDataDTO()
        {

        }

        public MailDataDTO(List<string> to, string subject)
        {
            To = to;
            Subject = subject;
        }


        public MailDataDTO(List<string> to, string subject, string? body = null, string? from = null, string? displayName = null, string? replyTo = null, string? replyToName = null, List<string>? bcc = null, List<string>? cc = null)
        {
            // Receiver
            To = to;
            Bcc = bcc ?? new List<string>();
            Cc = cc ?? new List<string>();
            // Sender
            From = from;
            DisplayName = displayName;
            ReplyTo = replyTo;
            ReplyToName = replyToName;

            // Content
            Subject = subject;
            Body = body;
        }
    }
}

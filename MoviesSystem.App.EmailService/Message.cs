using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesSystem.App.EmailService
{
    public class Message
    {
        public List<MailboxAddress> Recipients { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> recipients, string subject, string content)
        {
            Recipients = new List<MailboxAddress>();

            Recipients.AddRange(recipients.Select(recipient => new MailboxAddress(recipient)));
            Subject = subject;
            Content = content;
        }
    }
}

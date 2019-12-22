using System.Collections.Generic;

namespace Bliki.Data
{
    public class EmailMessage
    {
        public EmailMessage(string subject, string content)
        {
            Subject = subject;
            Content = content;
            ToAddresses = new List<EmailAddress>();
            FromAddresses = new List<EmailAddress>();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> FromAddresses { get; set; }
        public string Subject { get; }
        public string Content { get; }
    }
}

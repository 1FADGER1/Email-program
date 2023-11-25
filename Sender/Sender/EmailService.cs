using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sender
{
    internal class EmailService
    {
        string subject = "subject!";
        string from = "from email";
        string to = "to email";
        string smtpHost = "smtp.gmail.com";
        string smtpUsername = "your email address";
        string smtpPassword = "your password in 'app passwords'"; //account google -> search ->  application passwords
        public async Task SendingEmail(string text, ObservableCollection<string> attachments)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("_", from));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = text;
            foreach (string attachmant in attachments)
            {
                //message.Attachments.Add(new Attachment(attachmant));
                bodyBuilder.Attachments.Add(attachmant);
            }
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(smtpHost, 465, true);
                    await client.AuthenticateAsync(smtpUsername, smtpPassword);
                    await client.SendAsync(message);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}

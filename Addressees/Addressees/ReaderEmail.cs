using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace Addressees
{
    internal class ReaderEmail
    {
        string subject = "Subject";
        string smtpHost = "imap.gmail.com";
        string smtpUsername = "your email address";
        string smtpPassword = "your password in 'app passwords'";//account google -> search ->  application passwords
        public async Task ReadingEmail(TextBox text)
        {
            using(ImapClient  client = new ImapClient())
            {
                try
                {
                    await client.ConnectAsync(smtpHost, 993, true);
                    await client.AuthenticateAsync(smtpUsername, smtpPassword);
                    await client.Inbox.OpenAsync(FolderAccess.ReadOnly);
                    var query = SearchQuery.SubjectContains(subject);
                    var messages = client.Inbox.Search(query);
                    foreach (var messageId in messages)
                    {
                        var message = client.Inbox.GetMessage(messageId);
                        string subject = message.Subject ?? string.Empty;
                        string textBody = message.TextBody ?? string.Empty;
                        foreach (var attachment in message.Attachments)
                        {
                            if (attachment is MimePart mimePart)
                            {
                                using (var stream = new MemoryStream())
                                {
                                    mimePart.Content.DecodeTo(stream);
                                    var content = Encoding.UTF8.GetString(stream.ToArray());
                                    text.Text += $"Subject: {subject}\nText: {textBody}\n Text in files: {content}\n";
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
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

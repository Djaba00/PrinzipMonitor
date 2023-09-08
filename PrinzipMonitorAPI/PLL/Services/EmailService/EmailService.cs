using System.Net.Mail;
using System.Net;

namespace PrinzipMonitorService.PLL.Services.EmailService
{
    public static class EmailService
    {
        public static async Task<bool> SendEmailAsync(string email, int? oldPrice, int? newPrice)
        {
            const string fromPassword = EmailAccounts.GmailPass;

            var fromAddress = new MailAddress("yatytdlatestov@gmail.com", "FlatQuestion");

            var toAddress = new MailAddress(email);

            var subject = $"Старая цена {oldPrice} руб., новая цена {newPrice} руб.";

            var smtp = new SmtpClient
            {
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
            })
            {
                await smtp.SendMailAsync(message);
            }

            return true;
        }
    }
}

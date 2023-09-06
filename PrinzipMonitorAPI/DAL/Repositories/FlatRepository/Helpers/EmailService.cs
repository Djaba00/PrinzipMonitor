using System.Net.Mail;
using System.Net;

namespace PrinzipMonitorService.DAL.Repositories.FlatRepository.Helpers
{
    public static class EmailService
    {
        public static async Task SendEmail(string email, decimal oldPrice, decimal newPrice)
        {
            MailAddress from = new MailAddress("yatytdlatestov@gmail.com", "FlatQuestion");

            MailAddress to = new MailAddress(email);

            MailMessage message = new MailMessage(from, to);

            message.Subject = $"Старая цена {oldPrice} руб., новая цена {newPrice} руб.";

            SmtpClient smtpClient = new SmtpClient("smpt.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("yatytdlatestov@gmail.com", "testsForTests");
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(message);
        }
    }
}

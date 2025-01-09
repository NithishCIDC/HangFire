using MimeKit;
using MailKit.Net.Smtp;

namespace HangFire.Application.EmailService
{
    public class EmailService : IEmailService
    {

        public void EmailSend(string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("The Boys", "theboyscidc@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "ECOB.org project";

            var bodyBuilder = new BodyBuilder { HtmlBody = "Hi, I am hariprakash" };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate("theboyscidc@gmail.com", "hhilghxgjktiscgx");
            client.Send(message);
            client.Disconnect(true);
            Console.WriteLine("Email Sent");
        }
    }
}

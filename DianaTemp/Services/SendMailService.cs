using System.Net;
using System.Net.Mail;

namespace DianaTemp.Services
{
    public static class SendMailService
    {
        public static void SendMail(string to, string url)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("bd6gq7p0j@code.edu.az", "fkjo fncy zgts ubdr");
                client.EnableSsl = true;

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("bd6gq7p0j@code.edu.az"),
                    Subject = "Welcome to Diana",
                    Body = $"<h1> Hello to our website, click the link: {url} </h1>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                client.Send(mailMessage);
            }
        }

        public static void SendWelcomeMail(string to, string confirmationUrl)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("bd6gq7p0j@code.edu.az", "fkjo fncy zgts ubdr"); // Use App Password for Gmail
                client.EnableSsl = true;

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("your-email@gmail.com"),
                    Subject = "Welcome to Diana - Confirm Your Email",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                mailMessage.Body = $"<h1>Welcome to Diana!</h1>" +
                                   $"<p>Thank you for subscribing. Please click the link below to confirm your email:</p>" +
                                   $"<a href='{confirmationUrl}'>Confirm Email</a>";

                client.Send(mailMessage);
            }
        }
    }
}

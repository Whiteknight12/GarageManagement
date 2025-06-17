using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GarageManagement.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpOptions _options = new();

        public SmtpEmailService(IOptions<SmtpOptions> opts)
        {

        }
            

        public async Task SendPasswordResetAsync(string toEmail, string link)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("MyGara", _options.FromAddress));
            msg.To.Add(MailboxAddress.Parse(toEmail));
            msg.Subject = "Đặt lại mật khẩu";
            msg.Body = new TextPart("html")
            {
                Text = $"Click vào <a href=\"{link}\">đây</a> để xem mật khẩu."
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_options.Username, _options.Password);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
        }
    }
    public class SmtpOptions
    {
        public string Host { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public bool UseSsl { get; set; } = true;
        public string Username { get; set; } = "tophuquy2712@gmail.com";
        public string Password { get; set; } = "";
        public string FromAddress { get; set; } = "tophuquy2712@gmail.com"; 
    }
}

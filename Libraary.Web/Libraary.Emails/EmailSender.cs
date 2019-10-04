namespace Libraary.Emails
{
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Options;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System.Threading.Tasks;

    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            this.Options = optionsAccessor.Value;
        }
        public AuthMessageSenderOptions Options { get; }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return this.Execute(this.Options.SendGridKey, subject, htmlMessage, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("libraary@abv.bg", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}

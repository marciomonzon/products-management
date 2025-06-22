using EmailNotificationWorker.Models;

namespace EmailNotificationWorker.Services
{
    public class SendEmailService
    {
        private readonly EmailSettings _settings;

        public SendEmailService()
        {
            _settings = new EmailSettings();
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            // simulating email sending
            Console.WriteLine("Sending e-mail...");
            Console.WriteLine("E-mail sent!");
        }
    }
}

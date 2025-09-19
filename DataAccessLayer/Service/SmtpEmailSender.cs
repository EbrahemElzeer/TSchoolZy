using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repositey.Configuration;

namespace Application.Service
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtp;

        public SmtpEmailSender(IOptions<SmtpSettings> smtpOptions)
        {
            _smtp = smtpOptions.Value;
        }


        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_smtp.Host)
            {
                Port = _smtp.Port,
                Credentials = new NetworkCredential(_smtp.Email, _smtp.Password),
                EnableSsl = true
            };


            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtp.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }

}

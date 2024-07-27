using AdvPOS.Models;
using AdvPOS.Models.EmailConfigViewModel;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AdvPOS.Services
{
    public class EmailSender : IEmailSender
    {
        private IFunctional _functional { get; }
        private readonly ICommon _iCommon;

        public EmailSender(IFunctional functional, ICommon iCommon)
        {
            _functional = functional;
            _iCommon = iCommon;
        }
        public async Task<Task> SendEmailAsyncOLD(string email, string subject, string message)
        {
            try
            {
                //sendgrid is become default
                SendGridSetting _sendGridOptions = await _iCommon.GetSendGridEmailSetting();
                if (_sendGridOptions.IsDefault)
                {
                    _functional.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey,
                                                        _sendGridOptions.FromEmail,
                                                        _sendGridOptions.FromFullName,
                                                        subject,
                                                        message,
                                                        email)
                                                        .Wait();
                }

                //smtp is become default
                SMTPEmailSetting _smtpOptions = await _iCommon.GetSMTPEmailSetting();
                if (_smtpOptions.IsDefault)
                {
                    _functional.SendEmailByGmailAsync(_smtpOptions.FromEmail,
                                                _smtpOptions.FromFullName,
                                                subject,
                                                message,
                                                email,
                                                email,
                                                _smtpOptions.UserName,
                                                _smtpOptions.Password,
                                                _smtpOptions.Host,
                                                _smtpOptions.Port,
                                                _smtpOptions.IsSSL)
                                                .Wait();
                }
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Task> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                EmailConfig _smtpOptions = await _iCommon.GetEmailConfig();
                _functional.SendEmailByGmailAsync(_smtpOptions.Email,
                                                _smtpOptions.SenderFullName,
                                                subject,
                                                message,
                                                email,
                                                email,
                                                _smtpOptions.Email,
                                                _smtpOptions.Password,
                                                _smtpOptions.Hostname,
                                                _smtpOptions.Port,
                                                _smtpOptions.SSLEnabled)
                                                .Wait();
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Task> SendEmailByGmailAsync(SendEmailViewModel vm)
        {
            MailMessage _MailMessage = new();
            _MailMessage.From = new MailAddress(vm.SenderEmail, vm.SenderFullName);
            _MailMessage.To.Add(new MailAddress(vm.ReceiverEmail, vm.ReceiverFullName));
            _MailMessage.Subject = vm.Subject;
            _MailMessage.Body = vm.Body;
            _MailMessage.IsBodyHtml = true;

            _MailMessage.Attachments.Add(new Attachment(vm.FileStream, vm.FileName, vm.FileType));

            using (var smtp = new SmtpClient())
            {
                smtp.UseDefaultCredentials = false;
                var credential = new NetworkCredential
                {
                    UserName = vm.UserName,
                    Password = "yjlnqcpeqbtiowoa"
                };
                smtp.Credentials = credential;
                smtp.Host = vm.Host;
                smtp.Port = vm.Port;
                smtp.EnableSsl = vm.IsSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(_MailMessage);
            }

            return Task.CompletedTask;
        }
    }
}

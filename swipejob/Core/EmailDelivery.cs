using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using RazorEngine;
using RazorEngine.Templating;
using SendGrid;
using Encoding = System.Text.Encoding;
using SwipeJob.Utility;

namespace SwipeJob.Core
{
    public class EmailDelivery
    {
        public static void Config()
        {
            Engine.Razor.AddTemplate("JobSeekerRegisterActivation.htm", GetEmailTemplate("JobSeekerRegisterActivation.htm"));
            Engine.Razor.Compile("JobSeekerRegisterActivation.htm");

            Engine.Razor.AddTemplate("JobSeekerRegisterCompleted.htm", GetEmailTemplate("JobSeekerRegisterCompleted.htm"));
            Engine.Razor.Compile("JobSeekerRegisterCompleted.htm");

            Engine.Razor.AddTemplate("EmployerRegisterCompleted.htm", GetEmailTemplate("EmployerRegisterCompleted.htm"));
            Engine.Razor.Compile("EmployerRegisterCompleted.htm");

            Engine.Razor.AddTemplate("ForgotPassword.htm", GetEmailTemplate("ForgotPassword.htm"));
            Engine.Razor.Compile("ForgotPassword.htm");

            Engine.Razor.AddTemplate("JobSeekerAppliedJob.htm", GetEmailTemplate("JobSeekerAppliedJob.htm"));
            Engine.Razor.Compile("JobSeekerAppliedJob.htm");
        }

        public static async Task SendJobSeekerRegisterActivation(string email, string confirmationCode)
        {
            var model = new
            {
                WebUrl = ConfigurationManager.AppSettings["Host"],
                ConfirmationCode = confirmationCode
            };

            await SendEmail(new List<string> { email }, null, null, "[SwipeJob] Account activation", "JobSeekerRegisterActivation.htm", model);
        }

        public static async Task SendJobSeekerAppliedJob(string employerEmail, string jobSeekerName,string jobName, Guid jobSeekerId)
        {
            var model = new
            {
                WebUrl = ConfigurationManager.AppSettings["Host"],
                JobSeekerName = jobSeekerName,
                JobName = jobName,
                JobSeekerId = jobSeekerId
            };

            await SendEmail(new List<string> { employerEmail }, null, null, "[SwipeJob] JobSeeker applied job", "JobSeekerAppliedJob.htm", model);
        }

        public static async Task SendJobSeekerRegisterCompleted(string email)
        {
            var model = new
            {
                WebUrl = ConfigurationManager.AppSettings["Host"]
            };

            await SendEmail(new List<string> { email }, null, null, "[SwipeJob] Register successful", "JobSeekerRegisterCompleted.htm", model);
        }

        public static async Task SendEmployerRegisterCompleted(string email, string password)
        {
            var model = new
            {
                WebUrl = ConfigurationManager.AppSettings["Host"],
                Password = password
                
            };

            await SendEmail(new List<string> { email }, null, null, "[SwipeJob] Register successful", "EmployerRegisterCompleted.htm", model);
        }

        public static async Task SendForgotPasswordRequest(string email, string confirmationCode)
        {
            var model = new
            {
                WebUrl = ConfigurationManager.AppSettings["Host"],
                ConfirmationCode = confirmationCode
            };

            await SendEmail(new List<string> { email }, null, null, "[SwipeJob] Forgot password request", "ForgotPassword.htm", model);
        }


        public static async Task SendConfirm(string email, string password)
        {
            var model = new
            {
                WebUrl = ConfigurationManager.AppSettings["Host"],
                Password = password

            };

            await SendEmail(new List<string> { email }, null, null, "[SwipeJob] Register successful", "EmployerRegisterCompleted.htm", model);
        }

        private static async Task SendEmail(IEnumerable<string> tos, IEnumerable<string> cc, IEnumerable<string> bcc, string subject, string emailTemplateName, object model)
        {
            try
            {
                var body = Engine.Razor.Run(emailTemplateName, null, model);
                await SendGridMail(tos, cc, bcc, subject, body);
            }
            catch (Exception ex)
            {
                LoggingHelper.Log(ex);
            }
        }

        private static void SendMail(IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, string subject, string content)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                MailMessage email = new MailMessage
                {
                    Subject = subject,
                    Body = content,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true
                };

                if (to != null)
                {
                    foreach (string e in to)
                    {
                        email.To.Add(e);
                    }
                }

                if (cc != null)
                {
                    foreach (string e in cc)
                    {
                        email.CC.Add(e);
                    }
                }

                if (bcc != null)
                {
                    foreach (string e in bcc)
                    {
                        email.Bcc.Add(e);
                    }
                }

                smtpClient.Send(email);
            }
        }

        private static async Task SendGridMail(IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, string subject, string content)
        {
            SendGridMessage msg = new SendGridMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["SendGridSenderEmail"],
                                       ConfigurationManager.AppSettings["SendGridSenderName"]);
            msg.Subject = subject;
            foreach (string recipient in to)
            {
                msg.AddTo(recipient);
            }

            msg.Html = content;
            Web web = new Web(ConfigurationManager.AppSettings["SendGridApiKey"]);
            await web.DeliverAsync(msg);
        }

        private static string GetEmailTemplate(string emailTemplateName)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SwipeJob.Core.EmailTemplate." + emailTemplateName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}

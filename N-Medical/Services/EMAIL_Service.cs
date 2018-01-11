using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace N_Medical.Services
{
    public class EMAIL_Service
    {
        public void email(string emailFrom, string emailTo, string subject, string body, string password)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("mail.bdms.co.th", 25))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch(Exception x)
            {
                string a = x.Message.ToString();
                string b = null;
            }
            //using (MailMessage mail = new MailMessage())
            //{
            //    mail.From = new MailAddress(emailFrom);
            //    mail.To.Add(emailTo);
            //    mail.Subject = subject;
            //    mail.Body = body;
            //    mail.IsBodyHtml = true;
            //    // Can set to false, if you are sending pure text.

            //    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
            //    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

            //    using (SmtpClient smtp = new SmtpClient("mail.bdms.co.th", 25))
            //    {
            //        smtp.Credentials = new NetworkCredential(emailFrom, password);
            //        smtp.EnableSsl = true;
            //        smtp.Send(mail);
            //    }
            //}
        }
    }
}
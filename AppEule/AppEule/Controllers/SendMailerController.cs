
using System.Net.Mail;
using System.Web.Mvc; 

namespace AppEule.Controllers
{
    /// <summary>
    /// The controller for sending email notifications
    /// </summary>
    [HandleError()]
    public class SendMailerController : BaseController
    {
        //
        // GET: /SendMailer/ 
        public ActionResult Index()
        {
            return View();
        } 
        [HttpPost]
        public ViewResult Index(AppEule.Models.MailModel _objModelMail)
       {
            if (ModelState.IsValid)
            {
                
                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "groupware.dom.htw-dresden.de";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("tnicolaisen2", "kemeti1988");// Enter senders User name and password
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("Index", _objModelMail);
            }
            else
            {
                return View();
            }
        }
    }
}
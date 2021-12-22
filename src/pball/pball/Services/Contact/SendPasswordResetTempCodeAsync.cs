using System.Net.Mail;

namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<bool>> SendPasswordResetTempCodeAsync(LoginEmailModel loginEmailModel)
    {
        if (string.IsNullOrWhiteSpace(loginEmailModel.LoginEmail))
        {
            return await Task.FromResult(false);
        }

        Contact? contact = (from c in db.Contacts
                            where c.LoginEmail == loginEmailModel.LoginEmail
                            select c).FirstOrDefault();

        if (contact != null)
        {
            return await Task.FromResult(false);
        }

        MailMessage mail = new MailMessage();

        mail.To.Add(Configuration["LoginEmail"]);

        mail.IsBodyHtml = true;

        SmtpClient myClient = new SmtpClient();

        myClient.Host = Configuration["SmtpHost"];
        myClient.Port = 587;

        myClient.Credentials = new NetworkCredential(
            Configuration["NetworkCredentialUserName"],
            Configuration["NetworkCredentialPassword"]);
        
        myClient.EnableSsl = true;

        string subject = PBallRes.TemporaryCodeToChangeYourPassword;

        StringBuilder msg = new StringBuilder();

        msg.AppendLine($"<h2>{ PBallRes.TemporaryCodeToChangeYourPassword }</h2>");
        msg.AppendLine($"<h4>{ PBallRes.DateIssued }: " + DateTime.Now + "</h4>");
        msg.AppendLine($"<br />");
        msg.AppendLine($"<br />");
        msg.AppendLine($"<h3>{ GenerateTempCode() }</h3>");
        msg.AppendLine($"<br>");
        msg.AppendLine($"<p>{ PBallRes.AutomaticEmail }</p>");

        mail.Subject = subject;
        mail.Body = msg.ToString();
        //myClient.Send(mail);

        return await Task.FromResult(Ok(true));
    }
}


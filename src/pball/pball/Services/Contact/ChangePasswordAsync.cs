using System.Net.Mail;

namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<bool>> ChangePasswordAsync(ChangePasswordModel passwordChangeModel)
    {
        if (string.IsNullOrWhiteSpace(passwordChangeModel.LoginEmail))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LoginEmail")));
        }

        if (string.IsNullOrWhiteSpace(passwordChangeModel.Password))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Password")));
        }

        if (string.IsNullOrWhiteSpace(passwordChangeModel.TempCode))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "TempCode")));
        }

        Contact? contact = (from c in db.Contacts
                            where c.LoginEmail == passwordChangeModel.LoginEmail
                            && c.ResetPasswordTempCode == passwordChangeModel.TempCode
                            select c).FirstOrDefault();

        if (contact != null)
        {
            contact.PasswordHash = ScrambleService.Scramble(passwordChangeModel.Password);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
            }
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

        string subject = $"{ PBallRes.YourPasswordHasBeenChanged }";

        StringBuilder msg = new StringBuilder();

        msg.AppendLine($"<h2>PBallRes.YourPasswordHasBeenChanged</h2>");
        msg.AppendLine($"<h3>" + DateTime.Now + "</h3>");

        msg.AppendLine($"<br>");
        msg.AppendLine($"<p>{ PBallRes.AutomaticEmail }</p>");

        mail.Subject = subject;
        mail.Body = msg.ToString();
        //myClient.Send(mail);

        return await Task.FromResult(Ok(true));
    }
}


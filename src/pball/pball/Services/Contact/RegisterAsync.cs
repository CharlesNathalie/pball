namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<Contact>> RegisterAsync(RegisterModel registerModel)
    {
        // no need
        //if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        //{
        //    return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        //}

        if (registerModel == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._ShouldNotBeNullOrEmpty, "registerModel")));
        }

        if (string.IsNullOrWhiteSpace(registerModel.LoginEmail))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LoginEmail")));
        }

        if (registerModel.LoginEmail.Length < 5 || registerModel.LoginEmail.Length > 255)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "5", "100")));
        }

        if (string.IsNullOrWhiteSpace(registerModel.FirstName))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "FirstName")));
        }

        if (registerModel.FirstName.Length > 100)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._MaxLengthIs_, "FirstName", "100")));
        }

        if (string.IsNullOrWhiteSpace(registerModel.LastName))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LastName")));
        }

        if (registerModel.LastName.Length > 100)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._MinLengthIs_, "LastName", "100")));
        }

        if (string.IsNullOrWhiteSpace(registerModel.Initial))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Initial")));
        }
        else
        {
            if (registerModel.Initial.Length > 50)
            {
                return await Task.FromResult(BadRequest(string.Format(PBallRes._MaxLengthIs_, "Initial", "50")));
            }
        }

        if (registerModel.PlayerLevel < 1.0f || registerModel.PlayerLevel > 5.0f)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0")));
        }

        if (string.IsNullOrWhiteSpace(registerModel.Password))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Password")));
        }

        Contact? contact = (from c in db.Contacts
                            where c.LoginEmail == registerModel.LoginEmail
                            select c).FirstOrDefault();

        if (contact == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._AlreadyTaken, registerModel.LoginEmail)));
        }

        if (string.IsNullOrWhiteSpace(registerModel.Initial))
        {
            contact = (from c in db.Contacts
                       where c.FirstName == registerModel.FirstName
                       && c.LastName == registerModel.LastName
                       select c).FirstOrDefault();

            if (contact != null)
            {
                string FullName = $"{ registerModel.FirstName } { registerModel.LastName }";

                return await Task.FromResult(BadRequest(string.Format(PBallRes._AlreadyTaken, FullName)));
            }
        }
        else
        {
            contact = (from c in db.Contacts
                       where c.FirstName == registerModel.FirstName
                       && c.LastName == registerModel.LastName
                       && c.Initial == registerModel.Initial
                       select c).FirstOrDefault();

            if (contact != null)
            {
                string Initial = registerModel.Initial.EndsWith(".") ? registerModel.Initial.Substring(0, registerModel.Initial.Length - 1) : registerModel.Initial;
                string FullName = $"{ registerModel.FirstName } { Initial } { registerModel.LastName }";

                return await Task.FromResult(BadRequest(string.Format(PBallRes._AlreadyTaken, FullName)));
            }
        }

        contact = new Contact()
        {
            LoginEmail = registerModel.LoginEmail,
            FirstName = registerModel.FirstName,
            LastName = registerModel.LastName,
            Initial = registerModel.Initial,
            PlayerLevel = registerModel.PlayerLevel,
            IsAdmin = false,
            PasswordHash = ScrambleService.Scramble(registerModel.Password),
            Token = "",
            Removed = false,
            ResetPasswordTempCode = "",
            LastUpdateDate_UTC = DateTime.UtcNow,
            LastUpdateContactID = -1,
        };

        db.Contacts?.Add(contact);
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        contact.LastUpdateContactID = contact.ContactID;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        contact.PasswordHash = "";
        contact.Token = "";

        return await Task.FromResult(Ok(contact));
    }
}


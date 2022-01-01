namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<Contact>> RegisterAsync(RegisterModel registerModel)
    {
        ErrRes errRes = new ErrRes();


        if (string.IsNullOrWhiteSpace(registerModel.LoginEmail))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LoginEmail"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (registerModel.LoginEmail.Length < 6 || registerModel.LoginEmail.Length > 255)
        {
            errRes.ErrList.Add(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "255"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (!IsValidEmail(registerModel.LoginEmail))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsNotAValidEmail, registerModel.LoginEmail));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(registerModel.FirstName))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "FirstName"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (registerModel.FirstName.Length > 100)
        {
            errRes.ErrList.Add(string.Format(PBallRes._MaxLengthIs_, "FirstName", "100"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(registerModel.LastName))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LastName"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (registerModel.LastName.Length > 100)
        {
            errRes.ErrList.Add(string.Format(PBallRes._MaxLengthIs_, "LastName", "100"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (!string.IsNullOrWhiteSpace(registerModel.Initial))
        {
            if (registerModel.Initial.Length > 50)
            {
                errRes.ErrList.Add(string.Format(PBallRes._MaxLengthIs_, "Initial", "50"));
                return await Task.FromResult(BadRequest(errRes));
            }
        }

        if (registerModel.PlayerLevel < 1.0f || registerModel.PlayerLevel > 5.0f)
        {
            errRes.ErrList.Add(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(registerModel.Password))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Password"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (registerModel.Password.Length > 50)
        {
            errRes.ErrList.Add(string.Format(PBallRes._MaxLengthIs_, "Password", "50"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contact = (from c in db.Contacts
                            where c.LoginEmail == registerModel.LoginEmail
                            select c).FirstOrDefault();

        if (contact != null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._AlreadyTaken, registerModel.LoginEmail));
            return await Task.FromResult(BadRequest(errRes));
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

                errRes.ErrList.Add(string.Format(PBallRes._AlreadyTaken, FullName));
                return await Task.FromResult(BadRequest(errRes));
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

                errRes.ErrList.Add(string.Format(PBallRes._AlreadyTaken, FullName));
                return await Task.FromResult(BadRequest(errRes));
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
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        contact.LastUpdateContactID = contact.ContactID;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact contactCreated = new Contact()
        {
            ContactID = contact.ContactID,
            FirstName = contact.FirstName,
            Initial = contact.Initial,
            LastName = contact.LastName,
            IsAdmin = contact.IsAdmin,
            LastUpdateContactID = contact.LastUpdateContactID,
            LastUpdateDate_UTC = contact.LastUpdateDate_UTC,
            LoginEmail = contact.LoginEmail,
            PasswordHash = "",
            PlayerLevel = contact.PlayerLevel,
            Removed = contact.Removed,
            ResetPasswordTempCode = contact.ResetPasswordTempCode,
            Token = "",
        };

        return await Task.FromResult(Ok(contactCreated));
    }
}


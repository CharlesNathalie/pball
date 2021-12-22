namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<Contact>> ModifyContactAsync(Contact contact)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        if (contact == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._ShouldNotBeNullOrEmpty, "contact")));
        }

        if (string.IsNullOrWhiteSpace(contact.LoginEmail))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LoginEmail")));
        }

        if (contact.LoginEmail.Length < 5 || contact.LoginEmail.Length > 255)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "5", "100")));
        }

        if (string.IsNullOrWhiteSpace(contact.FirstName))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "FirstName")));
        }

        if (contact.FirstName.Length > 100)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._MaxLengthIs_, "FirstName", "100")));
        }

        if (string.IsNullOrWhiteSpace(contact.LastName))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LastName")));
        }

        if (contact.LastName.Length > 100)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._MinLengthIs_, "LastName", "100")));
        }

        if (string.IsNullOrWhiteSpace(contact.Initial))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Initial")));
        }
        else
        {
            if (contact.Initial.Length > 50)
            {
                return await Task.FromResult(BadRequest(string.Format(PBallRes._MaxLengthIs_, "Initial", "50")));
            }
        }

        if (contact.PlayerLevel < 1.0f || contact.PlayerLevel > 5.0f)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0")));
        }

        Contact? contactToModify = (from c in db.Contacts
                                    where c.ContactID == contact.ContactID
                                    select c).FirstOrDefault();

        if (contactToModify == null)
        {
            return await Task.FromResult(BadRequest(String.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", contact.ContactID.ToString())));
        }

        Contact? contactAlreadyExist = (from c in db.Contacts
                                        where c.ContactID != contact.ContactID
                                        && c.LoginEmail == contact.LoginEmail
                                        select c).FirstOrDefault();

        if (contactAlreadyExist == null)
        {
            return await Task.FromResult(BadRequest(String.Format(PBallRes._AlreadyTaken, "LoginEmail")));
        }


        if (string.IsNullOrWhiteSpace(contactToModify.Initial))
        {
            contactAlreadyExist = (from c in db.Contacts
                                   where c.ContactID != contact.ContactID
                                   && c.FirstName == contact.FirstName
                                   && c.LastName == contact.LastName
                                   select c).FirstOrDefault();

            if (contactAlreadyExist == null)
            {
                string FullName = $"{ contact.FirstName } { contact.LastName }";

                return await Task.FromResult(BadRequest(string.Format(PBallRes._AlreadyTaken, FullName)));
            }

        }
        else
        {
            contactAlreadyExist = (from c in db.Contacts
                                   where c.ContactID != contact.ContactID
                                   && c.FirstName == contact.FirstName
                                   && c.LastName == contact.LastName
                                   && c.Initial == contact.Initial
                                   select c).FirstOrDefault();

            if (contactToModify == null)
            {
                string Initial = contact.Initial == null ? "" : contact.Initial.EndsWith(".") ? contact.Initial.Substring(0, contact.Initial.Length - 1) : contact.Initial;
                string FullName = $"{ contact.FirstName } { Initial } { contact.LastName }";

                return await Task.FromResult(BadRequest(string.Format(PBallRes._AlreadyTaken, FullName)));
            }

        }

        contactToModify.LoginEmail = contact.LoginEmail;
        contactToModify.FirstName = contact.FirstName;
        contactToModify.FirstName = contact.FirstName;
        contactToModify.LastName = contact.LastName;
        contactToModify.Initial = contact.Initial;
        contactToModify.PlayerLevel = contact.PlayerLevel;
        contactToModify.ResetPasswordTempCode = "";
        contactToModify.LastUpdateDate_UTC = DateTime.UtcNow;
        contactToModify.LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        return await Task.FromResult(contact);
    }
}


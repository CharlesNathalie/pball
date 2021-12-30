namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<Contact>> ModifyContactAsync(Contact contact)
    {
        ErrRes errRes = new ErrRes();

        if (LoggedInService.LoggedInContactInfo == null || LoggedInService.LoggedInContactInfo.LoggedInContact == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(contact.LoginEmail))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LoginEmail"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (contact.LoginEmail.Length < 6 || contact.LoginEmail.Length > 100)
        {
            errRes.ErrList.Add(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "100"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(contact.FirstName))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "FirstName"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (contact.FirstName.Length > 100)
        {
            errRes.ErrList.Add(string.Format(PBallRes._MaxLengthIs_, "FirstName", "100"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(contact.LastName))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LastName"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (contact.LastName.Length > 100)
        {
            errRes.ErrList.Add(string.Format(PBallRes._MaxLengthIs_, "LastName", "100"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (!string.IsNullOrWhiteSpace(contact.Initial))
        {
            if (contact.Initial.Length > 50)
            {
                errRes.ErrList.Add(string.Format(PBallRes._MaxLengthIs_, "Initial", "50"));
                return await Task.FromResult(BadRequest(errRes));
            }
        }

        if (contact.PlayerLevel < 1.0f || contact.PlayerLevel > 5.0f)
        {
            errRes.ErrList.Add(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contactToModify = (from c in db.Contacts
                                    where c.ContactID == contact.ContactID
                                    select c).FirstOrDefault();

        if (contactToModify == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", contact.ContactID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contactAlreadyExist = (from c in db.Contacts
                                        where c.ContactID != contact.ContactID
                                        && c.LoginEmail == contact.LoginEmail
                                        select c).FirstOrDefault();

        if (contactAlreadyExist != null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._AlreadyTaken, "LoginEmail"));
            return await Task.FromResult(BadRequest(errRes));
        }


        if (string.IsNullOrWhiteSpace(contactToModify.Initial))
        {
            contactAlreadyExist = (from c in db.Contacts
                                   where c.ContactID != contact.ContactID
                                   && c.FirstName == contact.FirstName
                                   && c.LastName == contact.LastName
                                   select c).FirstOrDefault();

            if (contactAlreadyExist != null)
            {
                string FullName = $"{ contact.FirstName } { contact.LastName }";

                errRes.ErrList.Add(string.Format(PBallRes._AlreadyTaken, FullName));
                return await Task.FromResult(BadRequest(errRes));
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

            if (contactAlreadyExist != null)
            {
                string Initial = contact.Initial == null ? "" : contact.Initial.EndsWith(".") ? contact.Initial.Substring(0, contact.Initial.Length - 1) : contact.Initial;
                string FullName = $"{ contact.FirstName } { Initial } { contact.LastName }";

                errRes.ErrList.Add(string.Format(PBallRes._AlreadyTaken, FullName));
                return await Task.FromResult(BadRequest(errRes));
            }
        }

        Contact contactToRet = new Contact();

        if (contactToModify != null)
        {
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
                errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
                return await Task.FromResult(BadRequest(errRes));
            }

            contactToRet = new Contact()
            {
                ContactID = contactToModify.ContactID,
                FirstName = contactToModify.FirstName,
                Initial = contactToModify.Initial,
                IsAdmin = contactToModify.IsAdmin,
                LastName = contactToModify.LastName,
                LastUpdateContactID = contactToModify.LastUpdateContactID,
                LastUpdateDate_UTC = contactToModify.LastUpdateDate_UTC,
                LoginEmail = contactToModify.LoginEmail,
                PasswordHash = "",
                PlayerLevel = contactToModify.PlayerLevel,
                Removed = contactToModify.Removed,
                ResetPasswordTempCode = "",
                Token = "",
            };
        }

        return await Task.FromResult(Ok(contactToRet));

    }
}


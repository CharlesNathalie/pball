namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<bool>> LogoffAsync(int ContactID)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (ContactID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "ContactID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contactExist = (from c in LoggedInService.LoggedInContactList
                                 where c.ContactID == ContactID
                                 select c).FirstOrDefault();

        if (contactExist != null)
        {
            if (LoggedInService != null)
            {
                LoggedInService.LoggedInContactList.Remove(contactExist);
            }
        }


        Contact? contact = (from c in db.Contacts
                            where c.ContactID == ContactID
                            select c).FirstOrDefault();

        if (contact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", ContactID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        try
        {
            contact.Token = "";

            if (UserService != null)
            {
                UserService.User = null;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
                return await Task.FromResult(BadRequest(errRes));
            }
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(true));
    }
}


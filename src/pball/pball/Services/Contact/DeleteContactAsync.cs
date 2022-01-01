namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<Contact>> DeleteContactAsync(int contactID)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contact = (from c in db.Contacts
                            where c.ContactID == contactID
                            select c).FirstOrDefault();

        if (contact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", contactID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        contact.Removed = true;
        contact.LastUpdateDate_UTC = DateTime.UtcNow;
        contact.LastUpdateContactID = 0;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(contact));
    }
}


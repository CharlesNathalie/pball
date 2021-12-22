namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<Contact>> DeleteContactAsync(int contactID)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
                return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        Contact? contact = (from c in db.Contacts
                            where c.ContactID == contactID
                            select c).FirstOrDefault();

        if (contact == null)
        {
            return await Task.FromResult(BadRequest(String.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", contactID.ToString())));
        }

        contact.Removed = true;
        contact.LastUpdateDate_UTC = DateTime.UtcNow;
        contact.LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID;

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


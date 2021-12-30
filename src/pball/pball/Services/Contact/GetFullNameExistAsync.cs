namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<bool>> GetFullNameExistAsync(FullNameModel fullNameModel)
    {
        if (string.IsNullOrWhiteSpace(fullNameModel.FirstName))
        {
            return await Task.FromResult(Ok(false));
        }

        if (string.IsNullOrWhiteSpace(fullNameModel.LastName))
        {
            return await Task.FromResult(Ok(false));
        }

        if (!string.IsNullOrWhiteSpace(fullNameModel.Initial))
        {
            Contact? contact = (from c in db.Contacts
                                where c.FirstName == fullNameModel.FirstName
                                && c.LastName == fullNameModel.LastName
                                && c.Initial == fullNameModel.Initial
                                select c).FirstOrDefault();

            if (contact != null)
            {
                return await Task.FromResult(Ok(true));
            }
        }
        else
        {
            Contact? contact = (from c in db.Contacts
                                where c.FirstName == fullNameModel.FirstName 
                                && c.LastName == fullNameModel.LastName
                                select c).FirstOrDefault();

            if (contact != null)
            {
                return await Task.FromResult(Ok(true));
            }
        }

        return await Task.FromResult(Ok(false));
    }
}


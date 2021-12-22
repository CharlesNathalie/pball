namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<bool>> GetLoginEmailExistAsync(LoginEmailModel loginEmailModel)
    {
        // no need
        //if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        //{
        //    return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        //}

        if (string.IsNullOrWhiteSpace(loginEmailModel.LoginEmail))
        {
            return await Task.FromResult(false);
        }

        Contact? contact = (from c in db.Contacts
                            where c.LoginEmail == loginEmailModel.LoginEmail
                            select c).FirstOrDefault();

        if (contact != null)
        {
            return await Task.FromResult(true);
        }

        return await Task.FromResult(true);
    }
}


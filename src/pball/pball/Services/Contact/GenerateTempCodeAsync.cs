namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<bool>> GenerateTempCodeAsync(LoginEmailModel loginEmailModel)
    {
        ErrRes errRes = new ErrRes();

        Contact? contact = (from c in db.Contacts
                                 where c.LoginEmail == loginEmailModel.LoginEmail
                            select c).FirstOrDefault();

        if (contact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "LoginEmail", loginEmailModel.LoginEmail.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        int UniqueCodeSize = 4;
        string TempCode = "";

        Random r = new Random((int)DateTime.Now.Ticks);

        while (TempCode.Length < UniqueCodeSize)
        {
            TempCode += (r.Next(0, 9)).ToString();
        }

        contact.ResetPasswordTempCode = TempCode;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotModify_Error_, "Contact", ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(true));
    }
}


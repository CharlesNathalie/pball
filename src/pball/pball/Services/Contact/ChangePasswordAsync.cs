using System.Security;

namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<bool>> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
    {
        ErrRes errRes = new ErrRes();

        if (string.IsNullOrWhiteSpace(changePasswordModel.LoginEmail))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LoginEmail"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contactExist = (from c in db.Contacts
                                 where c.LoginEmail == changePasswordModel.LoginEmail
                                 select c).AsNoTracking().FirstOrDefault();

        if (contactExist == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "LoginEmail", changePasswordModel.LoginEmail));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(changePasswordModel.Password))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Password"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (changePasswordModel.Password.Length > 50)
        {
            errRes.ErrList.Add(string.Format(PBallRes._MaxLengthIs_, "Password", "50"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(changePasswordModel.TempCode))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "TempCode"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contact = (from c in db.Contacts
                            where c.LoginEmail == changePasswordModel.LoginEmail
                            && c.ResetPasswordTempCode == changePasswordModel.TempCode
                            select c).FirstOrDefault();

        if (contact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "LoginEmail,TempCode", changePasswordModel.LoginEmail + "," + changePasswordModel.TempCode));
            return await Task.FromResult(BadRequest(errRes));
        }


        if (contact != null)
        {
            contact.PasswordHash = ScrambleService.Scramble(changePasswordModel.Password);
            contact.ResetPasswordTempCode = "";

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

        return await Task.FromResult(Ok(true));
    }
}


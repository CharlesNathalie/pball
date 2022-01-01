namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<string>> GenerateTempCodeAsync(LeagueContactGenerateCodeModel leagueContactGenerateCodeModel)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueContactGenerateCodeModel.LeagueAdminContactID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueAdminContactID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contactAdmin = (from c in db.Contacts
                                 where c.ContactID == leagueContactGenerateCodeModel.LeagueAdminContactID
                                 select c).FirstOrDefault();

        if (contactAdmin == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", leagueContactGenerateCodeModel.LeagueAdminContactID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueContactGenerateCodeModel.LeaguePlayerContactID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeaguePlayerContactID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contactPlayer = (from c in db.Contacts
                                  where c.ContactID == leagueContactGenerateCodeModel.LeaguePlayerContactID
                                  select c).FirstOrDefault();

        if (contactPlayer == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", leagueContactGenerateCodeModel.LeaguePlayerContactID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueContactGenerateCodeModel.LeagueID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        League? league = (from c in db.Leagues
                          where c.LeagueID == leagueContactGenerateCodeModel.LeagueID
                          select c).FirstOrDefault();

        if (league == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", leagueContactGenerateCodeModel.LeagueID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        LeagueContact? leagueContact = (from c in db.LeagueContacts
                                        where c.ContactID == leagueContactGenerateCodeModel.LeagueAdminContactID
                                        && c.LeagueID == leagueContactGenerateCodeModel.LeagueID
                                        select c).FirstOrDefault();

        if (leagueContact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "LeagueContact", "Contact,LeagueID",
                leagueContactGenerateCodeModel.LeagueAdminContactID.ToString() + "," + leagueContactGenerateCodeModel.LeagueID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (!leagueContact.IsLeagueAdmin)
        {
            errRes.ErrList.Add(string.Format(PBallRes.YouNeedToBeLeagueAdminToGenerateTempCode));
            return await Task.FromResult(BadRequest(errRes));
        }

        int UniqueCodeSize = 4;
        string TempCode = "";

        Random r = new Random((int)DateTime.Now.Ticks);

        while (TempCode.Length < UniqueCodeSize)
        {
            TempCode += (r.Next(0, 9)).ToString();
        }

        contactPlayer.ResetPasswordTempCode = TempCode;
        // not changing the LastUpdateDate_UTC or LastUpdateContactID because ResetPasswordTempCode is not really a change

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotModify_Error_, "Contact", ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(TempCode));
    }
}


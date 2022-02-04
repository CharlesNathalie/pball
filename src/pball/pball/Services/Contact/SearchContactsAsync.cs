namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<List<Player>>> SearchContactsAsync(int LeagueID, string SearchTerms)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (!string.IsNullOrWhiteSpace(SearchTerms))
        {
            List<string> terms = SearchTerms.Split(" ").ToList();

            if (db != null && db.Contacts != null && db.LeagueContacts != null)
            {

                List<int> contactIDList = (from c in db.LeagueContacts
                                           where c.LeagueID == LeagueID
                                           select c.ContactID).ToList();

                if (terms.Count == 1)
                {
                    return await Task.FromResult(Ok((from c in db.Contacts
                                                     let full = c.FirstName + " " + c.LastName + " " + c.LoginEmail
                                                     where !contactIDList.Contains(c.ContactID)
                                                     && full.Contains(terms[0])
                                                     orderby c.LastName, c.FirstName, c.Initial
                                                     select new Player
                                                     {
                                                         ContactID = c.ContactID,
                                                         FirstName = c.FirstName,
                                                         Initial = c.Initial,
                                                         LastName = c.LastName,
                                                         LoginEmail = c.LoginEmail,
                                                     }).Take(10).AsNoTracking().ToList()));
                }
                else if (terms.Count == 2)
                {
                    return await Task.FromResult(Ok((from c in db.Contacts
                                                     let full = c.FirstName + " " + c.LastName + " " + c.LoginEmail
                                                     where !contactIDList.Contains(c.ContactID)
                                                     && full.Contains(terms[0])
                                                     && full.Contains(terms[1])
                                                     orderby c.LastName, c.FirstName, c.Initial
                                                     select new Player
                                                     {
                                                         ContactID = c.ContactID,
                                                         FirstName = c.FirstName,
                                                         Initial = c.Initial,
                                                         LastName = c.LastName,
                                                         LoginEmail = c.LoginEmail,
                                                     }).Take(10).AsNoTracking().ToList()));
                }
                else if (terms.Count == 3)
                {
                    return await Task.FromResult(Ok((from c in db.Contacts
                                                     let full = c.FirstName + " " + c.LastName + " " + c.LoginEmail
                                                     where !contactIDList.Contains(c.ContactID)
                                                     && full.Contains(terms[0])
                                                     && full.Contains(terms[1])
                                                     && full.Contains(terms[2])
                                                     orderby c.LastName, c.FirstName, c.Initial
                                                     select new Player
                                                     {
                                                         ContactID = c.ContactID,
                                                         FirstName = c.FirstName,
                                                         Initial = c.Initial,
                                                         LastName = c.LastName,
                                                         LoginEmail = c.LoginEmail,
                                                     }).Take(10).AsNoTracking().ToList()));
                }
            }
        }

        return await Task.FromResult(Ok((new List<Player>())));
    }
}


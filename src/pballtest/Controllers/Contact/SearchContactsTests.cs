namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task SearchContactsAsync_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                LeagueContact? leagueContact = (from c in db.LeagueContacts
                                                select c).FirstOrDefault();

                Assert.NotNull(leagueContact);
                if (leagueContact != null)
                {
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {

                        Contact? contact = (from c in db.Contacts
                                            from lc in db.LeagueContacts
                                            where c.ContactID == lc.ContactID
                                            && lc.LeagueID != leagueContact.LeagueID
                                            orderby c.ContactID
                                            select c).AsNoTracking().FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                                if (Configuration != null)
                                {
                                    string SearchTerms = "Charles";
                                    HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/searchcontacts/{ leagueContact.LeagueID }/{ SearchTerms }").Result;
                                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                    string responseContent = await response.Content.ReadAsStringAsync();
                                    List<Player>? playerListRet = JsonSerializer.Deserialize<List<Player>>(responseContent);
                                    Assert.NotNull(playerListRet);
                                    if (playerListRet != null)
                                    {
                                        Assert.NotEmpty(playerListRet);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task SearchContactsAsync_Error_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                LeagueContact? leagueContact = (from c in db.LeagueContacts
                                                select c).FirstOrDefault();

                Assert.NotNull(leagueContact);
                if (leagueContact != null)
                {
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).AsNoTracking().FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                                if (Configuration != null)
                                {
                                    string SearchTerms = "Charles";

                                    HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/searchcontacts/{ leagueContact.LeagueID }/{ SearchTerms }").Result;
                                    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                                    string responseContent = await response.Content.ReadAsStringAsync();
                                    ErrRes? errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                                    Assert.NotNull(errRes);
                                    if (errRes != null)
                                    {
                                        Assert.NotEmpty(errRes.ErrList);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}


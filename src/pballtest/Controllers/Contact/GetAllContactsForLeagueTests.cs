namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetAllContactsForLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
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
                    Assert.NotNull(db.LeagueContacts);
                    if (db.LeagueContacts != null)
                    {
                        LeagueContact? leagueContact = (from c in db.LeagueContacts
                                                        orderby c.LeagueContactID
                                                        select c).AsNoTracking().FirstOrDefault();

                        Assert.NotNull(leagueContact);
                        if (leagueContact != null)
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                                if (Configuration != null)
                                {
                                    HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getallcontactsforleague/{ leagueContact.LeagueID }").Result;
                                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                    string responseContent = await response.Content.ReadAsStringAsync();
                                    List<Contact>? contactListRet = JsonSerializer.Deserialize<List<Contact>>(responseContent);
                                    Assert.NotNull(contactListRet);
                                    if (contactListRet != null)
                                    {
                                        Assert.NotEmpty(contactListRet);
                                    }

                                    response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getallcontactsforleague/{ -1 }").Result;
                                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                    responseContent = await response.Content.ReadAsStringAsync();
                                    contactListRet = JsonSerializer.Deserialize<List<Contact>>(responseContent);
                                    Assert.NotNull(contactListRet);
                                    if (contactListRet != null)
                                    {
                                        Assert.Empty(contactListRet);
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
    public async Task GetAllContactsForLeagueAsync_Error_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
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
                    Assert.NotNull(db.LeagueContacts);
                    if (db.LeagueContacts != null)
                    {
                        LeagueContact? leagueContact = (from c in db.LeagueContacts
                                                        orderby c.LeagueContactID
                                                        select c).AsNoTracking().FirstOrDefault();

                        Assert.NotNull(leagueContact);
                        if (leagueContact != null)
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                                if (Configuration != null)
                                {
                                    HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getallcontactsforleague/{ leagueContact.LeagueID }").Result;
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


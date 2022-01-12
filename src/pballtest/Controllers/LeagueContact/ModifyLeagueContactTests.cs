namespace pball.Controllers.Tests;

public partial class LeagueContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
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
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            from lc in db.LeagueContacts
                                            where c.ContactID != lc.ContactID
                                            select c).AsNoTracking().FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            if (Configuration != null)
                            {
                                using (HttpClient httpClient = new HttpClient())
                                {
                                    LoginModel loginModel = new LoginModel()
                                    {
                                        LoginEmail = contact.LoginEmail,
                                        Password = contact.LastName,
                                    };

                                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                                    httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                                    string stringData = JsonSerializer.Serialize(loginModel);
                                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                    HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/login", contentData).Result;
                                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                    string responseContent = await response.Content.ReadAsStringAsync();
                                    Contact? contactLogin = JsonSerializer.Deserialize<Contact>(responseContent);

                                    Assert.NotNull(contactLogin);
                                    if (contactLogin != null)
                                    {
                                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contactLogin.Token);

                                        int ContactID = leagueContact.ContactID;
                                        leagueContact.ContactID = contact.ContactID;

                                        stringData = JsonSerializer.Serialize(leagueContact);
                                        contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                        response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact", contentData).Result;
                                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                        responseContent = await response.Content.ReadAsStringAsync();
                                        LeagueContact? leagueContactRet = JsonSerializer.Deserialize<LeagueContact>(responseContent);
                                        Assert.NotNull(leagueContactRet);
                                        if (leagueContactRet != null)
                                        {
                                            Assert.True(leagueContactRet.LeagueID > 0);

                                            leagueContact.ContactID = ContactID;

                                            stringData = JsonSerializer.Serialize(leagueContact);
                                            contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                            response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact", contentData).Result;
                                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                            responseContent = await response.Content.ReadAsStringAsync();
                                            LeagueContact? leagueContactRet2 = JsonSerializer.Deserialize<LeagueContact>(responseContent);
                                            Assert.NotNull(leagueContactRet2);
                                            if (leagueContactRet2 != null)
                                            {
                                                Assert.True(leagueContactRet2.LeagueID > 0);

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
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueContactAsync_Error_Test(string culture)
    {
        Assert.True(await LeagueContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
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
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            from lc in db.LeagueContacts
                                            where c.ContactID != lc.ContactID
                                            select c).AsNoTracking().FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            if (Configuration != null)
                            {
                                using (HttpClient httpClient = new HttpClient())
                                {
                                    LoginModel loginModel = new LoginModel()
                                    {
                                        LoginEmail = contact.LoginEmail,
                                        Password = contact.LastName,
                                    };

                                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                                    httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                                    string stringData = JsonSerializer.Serialize(loginModel);
                                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                    HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/login", contentData).Result;
                                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                    string responseContent = await response.Content.ReadAsStringAsync();
                                    Contact? contactLogin = JsonSerializer.Deserialize<Contact>(responseContent);

                                    Assert.NotNull(contactLogin);
                                    if (contactLogin != null)
                                    {
                                        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contactLogin.Token);

                                        int ContactID = leagueContact.ContactID;
                                        leagueContact.ContactID = contact.ContactID;

                                        stringData = JsonSerializer.Serialize(leagueContact);
                                        contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                        response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact", contentData).Result;
                                        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                                        responseContent = await response.Content.ReadAsStringAsync();
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
}


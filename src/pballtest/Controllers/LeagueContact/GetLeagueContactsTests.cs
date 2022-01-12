namespace pball.Controllers.Tests;

public partial class LeagueContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLeagueContactsAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        if (Configuration != null)
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                                //string stringData = JsonSerializer.Serialize(leagueContact);
                                //var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                //HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact", contentData).Result;
                                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact/getleaguecontacts/{ leagueContact.LeagueID }").Result;
                                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                string responseContent = await response.Content.ReadAsStringAsync();
                                List<LeagueContact>? leagueContactListRet = JsonSerializer.Deserialize<List<LeagueContact>>(responseContent);
                                Assert.NotNull(leagueContactListRet);
                                if (leagueContactListRet != null)
                                {
                                    Assert.NotEmpty(leagueContactListRet);
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
    public async Task GetLeagueContactsAsync_Error_Test(string culture)
    {
        Assert.True(await LeagueContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        if (Configuration != null)
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                                //string stringData = JsonSerializer.Serialize(leagueContact);
                                //var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                //HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact", contentData).Result;
                                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact/getleaguecontacts/{ leagueContact.LeagueID }").Result;
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


namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetChangePasswordRequestListAsync_Good_Test(string culture)
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
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                        httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                        if (Configuration != null)
                        {
                            HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getchangepasswordrequestlist").Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            List<ChangePasswordModel>? changePasswordModelListRet = JsonSerializer.Deserialize<List<ChangePasswordModel>>(responseContent);
                            Assert.NotNull(changePasswordModelListRet);
                            if (changePasswordModelListRet != null)
                            {
                                Assert.NotEmpty(changePasswordModelListRet);
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
    public async Task GetChangePasswordRequestListAsync_Error_Test(string culture)
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
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                        httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                        if (Configuration != null)
                        {
                            HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getchangepasswordrequestlist").Result;
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


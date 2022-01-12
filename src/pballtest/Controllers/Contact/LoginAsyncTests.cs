namespace pball.Controllers.Tests;

public partial class ContactControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_Good_Test(string culture)
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
                            LoginModel loginModel = new LoginModel()
                            {
                                LoginEmail = contact.LoginEmail,
                                Password = contact.LastName,
                            };

                            string stringData = JsonSerializer.Serialize(loginModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/login", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            contact = JsonSerializer.Deserialize<Contact>(responseContent);
                            Assert.NotNull(contact);

                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_Error_Test(string culture)
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
                            LoginModel loginModel = new LoginModel()
                            {
                                LoginEmail = contact.LoginEmail,
                                Password = contact.LastName + "not",
                            };

                            string stringData = JsonSerializer.Serialize(loginModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/login", contentData).Result;
                            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            ErrRes? errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                            Assert.NotNull(errRes);

                        }
                    }
                }
            }
        }
    }
}


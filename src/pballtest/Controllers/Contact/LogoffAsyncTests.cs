namespace pball.Controllers.Tests;

public partial class ContactControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LogoffAsync_Good_Test(string culture)
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
                        if (Configuration != null)
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

                                response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/logoff/{ contact.ContactID }").Result;
                                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                responseContent = await response.Content.ReadAsStringAsync();
                                bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                                Assert.NotNull(boolRet);
                                Assert.True(boolRet);
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
    public async Task LogoffAsync_Error_Test(string culture)
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
                            Contact? contactRet = JsonSerializer.Deserialize<Contact>(responseContent);
                            Assert.NotNull(contactRet);

                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                            response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/logoff/{ -1 }").Result;
                            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                            responseContent = await response.Content.ReadAsStringAsync();
                            ErrRes? errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                            Assert.NotNull(errRes);
                        }
                    }
                }
            }
        }
    }
}


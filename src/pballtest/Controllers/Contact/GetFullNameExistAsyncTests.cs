namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetFullNameExistAsync_Without_Init_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    where string.IsNullOrWhiteSpace(c.Initial)
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
                            FullNameModel fullNameModel = new FullNameModel()
                            {
                                FirstName = contact.FirstName,
                                LastName = contact.LastName,
                                Initial = contact.Initial,
                            };

                            string stringData = JsonSerializer.Serialize(fullNameModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getfullnameexist", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                            Assert.True(boolRet);

                            fullNameModel = new FullNameModel()
                            {
                                FirstName = contact.FirstName + "not",
                                LastName = contact.LastName,
                                Initial = contact.Initial,
                            };

                            stringData = JsonSerializer.Serialize(fullNameModel);
                            contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getfullnameexist", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            responseContent = await response.Content.ReadAsStringAsync();
                            boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                            Assert.False(boolRet);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetFullNameExistAsync_With_Init_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contactNoInit = (from c in db.Contacts
                                          where !string.IsNullOrWhiteSpace(c.Initial)
                                          select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(contactNoInit);
                if (contactNoInit != null)
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                        httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                        if (Configuration != null)
                        {
                            FullNameModel fullNameModel = new FullNameModel()
                            {
                                FirstName = contactNoInit.FirstName,
                                LastName = contactNoInit.LastName,
                                Initial = contactNoInit.Initial,
                            };

                            string stringData = JsonSerializer.Serialize(fullNameModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getfullnameexist", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                            Assert.True(boolRet);

                            fullNameModel = new FullNameModel()
                            {
                                FirstName = contactNoInit.FirstName + "Not",
                                LastName = contactNoInit.LastName,
                                Initial = contactNoInit.Initial,
                            };

                            stringData = JsonSerializer.Serialize(fullNameModel);
                            contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getfullnameexist", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            responseContent = await response.Content.ReadAsStringAsync();
                            boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                            Assert.False(boolRet);
                        }
                    }
                }
            }
        }
    }
}


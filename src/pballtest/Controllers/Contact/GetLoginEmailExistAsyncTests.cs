namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLoginEmailExistAsync_Good_Test(string culture)
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
                            LoginEmailModel loginEmailModel = new LoginEmailModel()
                            {
                                LoginEmail = contact.LoginEmail,
                            };

                            string stringData = JsonSerializer.Serialize(loginEmailModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getloginemailexist", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                            Assert.True(boolRet);

                            loginEmailModel.LoginEmail = $"Not{ contact.LoginEmail}";

                            stringData = JsonSerializer.Serialize(loginEmailModel);
                            contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getloginemailexist", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                        }
                    }
                }
            }
        }
    }
}


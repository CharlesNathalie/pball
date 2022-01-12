namespace pball.Controllers.Tests;

public partial class ContactControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_Good_Test(string culture)
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
                            RegisterModel registerModel = new RegisterModel()
                            {
                                FirstName = contact.FirstName + "new",
                                LastName = contact.LastName,
                                Initial = contact.Initial,
                                LoginEmail = "New" + contact.LoginEmail,
                                Password = contact.LastName,
                                PlayerLevel = contact.PlayerLevel,
                            };

                            string stringData = JsonSerializer.Serialize(registerModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/register", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            contact = JsonSerializer.Deserialize<Contact>(responseContent);
                            Assert.NotNull(contact);
                            if (contact != null)
                            {
                                Contact? contactToDelete = (from c in  db.Contacts
                                                            where c.ContactID == contact.ContactID
                                                            select c).FirstOrDefault();

                                Assert.NotNull(contactToDelete);
                                if (contactToDelete != null)
                                {
                                    try
                                    {
                                        db.Contacts.Remove(contactToDelete);
                                        db.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        Assert.True(false, ex.Message);
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
    public async Task RegisterAsync_Error_Test(string culture)
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
                            RegisterModel registerModel = new RegisterModel()
                            {
                                FirstName = "",
                                LastName = contact.LastName,
                                Initial = contact.Initial,
                                LoginEmail = "New" + contact.LoginEmail,
                                Password = contact.LastName,
                                PlayerLevel = contact.PlayerLevel,
                            };

                            string stringData = JsonSerializer.Serialize(registerModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/register", contentData).Result;
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


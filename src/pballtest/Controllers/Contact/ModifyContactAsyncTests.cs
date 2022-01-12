namespace pball.Controllers.Tests;

public partial class ContactControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_Good_Test(string culture)
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
                            string FirstName = contact.FirstName;
                            contact.FirstName = contact.FirstName + "changed";

                            string stringData = JsonSerializer.Serialize(contact);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/contact", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            Contact? contactRet = JsonSerializer.Deserialize<Contact>(responseContent);
                            Assert.NotNull(contactRet);
                            if (contactRet != null)
                            {
                                Assert.True(contactRet.ContactID > 0);
                                Assert.Equal(contact.FirstName, contactRet.FirstName);
                            }

                            contact.FirstName = FirstName;

                            stringData = JsonSerializer.Serialize(contact);
                            contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/contact", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            responseContent = await response.Content.ReadAsStringAsync();
                            contactRet = JsonSerializer.Deserialize<Contact>(responseContent);
                            Assert.NotNull(contactRet);
                            if (contactRet != null)
                            {
                                Assert.True(contactRet.ContactID > 0);
                                Assert.Equal(contact.FirstName, contactRet.FirstName);
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
    public async Task ModifyContactAsync_Error_Test(string culture)
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
                            contact.ContactID = -1;

                            string stringData = JsonSerializer.Serialize(contact);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/contact", contentData).Result;
                            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            contact = JsonSerializer.Deserialize<Contact>(responseContent);
                            Assert.NotNull(contact);
                        }
                    }
                }
            }
        }
    }
}


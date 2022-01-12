namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_Good_Test(string culture)
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
                                    select c).FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    Random random = new Random();
                    string TempCode = $"{ random.Next(1000, 9999) }";

                    contact.ResetPasswordTempCode = TempCode;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Assert.True(false, ex.Message);
                    }

                    using (HttpClient httpClient = new HttpClient())
                    {
                        ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                        {
                            LoginEmail = contact.LoginEmail,
                            Password = contact.LastName + "New",
                            TempCode = TempCode,
                        };

                        Assert.NotNull(Configuration);
                        if (Configuration != null)
                        {
                            string stringData = JsonSerializer.Serialize(changePasswordModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/changepassword", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);

                            Assert.NotNull(boolRet);
                            if (boolRet != null)
                            {
                                Assert.True(boolRet);

                                TempCode = $"{ random.Next(1000, 9999) }";

                                contact.ResetPasswordTempCode = TempCode;

                                try
                                {
                                    db.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    Assert.True(false, ex.Message);
                                }

                                changePasswordModel.Password = contact.LastName;
                                changePasswordModel.TempCode = TempCode;

                                stringData = JsonSerializer.Serialize(changePasswordModel);
                                contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/changepassword", contentData).Result;
                                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                responseContent = await response.Content.ReadAsStringAsync();
                                boolRet = JsonSerializer.Deserialize<bool>(responseContent);

                                Assert.NotNull(boolRet);
                                if (boolRet != null)
                                {
                                    Assert.True(boolRet);
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
    public async Task ChangePasswordAsync_Error_Test(string culture)
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
                    contact.ResetPasswordTempCode = "";

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Assert.True(false, ex.Message);
                    }

                    using (HttpClient httpClient = new HttpClient())
                    {
                        ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                        {
                            LoginEmail = contact.LoginEmail,
                            Password = contact.LastName + "New",
                            TempCode = "121a", // will create an error
                        };

                        Assert.NotNull(Configuration);
                        if (Configuration != null)
                        {
                            string stringData = JsonSerializer.Serialize(changePasswordModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/changepassword", contentData).Result;
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


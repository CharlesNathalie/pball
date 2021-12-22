namespace pball.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_Good_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await ContactServiceSetup(culture));

        int ContactID = 0;

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionAddRes = await ContactService.RegisterAsync(registerModel);
            Assert.NotNull(actionAddRes);
            Assert.NotNull(actionAddRes.Result);
            if (actionAddRes != null && actionAddRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionAddRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionAddRes.Result).Value);
                if (((OkObjectResult)actionAddRes.Result).Value != null)
                {
                    Contact? contactRet = (Contact?)((OkObjectResult)actionAddRes.Result).Value;
                    Assert.NotNull(contactRet);
                    if (contactRet != null)
                    {
                        ContactID = contactRet.ContactID;
                    }
                }
            }

            string TempCode = $"TempCode { random.Next(10, 99) }";

            if (db != null)
            {
                Contact? contactToAddTempCode = (from c in db.Contacts
                                                where c.ContactID == ContactID
                                                select c).FirstOrDefault();

                if (contactToAddTempCode != null)
                {
                    contactToAddTempCode.ResetPasswordTempCode = TempCode;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Assert.True(false, ex.Message);
                }
            }


            if (Configuration != null)
            {
                ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                {
                    LoginEmail = Configuration["LoginEmail"],
                    Password = $"{ Configuration["Password"] }New",
                    TempCode = TempCode,
                };
                var actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                Assert.NotNull(actionRes);
                Assert.NotNull(actionRes.Result);
                if (actionRes != null && actionRes.Result != null)
                {
                    Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
                    Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
                    if (((OkObjectResult)actionRes.Result).Value != null)
                    {
                        Contact? contactRet = (Contact?)((OkObjectResult)actionRes.Result).Value;
                        Assert.NotNull(contactRet);
                    }
                }

                LoginModel loginModel = new LoginModel()
                {
                    LoginEmail = Configuration["LoginEmail"],
                    Password = $"{ Configuration["Password"] }New",
                };

                var actionLoginRes = await ContactService.LoginAsync(loginModel);
                Assert.NotNull(actionLoginRes);
                Assert.NotNull(actionLoginRes.Result);
                if (actionLoginRes != null && actionLoginRes.Result != null)
                {
                    Assert.Equal(200, ((ObjectResult)actionLoginRes.Result).StatusCode);
                    Assert.NotNull(((OkObjectResult)actionLoginRes.Result).Value);
                    if (((OkObjectResult)actionLoginRes.Result).Value != null)
                    {
                        Contact? contactRet = (Contact?)((OkObjectResult)actionLoginRes.Result).Value;
                        Assert.NotNull(contactRet);
                    }
                }
            }
        }
    }
}


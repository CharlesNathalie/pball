namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                Assert.True(contact.ContactID > 0);
            }

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionRegisterRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
            }

            RegisterModel registerModel2 = await FillRegisterModelAsync();

            registerModel2.LoginEmail = "P" + registerModel.LoginEmail;
            registerModel2.FirstName = "P" + registerModel.FirstName;
            registerModel2.LastName = "P" + registerModel.LastName;
            registerModel2.Initial = "P" + registerModel.Initial;

            var actionRegisterRes2 = await ContactService.RegisterAsync(registerModel2);
            Contact? contact3 = await DoOKTestReturnContactAsync(actionRegisterRes2);
            Assert.NotNull(contact3);

            if (contact3 != null)
            {
                Assert.True(contact3.ContactID > 0);
            }

            if (LeagueService != null)
            {
                League leagueNew = await FillLeague();

                var actionAddLeagueRes = await LeagueService.AddLeagueAsync(leagueNew);
                League? league = await DoOKTestReturnLeagueAsync(actionAddLeagueRes);
                Assert.NotNull(league);

                if (league != null)
                {
                    Assert.True(league.LeagueID > 0);
                }

                if (LeagueContactService != null)
                {
                    if (contact != null && contact2 != null && contact3 != null && league != null)
                    {
                        LeagueContact leagueContactNew = new LeagueContact()
                        {
                            ContactID = contact.ContactID,
                            IsLeagueAdmin = true,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes = await LeagueContactService.AddLeagueContactAsync(leagueContactNew);
                        LeagueContact? leagueContact = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes);
                        Assert.NotNull(leagueContact);

                        if (leagueContact != null)
                        {
                            Assert.True(leagueContact.LeagueContactID > 0);
                        }

                        LeagueContact leagueContactNew2 = new LeagueContact()
                        {
                            ContactID = contact3.ContactID,
                            IsLeagueAdmin = false,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes2 = await LeagueContactService.AddLeagueContactAsync(leagueContactNew2);
                        LeagueContact? leagueContact2 = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes2);
                        Assert.NotNull(leagueContact2);

                        if (leagueContact2 != null)
                        {
                            Assert.True(leagueContact2.LeagueContactID > 0);
                        }

                        LeagueContactGenerateCodeModel leagueContactGenerateCodeModel = new LeagueContactGenerateCodeModel()
                        {
                            LeagueAdminContactID = contact.ContactID,
                            LeaguePlayerContactID = contact3.ContactID,
                            LeagueID = league.LeagueID,
                        };

                        var actionRes = await ContactService.GenerateTempCodeAsync(leagueContactGenerateCodeModel);
                        string? strRet = await DoOKTestReturnStringAsync(actionRes);
                        Assert.NotNull(strRet);
                        Assert.NotEmpty(strRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                Assert.True(contact.ContactID > 0);
            }

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
            }

            if (UserService != null)
            {
                UserService.User = null;
            }

            var actionRes = await ContactService.GenerateTempCodeAsync(new LeagueContactGenerateCodeModel());
            boolRet = await DoBadRequestStringTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_LeagueAdminContactID_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                Assert.True(contact.ContactID > 0);
            }

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
            }

            if (contact2 != null)
            {
                Assert.True(contact2.ContactID > 0);

                LeagueContactGenerateCodeModel leagueContactGenerateCodeModel = new LeagueContactGenerateCodeModel()
                {
                    LeagueAdminContactID = 0,
                    LeaguePlayerContactID = 0,
                    LeagueID = 0,
                };

                var actionRes = await ContactService.GenerateTempCodeAsync(leagueContactGenerateCodeModel);
                boolRet = await DoBadRequestStringTestAsync(string.Format(PBallRes._IsRequired, "LeagueAdminContactID"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                leagueContactGenerateCodeModel.LeagueAdminContactID = 10000;

                actionRes = await ContactService.GenerateTempCodeAsync(leagueContactGenerateCodeModel);
                boolRet = await DoBadRequestStringTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", leagueContactGenerateCodeModel.LeagueAdminContactID.ToString()), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_LeaguePlayerContactID_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                Assert.True(contact.ContactID > 0);
            }

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
            }

            if (contact2 != null)
            {
                Assert.True(contact2.ContactID > 0);

                LeagueContactGenerateCodeModel leagueContactGenerateCodeModel = new LeagueContactGenerateCodeModel()
                {
                    LeagueAdminContactID = contact2.ContactID,
                    LeaguePlayerContactID = 0,
                    LeagueID = 0,
                };

                var actionRes = await ContactService.GenerateTempCodeAsync(leagueContactGenerateCodeModel);
                boolRet = await DoBadRequestStringTestAsync(string.Format(PBallRes._IsRequired, "LeaguePlayerContactID"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                leagueContactGenerateCodeModel.LeaguePlayerContactID = 10000;

                actionRes = await ContactService.GenerateTempCodeAsync(leagueContactGenerateCodeModel);
                boolRet = await DoBadRequestStringTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", leagueContactGenerateCodeModel.LeaguePlayerContactID.ToString()), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                Assert.True(contact.ContactID > 0);
            }

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
            }

            RegisterModel registerModel2 = await FillRegisterModelAsync();

            registerModel2.LoginEmail = "P" + registerModel.LoginEmail;
            registerModel2.FirstName = "P" + registerModel.FirstName;
            registerModel2.LastName = "P" + registerModel.LastName;
            registerModel2.Initial = "P" + registerModel.Initial;

            var actionRegisterRes2 = await ContactService.RegisterAsync(registerModel2);
            Contact? contact3 = await DoOKTestReturnContactAsync(actionRegisterRes2);
            Assert.NotNull(contact3);

            if (contact3 != null)
            {
                Assert.True(contact3.ContactID > 0);
            }

            if (LeagueService != null)
            {
                if (LeagueContactService != null)
                {
                    if (contact != null && contact2 != null && contact3 != null)
                    {
                        LeagueContactGenerateCodeModel leagueContactGenerateCodeModel = new LeagueContactGenerateCodeModel()
                        {
                            LeagueAdminContactID = contact.ContactID,
                            LeaguePlayerContactID = contact3.ContactID,
                            LeagueID = 0,
                        };

                        var actionRes = await ContactService.GenerateTempCodeAsync(leagueContactGenerateCodeModel);
                        boolRet = await DoBadRequestStringTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);

                        leagueContactGenerateCodeModel.LeagueID = 100000;

                        actionRes = await ContactService.GenerateTempCodeAsync(leagueContactGenerateCodeModel);
                        boolRet = await DoBadRequestStringTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", leagueContactGenerateCodeModel.LeagueID.ToString()), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_IsMemberOfTheLeague_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                Assert.True(contact.ContactID > 0);
            }

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
            }

            RegisterModel registerModel2 = await FillRegisterModelAsync();

            registerModel2.LoginEmail = "P" + registerModel.LoginEmail;
            registerModel2.FirstName = "P" + registerModel.FirstName;
            registerModel2.LastName = "P" + registerModel.LastName;
            registerModel2.Initial = "P" + registerModel.Initial;

            var actionRegisterRes2 = await ContactService.RegisterAsync(registerModel2);
            Contact? contact3 = await DoOKTestReturnContactAsync(actionRegisterRes2);
            Assert.NotNull(contact3);

            if (contact3 != null)
            {
                Assert.True(contact3.ContactID > 0);
            }

            if (LeagueService != null)
            {
                League leagueNew = await FillLeague();

                var actionAddLeagueRes = await LeagueService.AddLeagueAsync(leagueNew);
                League? league = await DoOKTestReturnLeagueAsync(actionAddLeagueRes);
                Assert.NotNull(league);

                if (league != null)
                {
                    Assert.True(league.LeagueID > 0);
                }

                League leagueNew2 = await FillLeague();

                if (league != null)
                {
                    leagueNew2.LeagueName = "Other" + league.LeagueName;
                }

                var actionAddLeagueRes2 = await LeagueService.AddLeagueAsync(leagueNew2);
                League? league2 = await DoOKTestReturnLeagueAsync(actionAddLeagueRes2);
                Assert.NotNull(league2);

                if (league2 != null)
                {
                    Assert.True(league2.LeagueID > 0);
                }

                if (LeagueContactService != null)
                {
                    if (contact != null && contact2 != null && contact3 != null && league != null && league2 != null)
                    {
                        LeagueContact leagueContactNew = new LeagueContact()
                        {
                            ContactID = contact.ContactID,
                            IsLeagueAdmin = true,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes = await LeagueContactService.AddLeagueContactAsync(leagueContactNew);
                        LeagueContact? leagueContact = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes);
                        Assert.NotNull(leagueContact);

                        if (leagueContact != null)
                        {
                            Assert.True(leagueContact.LeagueContactID > 0);
                        }

                        LeagueContact leagueContactNew2 = new LeagueContact()
                        {
                            ContactID = contact3.ContactID,
                            IsLeagueAdmin = false,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes2 = await LeagueContactService.AddLeagueContactAsync(leagueContactNew2);
                        LeagueContact? leagueContact2 = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes2);
                        Assert.NotNull(leagueContact2);

                        if (leagueContact2 != null)
                        {
                            Assert.True(leagueContact2.LeagueContactID > 0);
                        }

                        LeagueContactGenerateCodeModel leagueContactGenerateCodeModel = new LeagueContactGenerateCodeModel()
                        {
                            LeagueAdminContactID = contact.ContactID,
                            LeaguePlayerContactID = contact3.ContactID,
                            LeagueID = league2.LeagueID,
                        };

                        var actionRes = await ContactService.GenerateTempCodeAsync(leagueContactGenerateCodeModel);
                        boolRet = await DoBadRequestStringTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "LeagueContact", "Contact,LeagueID",
                            leagueContactGenerateCodeModel.LeagueAdminContactID.ToString() + "," + leagueContactGenerateCodeModel.LeagueID.ToString()), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);

                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_IsAdminOfTheLeague_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                Assert.True(contact.ContactID > 0);
            }

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
            }

            RegisterModel registerModel2 = await FillRegisterModelAsync();

            registerModel2.LoginEmail = "P" + registerModel.LoginEmail;
            registerModel2.FirstName = "P" + registerModel.FirstName;
            registerModel2.LastName = "P" + registerModel.LastName;
            registerModel2.Initial = "P" + registerModel.Initial;

            var actionRegisterRes2 = await ContactService.RegisterAsync(registerModel2);
            Contact? contact3 = await DoOKTestReturnContactAsync(actionRegisterRes2);
            Assert.NotNull(contact3);

            if (contact3 != null)
            {
                Assert.True(contact3.ContactID > 0);
            }

            if (LeagueService != null)
            {
                League leagueNew = await FillLeague();

                var actionAddLeagueRes = await LeagueService.AddLeagueAsync(leagueNew);
                League? league = await DoOKTestReturnLeagueAsync(actionAddLeagueRes);
                Assert.NotNull(league);

                if (league != null)
                {
                    Assert.True(league.LeagueID > 0);
                }

                if (LeagueContactService != null)
                {
                    if (contact != null && contact2 != null && contact3 != null && league != null)
                    {
                        LeagueContact leagueContactNew = new LeagueContact()
                        {
                            ContactID = contact.ContactID,
                            IsLeagueAdmin = false,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes = await LeagueContactService.AddLeagueContactAsync(leagueContactNew);
                        LeagueContact? leagueContact = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes);
                        Assert.NotNull(leagueContact);

                        if (leagueContact != null)
                        {
                            Assert.True(leagueContact.LeagueContactID > 0);
                        }

                        LeagueContact leagueContactNew2 = new LeagueContact()
                        {
                            ContactID = contact3.ContactID,
                            IsLeagueAdmin = false,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes2 = await LeagueContactService.AddLeagueContactAsync(leagueContactNew2);
                        LeagueContact? leagueContact2 = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes2);
                        Assert.NotNull(leagueContact2);

                        if (leagueContact2 != null)
                        {
                            Assert.True(leagueContact2.LeagueContactID > 0);
                        }

                        LeagueContactGenerateCodeModel leagueContactGenerateCodeModel = new LeagueContactGenerateCodeModel()
                        {
                            LeagueAdminContactID = contact.ContactID,
                            LeaguePlayerContactID = contact3.ContactID,
                            LeagueID = league.LeagueID,
                        };

                        var actionRes = await ContactService.GenerateTempCodeAsync(leagueContactGenerateCodeModel);
                        boolRet = await DoBadRequestStringTestAsync(PBallRes.YouNeedToBeLeagueAdminToGenerateTempCode, actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);

                    }
                }
            }
        }
    }
}


//namespace pball.Controllers.Tests;

//public partial class ContactControllerTests : BaseControllerTests
//{
//    [Theory]
//    [InlineData("en-CA")]
//    [InlineData("fr-CA")]
//    public async Task GenerateTempCodeAsync_Good_Test(string culture)
//    {
//        Random random = new Random();

//        Assert.True(await ContactControllerSetup(culture));

//        int ContactID = 0;

//        if (ContactService != null)
//        {
//            RegisterModel registerModel = await FillRegisterModel();
//            Assert.NotEmpty(registerModel.FirstName);
//            Assert.NotEmpty(registerModel.LastName);
//            Assert.NotEmpty(registerModel.LoginEmail);

//            var actionAddRes = await ContactService.RegisterAsync(registerModel);
//            Assert.NotNull(actionAddRes);
//            Assert.NotNull(actionAddRes.Result);
//            if (actionAddRes != null && actionAddRes.Result != null)
//            {
//                Assert.Equal(200, ((ObjectResult)actionAddRes.Result).StatusCode);
//                Assert.NotNull(((OkObjectResult)actionAddRes.Result).Value);
//                if (((OkObjectResult)actionAddRes.Result).Value != null)
//                {
//                    Contact? contactRet = (Contact?)((OkObjectResult)actionAddRes.Result).Value;
//                    Assert.NotNull(contactRet);
//                    if (contactRet != null)
//                    {
//                        ContactID = contactRet.ContactID;
//                    }
//                }
//            }

//            if (Configuration != null)
//            {
//                using (HttpClient httpClient = new HttpClient())
//                {
//                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
//                    httpClient.DefaultRequestHeaders.Accept.Add(contentType);

//                    LeagueContactGenerateCodeModel leagueContactGenerateCodeModel = new LeagueContactGenerateCodeModel()
//                    {
//                        LeagueAdminContactID = 0, // need proper ID
//                         LeaguePlayerContactID = 0, // need proper id
//                          LeagueID = 0, // need proper id
//                    };

//                    if (Configuration != null)
//                    {
//                        string stringData = JsonSerializer.Serialize(leagueContactGenerateCodeModel);
//                        var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
//                        HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/generatetempcode", contentData).Result;
//                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

//                        string responseContent = await response.Content.ReadAsStringAsync();
//                        bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
//                        Assert.True(boolRet);
//                    }
//                }
//            }
//        }
//    }
//}


namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteContactAsync_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        if (ContactService != null)
        {
            int ContactIDToDelete = 0;

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
                        ContactIDToDelete = contactRet.ContactID;
                    }
                }
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                if (Configuration != null)
                {
                    HttpResponseMessage response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/{ ContactIDToDelete }").Result;
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Contact? contact = JsonSerializer.Deserialize<Contact>(responseContent);
                    Assert.NotNull(contact);
                }
            }
        }
    }
}


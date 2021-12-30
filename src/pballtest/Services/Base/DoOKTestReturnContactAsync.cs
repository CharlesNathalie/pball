namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<Contact?> DoOKTestReturnContactAsync(ActionResult<Contact> actionRes)
    {
        Contact? contact = new Contact();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                contact = (Contact?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(contact);
            }
        }

        return await Task.FromResult(contact);
    }
}


namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<List<Contact>?> DoOKTestReturnContactListAsync(ActionResult<List<Contact>> actionRes)
    {
        List<Contact>? contactList = new List<Contact>();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                contactList = (List<Contact>?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(contactList);
            }
        }

        return await Task.FromResult(contactList);
    }
}


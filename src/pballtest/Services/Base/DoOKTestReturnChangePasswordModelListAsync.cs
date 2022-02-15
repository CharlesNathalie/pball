namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<List<ChangePasswordModel>?> DoOKTestReturnChangePasswordModelListAsync(ActionResult<List<ChangePasswordModel>> actionRes)
    {
        List<ChangePasswordModel>? changePasswordModelList = new List<ChangePasswordModel>();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                changePasswordModelList = (List<ChangePasswordModel>?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(changePasswordModelList);
            }
        }

        return await Task.FromResult(changePasswordModelList);
    }
}


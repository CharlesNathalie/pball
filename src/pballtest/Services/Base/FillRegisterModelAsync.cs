namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<RegisterModel> FillRegisterModel()
    {
        Random random = new Random();

        if (LoggedInService != null && LoggedInService.LoggedInContactInfo != null && LoggedInService.LoggedInContactInfo.LoggedInContact != null)
        {
            if (Configuration != null)
            {
                return await Task.FromResult(new RegisterModel()
                {
                    FirstName = $"Charles{ random.Next(1, 1000)}",
                    LastName = $"LeBlanc{ random.Next(1, 1000)}",
                    Initial = $"G{ random.Next(1, 10)}",
                    Password = Configuration["Password"],
                    LoginEmail = Configuration["LoginEmail"],
                    PlayerLevel = random.Next(1, 5),
                });
            }
        }

        return await Task.FromResult(new RegisterModel());
    }
}


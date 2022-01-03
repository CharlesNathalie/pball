namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<RegisterModel> FillRegisterModelAsync()
    {
        Random random = new Random();

        if (Configuration != null)
        {
            return await Task.FromResult(new RegisterModel()
            {
                FirstName = $"Charles{ random.Next(1, 10000)}",
                LastName = $"LeBlanc{ random.Next(1, 10000)}",
                Initial = $"G{ random.Next(1, 10000) }",
                Password = $"{ random.Next(1, 10000) }a",
                LoginEmail = $"{ random.Next(1, 10000) }a@gmail.com",
                PlayerLevel = random.Next(1, 5),
            });
        }

        return await Task.FromResult(new RegisterModel());
    }
}


namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<RegisterModel> FillRegisterModelAsync()
    {
        Random random = new Random();

        RegisterModel registerModel = new RegisterModel();

        if (Configuration != null)
        {
            registerModel = new RegisterModel()
            {
                FirstName = $"FirstName{ random.Next(10000)}",
                LastName = $"LastName{ random.Next(10000)}",
                Initial = $"Init{ random.Next(10000)}",
                LoginEmail = $"{ random.Next(10000) }{ Configuration["LoginEmail"] }",
                Password = $"{ random.Next(10000) }{ Configuration["Password"] }",
                PlayerLevel = 1 + (4 * random.NextDouble()),
            };
        }

        return await Task.FromResult(registerModel);
    }
}


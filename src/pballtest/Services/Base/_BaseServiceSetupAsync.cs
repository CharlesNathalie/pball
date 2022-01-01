namespace pball.Services.Tests;

[Collection("Sequential")]
public partial class BaseServiceTests
{
    protected IConfiguration? Configuration { get; set; }
    protected IServiceProvider? Provider { get; set; }
    protected IServiceCollection? Services { get; set; }
    protected IContactService? ContactService { get; set; }
    protected IGameService? GameService { get; set; }
    protected ILeagueService? LeagueService { get; set; }
    protected ILeagueContactService? LeagueContactService { get; set; }
    protected IUserService? UserService { get; set; }
    protected ILoggedInService? LoggedInService { get; set; }
    protected IScrambleService? ScrambleService { get; set; }
    protected PBallContext? db { get; set; }

    protected async Task<bool> _BaseServiceSetupAsync(string culture)
    {
        Configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
           .AddJsonFile("appsettings.json")
           .AddUserSecrets("pballtest")
           .Build();

        Services = new ServiceCollection();

        Services.AddSingleton<IConfiguration>(Configuration);

        Assert.NotNull(Configuration["pballurl"]);
        Assert.NotNull(Configuration["APISecret"]);
        Assert.NotNull(Configuration["pballDB"]);
        Assert.NotNull(Configuration["LoginEmail"]);
        Assert.NotNull(Configuration["Password"]);

        Services.AddSingleton<IContactService, ContactService>();
        Services.AddSingleton<IGameService, GameService>();
        Services.AddSingleton<ILeagueService, LeagueService>();
        Services.AddSingleton<ILeagueContactService, LeagueContactService>();
        Services.AddSingleton<IUserService, UserService>();
        Services.AddSingleton<ILoggedInService, LoggedInService>();
        Services.AddSingleton<IScrambleService, ScrambleService>();

        Services.AddDbContext<PBallContext>(options =>
        {
            options.UseSqlServer(Configuration["pballDB"]);
        });

        Provider = Services.BuildServiceProvider();
        Assert.NotNull(Provider);

        ContactService = Provider.GetService<IContactService>();
        Assert.NotNull(ContactService);

        GameService = Provider.GetService<IGameService>();
        Assert.NotNull(GameService);

        LeagueService = Provider.GetService<ILeagueService>();
        Assert.NotNull(LeagueService);

        LeagueContactService = Provider.GetService<ILeagueContactService>();
        Assert.NotNull(LeagueContactService);

        LoggedInService = Provider.GetService<ILoggedInService>();
        Assert.NotNull(LoggedInService);

        UserService = Provider.GetService<IUserService>();
        Assert.NotNull(UserService);

        ScrambleService = Provider.GetService<IScrambleService>();
        Assert.NotNull(ScrambleService);

        db = Provider.GetService<PBallContext>();
        Assert.NotNull(db);

        await ClearAllLeagueContactsFromDBAsync();
        await ClearAllGamesFromDBAsync();
        await ClearAllLeaguesFromDBAsync();
        await ClearAllContactsFromDBAsync();

        RegisterModel registerModel = await FillRegisterModelAsync();

        Contact? contact = await DoRegisterTestAsync(registerModel);
        Assert.NotNull(contact);

        if (contact != null)
        {
            Assert.True(contact.ContactID > 0);

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            Contact? contact2 = await DoLoginTestAsync(loginModel);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                Assert.True(contact2.ContactID > 0);
            }
        }

        return await Task.FromResult(true);
    }
}


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
    protected IHelperService? HelperService { get; set; }
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

        Services.AddSingleton<IContactService, ContactService>();
        Services.AddSingleton<IGameService, GameService>();
        Services.AddSingleton<ILeagueService, LeagueService>();
        Services.AddSingleton<ILeagueContactService, LeagueContactService>();
        Services.AddSingleton<IHelperService, HelperService>();
        Services.AddSingleton<IUserService, UserService>();
        Services.AddSingleton<ILoggedInService, LoggedInService>();
        Services.AddSingleton<IScrambleService, ScrambleService>();

        Services.AddDbContext<PBallContext>(options =>
        {
            options.UseSqlite($"Data Source={ Configuration["pballDB"] }");
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

        HelperService = Provider.GetService<IHelperService>();
        Assert.NotNull(HelperService);

        UserService = Provider.GetService<IUserService>();
        Assert.NotNull(UserService);

        ScrambleService = Provider.GetService<IScrambleService>();
        Assert.NotNull(ScrambleService);

        db = Provider.GetService<PBallContext>();
        Assert.NotNull(db);

        int MinContactCount = 100;
        int MinLeagueCount = 5;
        int MinLeagueContactCount = 15 * MinLeagueCount;
        int MinGameCount = 100 * MinLeagueCount;

        if (db != null && db.Contacts != null && db.Leagues != null && db.LeagueContacts != null && db.Games != null)
        {
            int countContact = (from c in db.Contacts select c).Count();
            int countLeague = (from c in db.Leagues select c).Count();
            int countLeagueConact = (from c in db.LeagueContacts select c).Count();
            int countGame = (from c in db.Games select c).Count();

            if (countContact < MinContactCount || countLeague < MinLeagueCount || countLeagueConact < MinLeagueContactCount || countGame < MinGameCount)
            {
                bool? boolRet = await FillDBWithTestDataAsync(MinContactCount, MinLeagueCount, MinLeagueContactCount / MinLeagueCount, MinGameCount / MinLeagueCount);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }

        return await Task.FromResult(true);
    }
}


namespace pball.Controllers.Tests;

[Collection("Sequential")]
public partial class BaseControllerTests
{
    protected IConfiguration? Configuration { get; set; }
    protected IServiceProvider? Provider { get; set; }
    protected IServiceCollection? Services { get; set; }
    protected PBallContext? db { get; set; }

    protected async Task<bool> BaseControllerSetup(string culture)
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

        //Services.AddDbContext<PBallContext>(options =>
        //{
        //    options.UseSqlServer(Configuration["pballDB"]);
        //});

        Services.AddDbContext<PBallContext>(options =>
        {
            options.UseSqlite($"Data Source={ Configuration["pballDB"] }");
        });

        Provider = Services.BuildServiceProvider();
        Assert.NotNull(Provider);

        db = Provider.GetService<PBallContext>();
        Assert.NotNull(db);

        await ClearAllLeagueContactsFromDBAsync();
        await ClearAllGamesFromDBAsync();
        await ClearAllLeaguesFromDBAsync();
        await ClearAllContactsFromDBAsync();

        return await Task.FromResult(true);
    }
}


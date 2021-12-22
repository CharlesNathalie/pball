namespace pball.Tests;

[Collection("Sequential")]
public partial class BaseControllerTests
{
    protected IConfiguration? Configuration { get; set; }
    protected IServiceProvider? Provider { get; set; }
    protected IServiceCollection? Services { get; set; }
    protected IContactService? ContactService { get; set; }
    protected IGameService? GameService { get; set; }
    protected ILeagueService? LeagueService { get; set; }
    protected ILeagueContactService? LeagueContactService { get; set; }
    protected ILoggedInService? LoggedInService { get; set; }
    protected IScrambleService? ScrambleService { get; set; }
    protected PBallContext? db { get; set; }
    //protected Contact? contact { get; set; }

    protected async Task<bool> BaseControllerSetup(string culture)
    {
        Configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
           .AddJsonFile("appsettings.json")
           .AddUserSecrets("pballtests")
           .Build();

        Services = new ServiceCollection();

        Services.AddSingleton<IConfiguration>(Configuration);

        Assert.NotNull(Configuration["pballurl"]);
        Assert.NotNull(Configuration["APISecret"]);
        Assert.NotNull(Configuration["pballDB"]);
        Assert.NotNull(Configuration["LoginEmail"]);
        Assert.NotNull(Configuration["Password"]);
        Assert.NotNull(Configuration["SmtpHost"]);
        Assert.NotNull(Configuration["NetworkCredentialUserName"]);
        Assert.NotNull(Configuration["NetworkCredentialPassword"]);

        Services.AddSingleton<IContactService, ContactService>();
        Services.AddSingleton<IGameService, GameService>();
        Services.AddSingleton<ILeagueService, LeagueService>();
        Services.AddSingleton<ILeagueContactService, LeagueContactService>();
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

        ScrambleService = Provider.GetService<IScrambleService>();
        Assert.NotNull(ScrambleService);

        if (db != null)
        {
            List<LeagueContact> leagueContactList = (from c in db.LeagueContacts
                                                     select c).ToList();

            try
            {
                db.LeagueContacts?.RemoveRange(leagueContactList);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }

            List<League> leagueList = (from c in db.Leagues
                                       select c).ToList();

            try
            {
                db.Leagues?.RemoveRange(leagueList);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }

            List<Game> gameList = (from c in db.Games
                                   select c).ToList();

            try
            {
                db.Games?.RemoveRange(gameList);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }

            List<Contact> contactList = (from c in db.Contacts
                                         select c).ToList();

            try
            {
                db.Contacts?.RemoveRange(contactList);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        RegisterModel registerModel = new RegisterModel()
        {
            FirstName = "Charles",
            LastName = "LeBlanc",
            Initial = "",
            LoginEmail = Configuration["LoginEmail"],
            Password = Configuration["Password"],
            PlayerLevel = 3.0f,
        };

        if (ContactService != null)
        {
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
                }
            }
        }

        if (LoggedInService != null)
        {
            await LoggedInService.SetLoggedInContactInfoAsync(Configuration["LoginEmail"]);
            Assert.NotNull(LoggedInService.LoggedInContactInfo);
            Assert.NotNull(LoggedInService.LoggedInContactInfo.LoggedInContact);
        }

        using (HttpClient httpClient = new HttpClient())
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = Configuration["LoginEmail"],
                Password = Configuration["Password"],
            };

            string stringData = JsonSerializer.Serialize(loginModel);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/en-CA/auth/token", contentData).Result;
            Assert.True((int)response.StatusCode == 200);

            Contact? contact = JsonSerializer.Deserialize<Contact>(response.Content.ReadAsStringAsync().Result);
            Assert.NotNull(contact);
        }

        return await Task.FromResult(true);
    }
}


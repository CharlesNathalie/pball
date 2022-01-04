using PBallServices;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                         builder =>
                         {
                             builder.WithOrigins("*")
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowAnyOrigin();
                         });
    });
}

builder.Services.AddDbContext<PBallContext>(options =>
    options.UseSqlServer(builder.Configuration["pballDB"]));

builder.Services.AddSingleton<IScrambleService, ScrambleService>();
builder.Services.AddSingleton<ILoggedInService, LoggedInService>();

builder.Services.AddScoped<IHelperService, HelperService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<ILeagueContactService, LeagueContactService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(MyAllowSpecificOrigins);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{culture}/{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

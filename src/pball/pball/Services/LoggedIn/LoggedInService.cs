﻿namespace PBallServices;

public partial class LoggedInService : ILoggedInService
{
    public LoggedInContactInfo LoggedInContactInfo { get; set; }

    private IConfiguration Configuration { get; }
    private PBallContext db { get; }

    public LoggedInService(IConfiguration Configuration, PBallContext db)
    {
        if (Configuration == null) throw new Exception($"{ string.Format(PBallRes._ShouldNotBeNullOrEmpty, "Configuration") }");
        if (db == null) throw new Exception($"{ string.Format(PBallRes._ShouldNotBeNullOrEmpty, "db") }");

        this.Configuration = Configuration;
        this.db = db;

        LoggedInContactInfo = new LoggedInContactInfo();
    }
}


namespace PBallModels;

public partial class PBallContext : DbContext
{
    public virtual DbSet<Contact>? Contacts { get; set; }
    public virtual DbSet<Game>? Games { get; set; }
    public virtual DbSet<League>? Leagues { get; set; }
    public virtual DbSet<LeagueContact>? LeagueContacts { get; set; }

    public PBallContext()
    {

    }

    public PBallContext(DbContextOptions<PBallContext> options)
        : base(options)
    {

    }
}

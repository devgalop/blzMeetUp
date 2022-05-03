using MeetUpBack.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetUpBack.Data;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public DbSet<City>? Cities { get; set; }
    public DbSet<Country>? Countries { get; set; }
    public DbSet<Location>? Locations { get; set; }
    public DbSet<MeetUp>? MeetUps { get; set; }
    public DbSet<Event>? Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {        
        options.UseSqlServer(Configuration.GetConnectionString("DBMEETUP"));
    }
}
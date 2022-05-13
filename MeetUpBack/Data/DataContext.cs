using MeetUpBack.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetUpBack.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<MeetUp> MeetUps { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new SeedDb(modelBuilder).Seed();
    }
}
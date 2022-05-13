using MeetUpBack.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetUpBack.Data;

public class SeedDb
{
    private readonly ModelBuilder modelBuilder;

    public SeedDb(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void Seed()
    {
        //Add here the rows to insert into the database
        // modelBuilder.Entity<entityName>().HasData(...);
    }
    
}
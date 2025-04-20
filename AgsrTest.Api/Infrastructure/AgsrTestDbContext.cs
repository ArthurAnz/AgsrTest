using AgsrTest.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgsrTest.Api.Infrastructure;

public class AgsrTestDbContext : DbContext
{
    public AgsrTestDbContext(DbContextOptions<AgsrTestDbContext> options)
        : base(options)
    {

    }

    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>()
            .Property(p => p.BirthDate)
            .HasColumnType("DATETIME2")
            .HasPrecision(0);
    }
}

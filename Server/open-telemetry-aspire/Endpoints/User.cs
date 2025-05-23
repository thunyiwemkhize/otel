using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace open_telemetry_aspire.Endpoints;

public record User
{
    public int Id { get; set; }

    [MinLength(10)]
    [MaxLength(20)]
    public string Name { get; set; }

    [MinLength(10)]
    [MaxLength(20)]
    public string Surname { get; set; }
}

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public DbSet<User> Persons { get; set; }
}

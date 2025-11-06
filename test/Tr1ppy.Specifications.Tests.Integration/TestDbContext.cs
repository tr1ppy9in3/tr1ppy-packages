using Microsoft.EntityFrameworkCore;
using Tr1ppy.Specifications.TestsCore;

namespace Tr1ppy.Specifications.IntegrationTests;

public class TestDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var users = new List<User>()
        {
            new User(Id: 1, Name: "JonhDoe", Email: "JonhDoe", Balance: 20000, Age: 21),
            new User(Id: 2, Name: "JanhDoe", Email: "JanhDoe", Balance: 30000, Age: 14),
        };

        modelBuilder.Entity<User>().HasData(users);
    }
}
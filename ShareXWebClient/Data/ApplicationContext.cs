using Microsoft.EntityFrameworkCore;
using ShareXWebClient.Models;

namespace ShareXWebClient.Data;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Text> Texts { get; set; } = null!;
    public DbSet<Link> Links { get; set; } = null!;
    public DbSet<Content> Contents { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
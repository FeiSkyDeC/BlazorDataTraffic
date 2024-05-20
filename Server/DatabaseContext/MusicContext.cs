using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.DatabaseContext;

public class MusicContext:DbContext
{
    public MusicContext(DbContextOptions<MusicContext> options) : base(options)
    {

    }

    public DbSet<Music> Musics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Music>(entity =>
        {
            entity.HasKey(m => m.Id);
        });
        base.OnModelCreating(modelBuilder);
    }
}


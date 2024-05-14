using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Shared.Models;

namespace Server.DatabaseContext;

public class WordDbContext:DbContext
{
    public WordDbContext(DbContextOptions<WordDbContext> options): base(options)
    {
    }

    public DbSet<Word> Words { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Word>(entity =>
        {
            // 设置主键
            entity.HasKey(e => e.Id); 
        });
        base.OnModelCreating(modelBuilder);
    }
}

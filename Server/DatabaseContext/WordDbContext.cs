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

    // 添加
    public DbSet<Word> Words { get; set; } = null!;

    // 配置
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Word>(entity =>
        {
            // 设置主键
            entity.HasKey(w => w.Id); 
        });
        base.OnModelCreating(modelBuilder);
    }
}

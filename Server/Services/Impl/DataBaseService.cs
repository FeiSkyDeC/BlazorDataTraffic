using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Server.DatabaseContext;
using Server.Services;

namespace Server.Services.Impl;
public class DatabaseService : IDatabaseService
{
    private readonly WordDbContext _context;

    public DatabaseService(WordDbContext context)
    {
        _context = context;
    }

    public async Task SaveWordCountsAsync(IDictionary<string, int> wordCounts)
    {
        // 创建一个WordCount实体列表来保存数据
        var wordCountEntities = new List<Word>();

        // 遍历wordCounts字典，创建WordCount实体并添加到列表中
        foreach (var wordCount in wordCounts)
        {
            wordCountEntities.Add(new Word
            {
                Name = wordCount.Key,
                Count = wordCount.Value
            });
        }

        // 将WordCount实体列表添加到DbContext中
        await _context.Words.AddRangeAsync(wordCountEntities);

        // 保存更改到数据库
        await _context.SaveChangesAsync();
    }
}


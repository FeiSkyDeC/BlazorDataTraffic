using Server.DatabaseContext;
using Shared.Models;

namespace Server.Services.Impl;

public class DbInitializer
{
    private List<Word> words = new List<Word>();

    public void RandomNumber()
    {
        Random rand = new Random();
        for (int i = 0; i < 30; i++)
        {
            int randomNumber = rand.Next(100);
            words.Add(new Word { Name = $"Word{i + 1}", Count = randomNumber });
        }
    }

    public static void Initialize(WordDbContext context)
    {
        //有数据就不创建了，其实没有
        if (context.Words.Any())
        {
            return;
        }

        //实例化类
        DbInitializer initializer = new DbInitializer();
        initializer.RandomNumber();

        context.Words.AddRange(initializer.words);
        //context.Words.Add(new Word { Name = "hello", Count = 0 });
        context.SaveChanges();
    }
}
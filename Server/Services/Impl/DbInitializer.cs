using Server.DatabaseContext;
using Shared.Models;
using System.Text;
using System;

namespace Server.Services.Impl;

public class DbInitializer
{
    // 生成数据
    private List<Word> words = new List<Word>();

    public void RandomNumber()
    {
        Random rand = new Random();
        // 生成随机字母并组成单词
        string letters = "abcdefghijklmnopqrstuvwxyz";
        int length = 6; //单词长度为6
        StringBuilder wordBuilder = new StringBuilder();

        for (int i = 0; i < 100; i++)
        {
            int randomNumber = rand.Next(100);

            for (int j = 0; j < length; j++)
            {
                // 从字母表中随机选择一个字母并添加到单词中
                char randomLetter = letters[rand.Next(letters.Length)];
                wordBuilder.Append(randomLetter);
            }

            words.Add(new Word { Name = $"{wordBuilder.ToString()}", Count = randomNumber });
            // 清空

            wordBuilder.Clear();
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

    //初始化Music数据库
    public static void InitializeMusic(MusicContext context)
    {
        if (context.Musics.Any())
        {
            return;
        }

        //实例化类
        //DbInitializer initializer = new DbInitializer();

        var musics = new Music[]
        {
            new Music { Name = "DreamItPossible", Path = "/Resource/DreamItPossible.mp3" },
            new Music { Name = "FoolForYou", Path = "/Resource/FoolForYou.mp3" },
            new Music { Name="Senorita", Path = "/Resource/Senorita.mp3" }
        };
        
        context.Musics.AddRange(musics);
        context.SaveChanges();
    }
}
using Microsoft.EntityFrameworkCore;
namespace Server;
using Server.DatabaseContext;
using Shared.Models;
using Server.Services.Impl;
using Server.Services;

public class Program
{
    public static void Main(string[] args)
    {
        // �����������Ժ��м����CORS
        var AllowSpecificOrigns = "allowSpecificOrgins";

        var builder = WebApplication.CreateBuilder(args);

        // ע�� CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name:AllowSpecificOrigns,
                policy =>
                {
                    // ������ �ͻ���  & ������ URL
                    policy.WithOrigins("https://localhost:7258",
                                        "https://localhost:7229");
                });
        });

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddServerSideBlazor();
        // Add Services
        //builder.Services.AddSingleton<IDatabaseService,DatabaseService>();
        //builder.Services.AddSingleton<IFileReader,FileReaderService>();

        //ע�����ݿ������ģ�����ڴ����ݿ�
        builder.Services.AddDbContext<WordDbContext>(opt 
            =>opt.UseInMemoryDatabase("WordDatabase"));
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<WordDbContext>();
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);
        }
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // ���CORS
        app.UseCors(AllowSpecificOrigns);

        app.UseAuthorization();
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.Run();
    }
}

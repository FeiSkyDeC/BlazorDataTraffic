using Microsoft.EntityFrameworkCore;
using Shared.Services;
using Shared.Services.Impl;

namespace Server;
using Server.DatabaseContext;
using Shared.Models;
using Server.Services.Impl;
using Server.Services;

public class Program
{
    public static void Main(string[] args)
    {
        // 具有命名策略和中间件的CORS
        var allowSpecificOrigns = "allowSpecificOrgins";

        var builder = WebApplication.CreateBuilder(args);

        // 注册 CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name:allowSpecificOrigns,
                policy =>
                {
                    // 本机的 客户端  & 服务器 URL
                    policy.WithOrigins("https://localhost:7258",
                                        "https://localhost:7229");
                });
        });

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddServerSideBlazor();

        //注册数据库上下文，添加内存数据库
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

        // 添加CORS
        app.UseCors(allowSpecificOrigns);

        app.UseAuthorization();
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.Run();
    }
}

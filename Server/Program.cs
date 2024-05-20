using Microsoft.EntityFrameworkCore;
using Server.DatabaseContext;
using Server.Services.Impl;
namespace Server;
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
        
        //确保数据库存放路径存在
        //var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Database");
        var dataDirectory = "D:/Database";
        AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
        if (!Directory.Exists(dataDirectory))
        {
            Directory.CreateDirectory(dataDirectory);
        }

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddServerSideBlazor();

        //注册数据库上下文，添加内存数据库
        builder.Services.AddDbContext<WordDbContext>(opt 
            =>opt.UseInMemoryDatabase("WordDatabase"));
        
        //添加Sql数据库
        builder.Services.AddDbContext<MusicContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MusicContext")));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //添加数据库异常筛选器
        //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        var app = builder.Build();

        //创建Word数据库
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<WordDbContext>();
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);
        }
        
        //创建Music数据库
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<MusicContext>();
            context.Database.EnsureCreated();
            DbInitializer.InitializeMusic(context);
        }
        
        // Configure the HTTP request pipeline.
        // If current env is development env
        // 启用 Swagger 调试工具
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            
            // 启用开发者异常页
            app.UseDeveloperExceptionPage();
        }
        else
        {

            // 启用全局错误处理中间件和HSTS中间件
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // 终结点路由
        app.MapGet("/hello", () => "Hello World!");

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // 添加CORS
        app.UseCors(allowSpecificOrigns);

        // 启用认证中间件
        //app.UseAuthentication();
        
        // 启用授权中间件
        app.UseAuthorization();
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.Run();
    }
}

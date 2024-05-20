using Microsoft.EntityFrameworkCore;
using Server.DatabaseContext;
using Server.Services.Impl;
namespace Server;
public class Program
{
    public static void Main(string[] args)
    {
        // �����������Ժ��м����CORS
        var allowSpecificOrigns = "allowSpecificOrgins";

        var builder = WebApplication.CreateBuilder(args);

        // ע�� CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name:allowSpecificOrigns,
                policy =>
                {
                    // ������ �ͻ���  & ������ URL
                    policy.WithOrigins("https://localhost:7258",
                                        "https://localhost:7229");
                });
        });
        
        //ȷ�����ݿ���·������
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

        //ע�����ݿ������ģ�����ڴ����ݿ�
        builder.Services.AddDbContext<WordDbContext>(opt 
            =>opt.UseInMemoryDatabase("WordDatabase"));
        
        //���Sql���ݿ�
        builder.Services.AddDbContext<MusicContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MusicContext")));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //������ݿ��쳣ɸѡ��
        //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        var app = builder.Build();

        //����Word���ݿ�
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<WordDbContext>();
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);
        }
        
        //����Music���ݿ�
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<MusicContext>();
            context.Database.EnsureCreated();
            DbInitializer.InitializeMusic(context);
        }
        
        // Configure the HTTP request pipeline.
        // If current env is development env
        // ���� Swagger ���Թ���
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            
            // ���ÿ������쳣ҳ
            app.UseDeveloperExceptionPage();
        }
        else
        {

            // ����ȫ�ִ������м����HSTS�м��
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // �ս��·��
        app.MapGet("/hello", () => "Hello World!");

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // ���CORS
        app.UseCors(allowSpecificOrigns);

        // ������֤�м��
        //app.UseAuthentication();
        
        // ������Ȩ�м��
        app.UseAuthorization();
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.Run();
    }
}

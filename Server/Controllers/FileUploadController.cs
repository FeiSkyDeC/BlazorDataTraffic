using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FileUploadController:ControllerBase
{

    /// <summary>
    /// Web主机环境
    /// </summary>
    private readonly IWebHostEnvironment env;
    
    public FileUploadController(IWebHostEnvironment env)
    {
        this.env = env;
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        try
        {
            if (file != null && file.Length > 0)
            {
                // var filePath = Path.Combine(env.ContentRootPath,
                //     env.EnvironmentName, "Resource", file.Name);

                //var filePath = $"./Resource/{file.Name}";
                
                var filePath = Path.Combine(env.WebRootPath, "Resource", file.FileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok(new { message = "文件上传成功" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Pages;

public partial class FileUpload : ComponentBase
{
    /// <summary>
    /// 文件名，这个可以[去掉]
    /// </summary>
    private string? fileName { get; set; }

    /// <summary>
    /// 上传文件的载体
    /// </summary>
    private List<IBrowserFile> loadedFiles = new List<IBrowserFile>();
    
    /// <summary>
    /// Http请求
    /// </summary>
    [Inject]
    private HttpClient HttpClient { get; set; }
    
    /// <summary>
    /// 文件上传
    /// </summary>
    /// <param name="e"></param>
    private async Task LoadFiles(InputFileChangeEventArgs e)
    {
        // 可以选中多个文件
        foreach (var file in e.GetMultipleFiles())
        {
            try
            {
                // use multipart/fdata MIME type
                using var content = new MultipartFormDataContent();
                
                var fileContent = new StreamContent(file.OpenReadStream());
                
                // 设置ContentType
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");
                // 设置Content-Disposition头，指定文件名
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "\"file\"",
                    FileName = "\"" + file.Name + "\""
                };

                content.Add(fileContent, "file", file.Name);
                
                var response = await HttpClient.PostAsync("https://localhost:7229/api/FileUpload/Upload", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("文件上传成功!");
                }
                else
                {
                    Console.WriteLine("文件上传error!");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("FileUpload.razor.cs: Line error!");
            }
        }
    }
}
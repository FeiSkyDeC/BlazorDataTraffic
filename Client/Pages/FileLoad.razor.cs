using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Client.Pages;

public partial class FileLoad : ComponentBase
{
    [Inject] private HttpClient HttpClient { get; set; }

    private List<Music> musicList = new List<Music>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var response = await HttpClient.GetAsync("api/Music/GetMusics");

            if (response.IsSuccessStatusCode)
            {
                // 读取响应内容
                musicList = await HttpClient.GetFromJsonAsync<List<Shared.Models.Music>>("https://localhost:7229/api/Music/GetMusics");

                // 触发组件状态更新s
                StateHasChanged();
            }
        }
        
    }


    private async Task PlayMusic()
    {
        
    }
}
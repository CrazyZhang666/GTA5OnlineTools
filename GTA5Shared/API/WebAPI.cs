using GTA5Shared.API.Resp;

using RestSharp;

namespace GTA5Shared.API;

public static class WebAPI
{
    private static readonly RestClient client;

    static WebAPI()
    {
        var options = new RestClientOptions()
        {
            MaxTimeout = 5000
        };
        client = new RestClient(options);
    }

    /// <summary>
    /// Http 简单GET请求
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<RespContent> GET(string url)
    {
        var sw = new Stopwatch();
        sw.Start();
        var respContent = new RespContent();

        try
        {
            var request = new RestRequest(url);

            var response = await client.ExecuteGetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                respContent.IsSuccess = true;

            respContent.Message = response.Content;
        }
        catch (Exception ex)
        {
            respContent.Message = ex.Message;
        }

        sw.Stop();
        respContent.ExecTime = sw.Elapsed.TotalSeconds;

        return respContent;
    }
}

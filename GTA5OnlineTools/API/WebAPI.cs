using GTA5OnlineTools.Utils;
using GTA5OnlineTools.API.Resp;
using GTA5OnlineTools.API.RespJson;

using RestSharp;

namespace GTA5OnlineTools.API;

public static class WebAPI
{
    private static readonly RestClient client;

    static WebAPI()
    {
        var options = new RestClientOptions()
        {
            MaxTimeout = 5000,
            ThrowOnAnyError = true
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

    /// <summary>
    /// 有道翻译API
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static async Task<RespContent> YouDao(string message)
    {
        var sw = new Stopwatch();
        sw.Start();
        var respContent = new RespContent();

        try
        {
            var request = new RestRequest("http://fanyi.youdao.com/translate?&doctype=json&type=AUTO&i=" + message);

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

    /// <summary>
    /// 获取有道翻译内容
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static async Task<string> GetYouDaoContent(string message)
    {
        var result = await YouDao(message);
        if (result.IsSuccess)
        {
            var youDao = JsonUtil.JsonDese<YouDao>(result.Message);

            var stringBuilder = new StringBuilder();

            foreach (var item in youDao.translateResult)
            {
                foreach (var t in item)
                {
                    stringBuilder.Append(t.tgt);
                }
            }

            return stringBuilder.ToString();
        }
        else
        {
            return result.Message;
        }
    }
}

namespace GTA5OnlineTools.Utils;

public static class JsonUtil
{
    /// <summary>
    /// 反序列化z配置
    /// </summary>
    private static JsonSerializerOptions Options1 = new JsonSerializerOptions()
    {
        IncludeFields = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// 序列化配置
    /// </summary>
    private static JsonSerializerOptions Options2 = new JsonSerializerOptions()
    {
        WriteIndented = true,
        IncludeFields = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    /// <summary>
    /// 反序列化，将json字符串转换成json类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static T JsonDese<T>(string result)
    {
        return JsonSerializer.Deserialize<T>(result, Options1);
    }

    /// <summary>
    /// 序列化，将json类转换成json字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonClass"></param>
    /// <returns></returns>
    public static string JsonSeri<T>(T jsonClass)
    {
        return JsonSerializer.Serialize(jsonClass, Options2);
    }
}

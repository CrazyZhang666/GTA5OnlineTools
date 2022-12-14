namespace GTA5OnlineTools.API.RespJson;

public class YouDao
{
    public string type { get; set; }
    public int errorCode { get; set; }
    public int elapsedTime { get; set; }
    public List<List<TranslateResultItemItem>> translateResult { get; set; }
    public class TranslateResultItemItem
    {
        public string src { get; set; }
        public string tgt { get; set; }
    }
}

namespace wxRobotApp.Common;

public class WeChatProcessInfo
{
    public int Pid { get; set; }
    public string WeChatVersion { get; set; } = string.Empty;
    public string DllVersion { get; set; } = string.Empty;
    public string WeChatId { get; set; } = string.Empty;
    public int MessagePort { get; set; }
}
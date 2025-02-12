using System.Text.Json.Nodes;

namespace Application;
internal sealed class OneSignalRequest
{
    public string AppId { get; set; }
    public Dictionary<string, string> Headings { get; set; }
    public Dictionary<string, string> Contents { get; set; }
    public string Url { get; set; }
    public string TargetChannel { get; set; }
    public List<string>? IncludedSegments { get; set; }
    public List<string>? IncludeSubscriptionIds { get; set; }
    public string AndroidGroup { get; set; }
    public Uri? BigPicture { get; set; }
    public string LargeIcon { get; set; }
    public string SmallIcon { get; set; }
    public string ChromeWebImage { get; set; }
}

namespace CarinaStudio.ULogViewer.Net;

class BaiduSearchProvider : SimpleSearchProvider
{
    public BaiduSearchProvider(IULogViewerApplication app) : base(app, "Bing", "https://www.baidu.com/s?wd=", "%20")
    { }
}
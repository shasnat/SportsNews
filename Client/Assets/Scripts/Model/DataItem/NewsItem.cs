using System.Collections;
using System.Collections.Generic;

public class NewsItem {
	private string _newsSnippet;
	public string NewsSnippet {get{return _newsSnippet;} set{_newsSnippet = value;}}

	private string _articleName;
	public string ArticleName {get{return _articleName;} set{_articleName = value;}}

	private List<string> _picUrls;
	public List<string> PicUrls {get{return _picUrls;} set{_picUrls = value;}}

	private string _url;
	public string Url {get{return _url;} set{_url = value;}}

	public NewsItem(Dictionary<string, object> data) {
		if(data != null) {
			NewsSnippet = Util.GetOrDefault<string>(data, NewsService.kText, string.Empty);
			ArticleName = Util.GetOrDefault<string>(data, NewsService.kArticleName, string.Empty);
			Url = Util.GetOrDefault<string>(data, NewsService.kUrl, string.Empty);
			string picsString = Util.GetOrDefault<string>(data, NewsService.kPics, string.Empty);
			PicUrls = new List<string>();
			PicUrls.Add(picsString);

		}
	}
}

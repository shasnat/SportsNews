using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrendingContentItem {
	private NewsItem _newsItem;
	public NewsItem NewsItem {get{return _newsItem;}}

	private string _message;
	public string Message {get{return _message;}}

	private string _user;
	public string User {get{return _user;}}

	public TrendingContentItem(Dictionary<string, object> data) {
		SetData(data);
	}

	public void SetData(Dictionary<string, object> data) {
		_message = Util.GetOrDefault<string>(data, "message", string.Empty);
		_user = Util.GetOrDefault<string>(data, "user", string.Empty);
		Dictionary<string, object> newsItemData = Util.GetOrDefault<Dictionary<string, object>>(data, "newsItem", null);
		_newsItem = new NewsItem(newsItemData);
	}
}

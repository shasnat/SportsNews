using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NewsManager : MonoBehaviour {

	public static NewsManager instance;

	private Dictionary<string, List<NewsItem>> _allNews;

	void Start() {
		instance = this;
		_allNews = new Dictionary<string, List<NewsItem>>();
	}

	public void GetNewsFor(string newsKey, Action<List<NewsItem>> successCallback, Action failureCallback) {
		if(!string.IsNullOrEmpty(newsKey)) {
			if(_allNews.ContainsKey(newsKey)) {
				successCallback(_allNews[newsKey]);
			}
			else {
				NewsService.FetchNews(null,
				(response) => {
					List<NewsItem> parsedNews = ParseNewsResponse(response);
					_allNews[newsKey] = parsedNews;
					if(parsedNews != null) {
						successCallback(_allNews[newsKey]);
					}
					else {
						failureCallback();
					}
				}, null);
			}
		}
	}

	private List<NewsItem> ParseNewsResponse(Dictionary<string, object> response) {
		List<NewsItem> newsList = null;
		if(response != null) {
			object rawContent = null;
			response.TryGetValue(NewsService.kContent, out rawContent);
			if(rawContent != null) {
				Dictionary<string, object> content = rawContent as Dictionary<string, object>;
				newsList = new List<NewsItem>();
				foreach(var rawValue in content.Values) {
					var data = rawValue as Dictionary<string, object>;
					newsList.Add(new NewsItem(data));
				}
			}
		}
		return newsList;
	}
}

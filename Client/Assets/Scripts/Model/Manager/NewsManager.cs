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

	public void GetTrendingContent(string trendingKey, Action<List<TrendingContentItem>> successCallback, Action failureCallback) {
		List<TrendingContentItem> trendingList = new List<TrendingContentItem>();

		Dictionary<string, object> data = new Dictionary<string, object>();

		Dictionary<string, object> firstNewsData = new Dictionary<string, object>();
		firstNewsData["text"] = "We've reached mid-June and Michael Vick is still without a job. On Tuesday's edition of NFL Total Access, the veteran quarterback made his pitch to general managers.\n \"(I'm) a proven winner. I think that I've done a lot throughout the course of my career,\" he began.";
		firstNewsData["articleName"] = "Seahawks_Win_Superbowl.txt";
		firstNewsData["url"] = "http://www.nfl.com/news/story/0ap3000000497443/article/michael-vick-sells-himself-to-nfl-general-managers";
		firstNewsData["pics"] = "NFLImage1";

		data["newsItem"] = firstNewsData;
		data["message"] = "I just saw this and I can't believe it. #NFL #JustSawThis #CantBelieveIt";
		data["user"] = "@Shasnat";
		TrendingContentItem trendingItem = new TrendingContentItem(data);

		trendingList.Add(trendingItem);
		trendingList.Add(trendingItem);
		trendingList.Add(trendingItem);
		trendingList.Add(trendingItem);

		if(successCallback != null) {
			successCallback(trendingList);
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

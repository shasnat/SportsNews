using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public static class NewsService {

	public const string kContent = "content";
	public const string kText = "text";
	public const string kArticleName = "articleName";
	public const string kUrl = "url";
	public const string kPics = "pics";
	public const string kSuccess = "success";

	public static void FetchNews(Dictionary<string, string> inputParams, Action<Dictionary<string, object>> onSuccess, 
	                                                   Action<Dictionary<string, object>> onFailure) {
		Dictionary<string, object> response = new Dictionary<string, object>();
		response["success"] = true;

		Dictionary<string, object> firstNewsData = new Dictionary<string, object>();
		firstNewsData[kText] = "We've reached mid-June and Michael Vick is still without a job. On Tuesday's edition of NFL Total Access, the veteran quarterback made his pitch to general managers.\n \"(I'm) a proven winner. I think that I've done a lot throughout the course of my career,\" he began.";
		firstNewsData[kArticleName] = "Seahawks_Win_Superbowl.txt";
		firstNewsData[kUrl] = "http://www.nfl.com/news/story/0ap3000000497443/article/michael-vick-sells-himself-to-nfl-general-managers";
		firstNewsData[kPics] = "NFLImage1";

		Dictionary<string, object> contentDict = new Dictionary<string, object>();
		contentDict["first"] = firstNewsData;
		contentDict["second"] = firstNewsData;
		contentDict["third"] = firstNewsData;
		contentDict["fourth"] = firstNewsData;

		response[kContent] = contentDict;
		onSuccess(response);
		/*
		ParseCloud.CallFunctionAsync<Dictionary<string,object>>("GetSuggestions", new Dictionary<string, object>()).ContinueWith(
			t => {
			var suggestions = t.Result;
			if(suggestions != null) {
				response[kSuccess] = true;
				Dictionary<string, object> contentDict = new Dictionary<string, object>();
				foreach(var kvp in suggestions) {
					contentDict[kvp.Key] = kvp.Value as Dictionary<string, object>;
				}
				response[kContent] = contentDict;
				onSuccess(response);
			}
		});*/
	}
}

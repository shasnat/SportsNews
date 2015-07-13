using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrendingScreenScript : BaseScrollScreen {

	public UnityEngine.UI.Text title;
	public UnityEngine.UI.GridLayoutGroup newsGrid;
	public GameObject newsRowPrefab;

	void Start () {
		NewsManager.instance.GetNewsFor("trending", SetData, Fail);
	}

	private void SetData(List<NewsItem> newsItems) {
		foreach(NewsItem newsItem in newsItems) {
			GameObject newsRowObject = Instantiate(newsRowPrefab);
			newsRowObject.transform.SetParent(newsGrid.transform, false);
			NewsRow newsRow = newsRowObject.GetComponent<NewsRow>();
			if(newsRow != null) {
				newsRow.ScrollContainer = this.ScrollContainer;
				newsRow.ScrollSnapContainer = this.ScrollSnapContainer;
				newsRow.SetData(newsItem);
			}
		}
	}

	private void Fail() {
		Debug.Log("Trending No data");
	}
}

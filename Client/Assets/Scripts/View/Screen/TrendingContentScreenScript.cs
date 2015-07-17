using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrendingContentScreenScript : MonoBehaviour {
	
	public UnityEngine.UI.Text title;
	public UnityEngine.UI.GridLayoutGroup contentGrid;
	public GameObject trendingContentRowPrefab;
	
	void Start() {
		NewsManager.instance.GetTrendingContent("trending", SetData, Fail);
	}
	
	private void SetData(List<TrendingContentItem> contentItems) {
		foreach(TrendingContentItem contentItem in contentItems) {
			GameObject contentRowObject = Instantiate(trendingContentRowPrefab);
			contentRowObject.transform.SetParent(contentGrid.transform, false);
			TrendingContentRow trendingContentRow = contentRowObject.GetComponent<TrendingContentRow>();
			if(trendingContentRow != null) {
				trendingContentRow.SetData(contentItem);
			}
		}
	}
	
	private void Fail() {
		Debug.Log("Trending No data");
	}
}

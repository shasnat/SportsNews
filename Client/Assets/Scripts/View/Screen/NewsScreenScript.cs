using UnityEngine;
using System.Collections;

public class NewsScreenScript : MonoBehaviour, IScreen {
	
	public UnityEngine.UI.Text text;
	public UnityEngine.UI.Text header;
	public UnityEngine.UI.Image image;

	private NewsItem _newsItem;

	public void TransitionUpdate(GUIManager.GUIStateData data) {
		if(data != null && data.metadata != null && data.type == GUIManager.GUIDataType.NewsItem) {
			SetData(data.metadata as NewsItem);
		}
	}

	public void Update() {
		if (Application.platform == RuntimePlatform.Android) {
			if(Input.GetKey(KeyCode.Escape)) {
				GUIManager.instance.Transition(GUIManager.kBackTransition);
			}
		}
	}

	public void SetData(NewsItem newsItem) {
		if(newsItem != null) {
			_newsItem = newsItem;
			SetText();
			SetImage();
		}
	}
	
	private void SetText() {
		if(text != null) {
			text.text = "Retrieving...";
			FileManager.GetFile(_newsItem.ArticleName, _newsItem.Url,
			(content) => {
				text.text = content;
			},
			() => {
				Debug.LogError("SHAY FileManager.GetFile failure callback");
				text.text = "did not succeed";
			});
		}
	}
	
	private void SetImage() {
		if(image != null) {
			image.sprite.name = _newsItem.PicUrls[0];
		}
	}
}
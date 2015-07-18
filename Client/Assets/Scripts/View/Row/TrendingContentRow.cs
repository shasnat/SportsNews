using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TrendingContentRow : MonoBehaviour {
	public UnityEngine.UI.Text message;
	public UnityEngine.UI.Text articleText;
	public UnityEngine.UI.Image image;
	public UnityEngine.UI.Button button;

	private TrendingContentItem _trendingItem;

	public void Start() {
		SetButton();
	}

	public void SetData(TrendingContentItem trendingItem) {
		if(trendingItem != null) {
			_trendingItem = trendingItem;
			SetUI();
		}
	}

	private void SetButton() {
		Util.SetButtonClickHandler(button, 
		() => {
			if(_trendingItem != null && _trendingItem.NewsItem != null) {
				GUIManager.GUIStateData data = new GUIManager.GUIStateData();
				data.type = GUIManager.GUIDataType.NewsItem;
				data.metadata = _trendingItem.NewsItem;
				GUIManager.instance.Transition(button.name, data);
			}
		});
	}

	private void SetUI() {
		if(_trendingItem.NewsItem != null) {
			string messageText = _trendingItem.User + " " + _trendingItem.Message; 
			Util.SetText(articleText, _trendingItem.NewsItem.NewsSnippet);
			Sprite sprite = AssetManager.instance.GetSprite(_trendingItem.NewsItem.PicUrls[0]);
			Util.SetImage(image, sprite);
			Util.SetText(message, messageText);
		}
	}
}

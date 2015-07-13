using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NewsRow : BaseScrollScreen, IEndDragHandler {//, IDragHandler {

	public UnityEngine.UI.Text text;
	public UnityEngine.UI.Image image;
	public UnityEngine.UI.Button button;

	private NewsItem _newsItem;

	public void SetData(NewsItem newsItem) {
		if(newsItem != null) {
			_newsItem = newsItem;
			SetText();
			SetImage();
			SetButton();
		}
	}
	/*
	public void OnDrag(PointerEventData data) {
//		if(scrollSnapContainer != null) {
			scrollSnapContainer.SendMessage("OnDrag", data );
			//scrollContainer.SendMessage("OnDrag", data );
//		}
	}*/

	public void OnEndDrag(PointerEventData data) {
		if(ScrollSnapContainer != null) {
			Debug.Log("On Drag End");
			ScrollSnapContainer.DragEnd();
		}
	}

	private void SetButton() {
		if(button != null) {
			button.onClick.AddListener(() => {
				ClickHandler(button.name);
			});
		}
	}

	void OnDestroy() {
		if(button != null) {
			button.onClick.RemoveAllListeners();
		}
	}

	private void ClickHandler(string name) {
	//	Application.OpenURL(_newsItem.Url);
		GUIManager.GUIStateData data = new GUIManager.GUIStateData();
		data.type = GUIManager.GUIDataType.NewsItem;
		data.metadata = _newsItem;
		GUIManager.instance.Transition(name, data);
	}

	private void SetText() {
		if(text != null) {
			text.text = _newsItem.NewsSnippet;
		}
	}

	private void SetImage() {
		if(image != null) {
			image.sprite.name = _newsItem.PicUrls[0];
		}
	}
}

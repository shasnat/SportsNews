using UnityEngine;
using System.Collections;

public class MainScreenScript : MonoBehaviour, IScreen {
	
	public UnityEngine.UI.GridLayoutGroup screenGrid;
	public GameObject trendingScreenPrefab;
	public GameObject[] screens;

	public void TransitionUpdate(GUIManager.GUIStateData data) {
	}

	void Start () {
		if(screens != null) {
			UnityEngine.UI.ScrollRect scrollRect = gameObject.GetComponent<UnityEngine.UI.ScrollRect>();
			ScrollRectSnap scrollSnap = gameObject.GetComponent<ScrollRectSnap>();
			foreach(GameObject screenPrefab in screens) {
				GameObject screenObject = Instantiate(screenPrefab);
				screenObject.transform.SetParent(screenGrid.transform, false);
				BaseScrollScreen scrollScreen = screenObject.GetComponent<BaseScrollScreen>();
				if(scrollScreen != null && scrollRect != null && scrollSnap != null) {
					scrollScreen.ScrollContainer = scrollRect;
					scrollScreen.ScrollSnapContainer = scrollSnap;
				}
			}
		}
	}
}

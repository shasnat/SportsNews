using UnityEngine;
using System.Collections;

public class StartupManager : MonoBehaviour {
	
	void Awake() {
		gameObject.AddComponent<UserManager>();
		gameObject.AddComponent<GUIManager>();
		gameObject.AddComponent<HttpRequestManager>();
		gameObject.AddComponent<NewsManager>();
		gameObject.AddComponent<ScheduleManager>();
	}
	void Start() {
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.orientation = ScreenOrientation.AutoRotation;
	}
}

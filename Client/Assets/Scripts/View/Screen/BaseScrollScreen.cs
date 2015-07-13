using UnityEngine;

public class BaseScrollScreen : MonoBehaviour {
	private UnityEngine.UI.ScrollRect _scrollContainer;
	public UnityEngine.UI.ScrollRect ScrollContainer {get{return _scrollContainer;} set{_scrollContainer = value;}}
	private ScrollRectSnap _scrollSnapContainer;
	public ScrollRectSnap ScrollSnapContainer {get{return _scrollSnapContainer;} set{_scrollSnapContainer = value;}}
}

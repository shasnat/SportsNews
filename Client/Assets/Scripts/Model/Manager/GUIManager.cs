using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance;
	public const string kBackTransition = "Back";
	public const string kPrefabsFolder = "Prefabs/";
	public const string kPopupsFolder = "Popups/";
	public const string kSchedulePopup = "SchedulePopup";
	private Stack _stateStack;
	private GUIState _currentState;
	private Dictionary<string, Dictionary<string, string>> _transitions;

	public class GUIState {
		public string screenName = null;
		public GUIStateData data = null;
	}

	public class GUIStateData {
		public GUIDataType type;
		public object metadata;
	}

	public enum GUIDataType {
		None,
		NewsItem,
		ScheduleItem
	}

	void Start() {
		instance = this;
		SetupStateStack();
		SetupTransitions();
	}

	private void SetupStateStack() {
		_stateStack = new Stack();
		GUIState initialState = new GUIState();
		initialState.screenName = "MainScreen";
		PushState(initialState);
	}

	private void PushState(GUIState state) {
		_stateStack.Push(state);
		_currentState = state;
	}

	//TODO: data drive this
	private void SetupTransitions() {
		_transitions = new Dictionary<string, Dictionary<string, string>>();
		_transitions["MainScreen"] = new Dictionary<string, string>();
		_transitions["MainScreen"]["ArticleButton"] = "NewsScreen";

		_transitions["NewsScreen"] = new Dictionary<string, string>();
		_transitions["NewsScreen"]["BackButton"] = "MainScreen";
	}

	public bool Transition(GUIState state) {
		bool success = false;
		if(state != null) {
			Object screenPrefab = Resources.Load(kPrefabsFolder+state.screenName);
			if(screenPrefab != null) {
				DestroyChildren();
				GameObject screenObject = Instantiate(screenPrefab) as GameObject;
				if(screenObject != null) {
					success = true;
					PushState(state);
					screenObject.transform.SetParent(gameObject.transform, false);
					IScreen screen = screenObject.GetComponent<IScreen>();
					if(screen != null) {
						screen.TransitionUpdate(state.data);
					}
				}
			}
		}
		return success;
	}

	private GUIState GetTransition(GUIState startState, string transitionName, GUIStateData data = null) {
		GUIState endState = null;
		if(startState != null) {
			if(_transitions.ContainsKey(startState.screenName)) {
				Dictionary<string, string> options = _transitions[startState.screenName];
				if(options.ContainsKey(transitionName)) {
					endState = new GUIState();
					endState.screenName = options[transitionName];
					endState.data = data;
				}
			}
		}
		return endState;
	}

	public bool Transition(string transitionName, GUIStateData data = null) {
		bool success = false;
		if(transitionName.Equals(kBackTransition)) {
			BackTransition();
		}
		else {
			GUIState endState = GetTransition(_currentState, transitionName);
			endState.data = data;
			Transition(endState);
		}
		return success;
	}

	public void SpawnPopup(GUIStateData data = null, string prefabName = kSchedulePopup) {
		Object popupPrefab = Resources.Load(kPrefabsFolder+kPopupsFolder+prefabName);
		if(popupPrefab != null) {
			GameObject prefabObject = Instantiate(popupPrefab) as GameObject;
			if(prefabObject != null) {
				prefabObject.transform.SetParent(this.transform, false);
				IScreen screen = prefabObject.GetComponent<IScreen>();
				if(screen != null) {
					screen.TransitionUpdate(data);
				}
			}
		}
	}

	private bool BackTransition() {
		bool success = false;
		if(_stateStack != null && _stateStack.Count > 1) {
			_stateStack.Pop();
			GUIState prevState = _stateStack.Pop() as GUIState;
			success = Transition(prevState);
		}
		return success;
	}

	private void DestroyChildren() {
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
}

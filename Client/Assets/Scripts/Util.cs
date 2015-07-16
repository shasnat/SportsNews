using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class Util {

	/// <summary>
	/// Gets value associated with key or returns default
	/// </summary>
	/// <param name="data">Data.</param>
	/// <param name="key">Key.</param>
	/// <param name="store">Store.</param>
	/// <param name="defaultVal">Default value.</param>
	public static T GetOrDefault<T>(Dictionary<string, object> data, string key, T defaultVal) {
		object storeObject = defaultVal;
		data.TryGetValue(key, out storeObject);
		return (T)storeObject;
	}

	public static long CurrentUnixTimestamp() {
		long unixTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
		return unixTimestamp;
	}

	public static void SetLabel(UnityEngine.UI.Text label, string text) {
		if(label != null) {
			label.text = text;
		}
	}

	public static void SetImage(UnityEngine.UI.Image image, Sprite sprite) {
		if(image != null && sprite != null) {
			image.sprite = sprite;
		}
	}

	public static void SetButtonClickHandler(UnityEngine.UI.Button button, UnityEngine.Events.UnityAction call) {
		if(button != null) {
			button.onClick.AddListener(call);
		}
	}

	public static void RemoveButtonClickHandlers(UnityEngine.UI.Button button) {
		if(button != null) {
			button.onClick.RemoveAllListeners();
		}
	}
}

using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using System.Collections;

public class HttpRequestManager : MonoBehaviour {
	public static HttpRequestManager instance;

	void Start() {
		instance = this;
	}

	public void Get(string uri = "http://www.contoso.com/default.html", Action<string> onSuccess=null, Action onFailure=null) {
#if UNITY_EDITOR
		GetEditor(uri, onSuccess, onFailure);
#elif UNITY_ANDROID
		GetAndroid(uri, onSuccess, onFailure);
#elif UNITY_IPHONE
#endif
	}

	private void GetEditor(string uri = "http://www.contoso.com/default.html", Action<string> onSuccess=null, Action onFailure=null) {
		Debug.LogError("SHAY http manager calling get");
		// Create a request for the URL. 
		WebRequest request = WebRequest.Create(uri);
		// If required by the server, set the credentials.
		request.Credentials = CredentialCache.DefaultCredentials;
		// Get the response.
		WebResponse response = request.GetResponse();
		// Display the status.
		Debug.Log(((HttpWebResponse)response).StatusDescription);
		// Get the stream containing content returned by the server.
		Stream dataStream = response.GetResponseStream();
		// Open the stream using a StreamReader for easy access.
		StreamReader reader = new StreamReader(dataStream);
		// Read the content.
		string responseFromServer = reader.ReadToEnd();
		// Display the content.
		Debug.Log(responseFromServer);
		if(onSuccess != null) {
			onSuccess(responseFromServer);
		}
		// Clean up the streams and the response.
		reader.Close();
		response.Close();
	}

	private void GetAndroid(string uri = "http://www.contoso.com/default.html", Action<string> onSuccess=null, Action onFailure=null) {
		WWW www = new WWW(uri);
		Debug.LogError("SHAY starting wait for request coroutine");
		StartCoroutine(WaitForRequest(www, onSuccess, onFailure));
	}

	private IEnumerator WaitForRequest(WWW www, Action<string> onSuccess, Action onFailure=null) {
		yield return www;
		
		// check for errors
		if (www.error == null)
		{
			Debug.LogError("SHAY WWW Ok!: " + www.text);
			if(onSuccess != null) {
				onSuccess(www.text);
			}
		} else {
			Debug.LogError("SHAY WWW Error: "+ www.error);
			if(onFailure != null) {
				onFailure();
			}
		}    
	}
}

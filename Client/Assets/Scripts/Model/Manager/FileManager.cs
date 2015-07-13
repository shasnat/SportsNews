/// <summary>
/// Manages reading files from and writing files to disk.
/// Primarily used for downloading and reading news article content.
/// </summary>

using System;
using System.IO;
using UnityEngine;

public class FileManager {

	private const string kArticlesDirectory = "/Articles/";

	public static void WriteToFile(string filePath, string content) {
		Debug.LogError("SHAY Calling WriteToFile " + filePath);
		System.IO.FileInfo file = new System.IO.FileInfo(filePath);
	/*	try {
			Debug.LogError("SHAY deleting /data/app/com.Shasnat.SportsNews-1.apk");
			File.Delete("/data/app/com.Shasnat.SportsNews-1.apk");
		}
		catch(Exception ex) {
			Debug.LogError("Shay could not delete /data/app/com.Shasnat.SportsNews-1.apk " + ex.Message);
		}
		try {
			Debug.LogError("SHAY deleting /data/app/com.Shasnat.SportsNews-2.apk");
			File.Delete("/data/app/com.Shasnat.SportsNews-2.apk");
		}
		catch(Exception ex) {
			Debug.LogError("Shay could not delete /data/app/com.Shasnat.SportsNews-1.apk " + ex.Message);
		}*/

		file.Directory.Create(); // If the directory already exists, this method does nothing.
		if(file.Directory.Exists) {
			Debug.LogError("SHAY Trying to write to " + filePath);
			System.IO.File.WriteAllText(filePath, content);
		}
		else {
			Debug.LogError("SHAY Couldn't to write to " + filePath);
		}
	}

	public static void SynchronousRead(string fileName, Action<string> onSuccess=null, Action onFailure= null) {
		try {
			Debug.LogError("SHAY trying to read " + Application.persistentDataPath + kArticlesDirectory + fileName);
			using(StreamReader sr = new StreamReader(Application.persistentDataPath + kArticlesDirectory + fileName)) {
				string line = sr.ReadToEnd();
				Debug.Log(line);
				if(onSuccess != null) {
					onSuccess(line);
				}
			//	onFailure();
			}
		}
		catch(Exception e) {
			Debug.LogError("SHAY The file could not be read:");
			Debug.LogError(e.Message);
			if(onFailure != null) {
				onFailure();
			}
		}
	}

	public static void GetFile(string fileName, string url, Action<string> onSuccess=null, Action onFailure= null) {
		//download file
		string content = string.Empty;
		HttpRequestManager.instance.Get(url,
		(response) => {
			Debug.LogError("SHAY HTTP Get response received");
			if(!string.IsNullOrEmpty(response)) {
				content = response;
				//write file to disk //Application.persistentDataPath or dataPath
				WriteToFile(Application.persistentDataPath + kArticlesDirectory + fileName, content);
				//read file
				SynchronousRead(fileName, onSuccess, onFailure);
			}
			else if(onFailure != null) {
				onFailure();
			}
		});
	}

	public static void AsynchronousRead(string fileName, Action onSuccess = null, Action onFailure = null) {

	}
}
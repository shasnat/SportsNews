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
		System.IO.FileInfo file = new System.IO.FileInfo(filePath);
		file.Directory.Create(); // If the directory already exists, this method does nothing.
		if(file.Directory.Exists) {
			System.IO.File.WriteAllText(filePath, content);
		}
		else {
			Debug.LogError("SHAY Couldn't to write to " + filePath);
		}
	}

	public static void SynchronousRead(string fileName, Action<string> onSuccess=null, Action onFailure= null) {
	/*	try {
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
		}*/
		if(onSuccess != null) {
			string text = GetFakeArticle();
			onSuccess(text);
		}
	}

	private static string GetFakeArticle() {
		string text = "We\'ve reached mid-June and Michael Vick is still without a job. On Tuesday\'s edition of NFL Total Access, the veteran quarterback made his pitch to general managers.\n\"(I\'m) a proven winner. I think that I've done a lot throughout the course of my career,\" he began. \"I've proved that I can win games and play with some consistency and be a leader. I think those are the qualities that you want in a quarterback.\n\"A guy who is confident and is going to make the team win. Make the team believe in themselves and go out there and play hard. You know I did that consistently over the course of my career. I know I can continue to do it.\" \nVick\'s frustration is perhaps understandable. He\'s not exactly an old man at 34, not when elders like Tom Brady, Drew Brees and Peyton Manning are still active and thriving.\nBut what separates Vick from those players is performance level in recent seasons. Vick hasn\'t consistently looked like his former MVP self since his 2010 comeback campaign with the Eagles. His one-year stop with the Jets was troubling for his inability to wrestle away the starting job from the endlessly error-prone Geno Smith.\nVick was asked if he has thought about the possibility that his NFL career has come to an end.\n\"Going through times like this you kind of think about it,\" he said. \"I know I have been playing a long time. It\'s been 13 years. I know I still have a lot of football left in me. You never want to think of it in that regard.\"\n\"I still know that I have a lot left in the tank and I still look forward to playing again one day.\"\nWe\'ll put Vick\'s chances of getting another shot at 50/50. Vick still moves the needle. Moving an offense is a different question.";
		return text;
	}

	public static void GetFile(string fileName, string url, Action<string> onSuccess=null, Action onFailure= null) {
		//download file
		string content = string.Empty;
		HttpRequestManager.instance.Get(url,
		(response) => {
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
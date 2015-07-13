using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class UserManager : MonoBehaviour {
	public static UserManager instance;

	public const string kUserName = "username";
	public const string kEmail = "email";
	public const string kPhone = "phone";
	public const string kPassword = "password";

	public void Start() {
		instance = this;
		ParseUser currentUser = ParseUser.CurrentUser;
		if(currentUser != null) {
			string name = currentUser[kUserName] as string;
			string password = "password";
			Login(name, password);
		}
		else {
			Dictionary<string, string> signUpParams = new Dictionary<string, string>();
			SignUp("shasnat", "password", "email@example.com", signUpParams);
		}
	}

	private void SignUp(string username, string password, string email, Dictionary<string, string> signUpParams) {
		Debug.Log("Signing Up");
		var user = new ParseUser()
		{
			Username = username,
			Password = password,
			Email = email
		};

		foreach(var kvp in signUpParams) {
			user[kvp.Key] = kvp.Value;
		}
		user.SignUpAsync();
	/*	Task signUpTask = user.SignUpAsync();
		signUpTask.ContinueWith(
		task => {
			Debug.Log("Calling cloud function");
			ParseCloud.CallFunctionAsync<Dictionary<string,object>>("GetSuggestions", new Dictionary<string, object>()).ContinueWith(
			t => {
				var suggestions = t.Result;
				if(suggestions != null) {
					foreach(var kvp in suggestions) {
						Debug.Log(kvp.Key + " " + kvp.Value);
					}
				}
			});
		});*/
	}

	public void Login(string name, string password) {
		Debug.Log("Calling Login");
		ParseUser.LogInAsync(name, password).ContinueWith(
		t => {
			if(t.IsFaulted || t.IsCanceled) {
				// The login failed. Check the error to see why.
			}
			else {
				// Login was successful.
			}
		});
	}

	public void LogOut() {
		ParseUser.LogOut();
		ParseUser currentUser = ParseUser.CurrentUser; // this will now be null
		if(currentUser != null) {
			throw new UnityException("LogOut failed. Current user not null.");
		}
	}

	public void OnDestroy() {
		LogOut();
	}
}

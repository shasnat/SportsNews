using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScheduleManager : MonoBehaviour {
	public static ScheduleManager instance;

	void Start() {
		instance = this;
	}

	public void GetScheduleFor(string team, Action<List<ScheduleItem>> successCallback, Action failureCallback) {
		ScheduleService.FetchSchedule(null, 
		(response) => {
			List<ScheduleItem> list = ParseScheduleResponse(response);
			if(successCallback != null) {
				successCallback(list);
			}
			else if (failureCallback != null) {
				failureCallback();
			}
		},
		(response) => {
			if(failureCallback != null) {
				failureCallback();
			}
		});
	}

	private List<ScheduleItem> ParseScheduleResponse(Dictionary<string, object> response) {
		List<ScheduleItem> scheduleList = null;
		if(response != null) {
			Dictionary<string, object> content = null;
			content = Util.GetOrDefault<Dictionary<string,object>>(response, ScheduleService.kContent, null);
			if(content != null) {
				scheduleList = new List<ScheduleItem>();
				foreach(var rawValue in content.Values) {
					var data = rawValue as Dictionary<string, object>;
					scheduleList.Add(new ScheduleItem(data));
				}
			}
			else {
				Debug.LogError("Content was null");
			}
		}
		return scheduleList;
	}
}

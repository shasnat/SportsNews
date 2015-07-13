using System;
using System.Collections;
using System.Collections.Generic;

public static class ScheduleService {

	public const string kContent = "content";
	public const string kHomeTeam = "homeTeam";
	public const string kAwayTeam = "awayTeam";
	public const string kHomeScore = "homeScore";
	public const string kAwayScore = "awayScore";
	public const string kMatchTimestamp = "timestamp";
	
	public static void FetchSchedule(Dictionary<string, string> inputParams, Action<Dictionary<string, object>> onSuccess, 
	                             Action<Dictionary<string, object>> onFailure) {
		Dictionary<string, object> response = new Dictionary<string, object>();
		response["success"] = true;

		Dictionary<string, object> scheduleData = new Dictionary<string, object>();
		scheduleData[kHomeTeam] = "Jets";
		scheduleData[kAwayTeam] = "Patriots";
		scheduleData[kHomeScore] = 4;
		scheduleData[kAwayScore] = 3;
		scheduleData[kMatchTimestamp] = Util.CurrentUnixTimestamp() - 60;

		Dictionary<string, object> scheduleDataTwo = new Dictionary<string, object>();
		scheduleDataTwo[kHomeTeam] = "Colts";
		scheduleDataTwo[kAwayTeam] = "Buckaneers";
		scheduleDataTwo[kHomeScore] = 9;
		scheduleDataTwo[kAwayScore] = 2;
		scheduleDataTwo[kMatchTimestamp] = Util.CurrentUnixTimestamp() + 60;
		
		Dictionary<string, object> contentDict = new Dictionary<string, object>();
		contentDict["first"] = scheduleData;
		contentDict["second"] = scheduleDataTwo;
		contentDict["third"] = scheduleData;
		contentDict["fourth"] = scheduleDataTwo;

		response[kContent] = contentDict;

		onSuccess(response);
	}
}

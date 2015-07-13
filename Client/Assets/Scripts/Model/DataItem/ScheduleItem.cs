using System.Collections;
using System.Collections.Generic;

public class ScheduleItem {
	private string _homeTeam;
	public string HomeTeam {get{return _homeTeam;} set{_homeTeam = value;}}

	private string _awayTeam;
	public string AwayTeam {get{return _awayTeam;} set{_awayTeam = value;}}

	private int _homeScore;
	public int HomeScore {get{return _homeScore;} set{_homeScore = value;}}

	private int _awayScore;
	public int AwayScore {get{return _awayScore;} set{_awayScore = value;}}

	private long _matchTimestamp;
	public long MatchTimestamp {get{return _matchTimestamp;} set{_matchTimestamp = value;}}

	public ScheduleItem(Dictionary<string, object> data) {
		if(data != null) {
			HomeTeam = Util.GetOrDefault<string>(data, ScheduleService.kHomeTeam, string.Empty);
			AwayTeam = Util.GetOrDefault<string>(data, ScheduleService.kAwayTeam, string.Empty);
			HomeScore = Util.GetOrDefault<int>(data, ScheduleService.kHomeScore, 0);
			AwayScore = Util.GetOrDefault<int>(data, ScheduleService.kAwayScore, 0);
			MatchTimestamp = Util.GetOrDefault<long>(data, ScheduleService.kMatchTimestamp, 0);
		}
	}

	public string GetOpponent() {
		string opponent = string.Empty;
		if(!string.IsNullOrEmpty(HomeTeam) && !string.IsNullOrEmpty(AwayTeam)) {
			//TODO: remove Jets. If user's team isn't HomeTeam, they are the opponents
			if(HomeTeam != "Jets") {
				opponent = HomeTeam;
			}
			else {
				opponent = AwayTeam;
			}
		}
		return opponent;
	}
}

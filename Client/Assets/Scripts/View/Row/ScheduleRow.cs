using UnityEngine;
using System.Collections;

public class ScheduleRow : BaseScrollScreen {

	public UnityEngine.UI.Image teamLogo;
	public UnityEngine.UI.Text opponentLabel;
	public UnityEngine.UI.Text dateLabel;
	public UnityEngine.UI.Text timeLabel;
	public UnityEngine.UI.Text homeAwayLabel;
	public UnityEngine.UI.Text winLossLabel;
	public UnityEngine.UI.Button button;

	private ScheduleItem _scheduleItem;

	public void SetData(ScheduleItem scheduleItem) {
		if(scheduleItem != null) {
			_scheduleItem = scheduleItem;
			SetTeamLogo();
			SetOpponentLabel();
			SetDateLabel();
			SetTimeLabel();
			SetHomeAwayLabel();
			SetWinLossLabel();
		}
	}

	private void SetTeamLogo() {
		if(teamLogo != null && !string.IsNullOrEmpty(_scheduleItem.HomeTeam) && !string.IsNullOrEmpty(_scheduleItem.AwayTeam)) {
			string spriteName = _scheduleItem.GetOpponent() + ".png";
			teamLogo.sprite.name = spriteName;
		}
	}

	private void SetOpponentLabel() {
		if(opponentLabel != null) {
			opponentLabel.text = _scheduleItem.GetOpponent();
		}
	}

	private void SetDateLabel() {
		if(dateLabel != null) {
			dateLabel.text = _scheduleItem.MatchTimestamp.ToString();
		}
	}

	private void SetTimeLabel() {
		if(timeLabel != null) {
			timeLabel.text = _scheduleItem.MatchTimestamp.ToString();
		}
	}

	private void SetHomeAwayLabel() {
		if(homeAwayLabel != null) {
			bool isHome = _scheduleItem.HomeTeam.Equals("Jets");
			string homeAwayText = isHome ? "Home" : "Away";
			homeAwayLabel.text = homeAwayText;
		}
	}

	private void SetWinLossLabel() {
		if(winLossLabel != null) {
			bool matchPassed = _scheduleItem.MatchTimestamp < Util.CurrentUnixTimestamp();
			winLossLabel.gameObject.SetActive(matchPassed);
			if(matchPassed) {
				string winLossText = "L";
				winLossLabel.text = winLossText;
			}
		}
	}
}

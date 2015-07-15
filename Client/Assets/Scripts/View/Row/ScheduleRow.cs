using UnityEngine;
using System.Collections;

public class ScheduleRow : BaseScrollScreen {

	public UnityEngine.UI.Image homeTeamLogo;
	public UnityEngine.UI.Text homeTeamLabel;
	public UnityEngine.UI.Image awayTeamLogo;
	public UnityEngine.UI.Text awayTeamLabel;
	public UnityEngine.UI.Text dateLabel;
	public UnityEngine.UI.Text timeLabel;
	public UnityEngine.UI.Text homeAwayLabel;
	public UnityEngine.UI.Text winLossLabel;
	public UnityEngine.UI.Button button;

	private ScheduleItem _scheduleItem;

	public void SetData(ScheduleItem scheduleItem) {
		if(scheduleItem != null) {
			_scheduleItem = scheduleItem;
			SetTeamLogos();
			SetTeamNameLabels();
			SetDateLabel();
			SetTimeLabel();
			SetHomeAwayLabel();
			SetWinLossLabel();
			SetButton();
		}
	}

	private void SetTeamLogos() {
		if(!string.IsNullOrEmpty(_scheduleItem.HomeTeam) && !string.IsNullOrEmpty(_scheduleItem.AwayTeam)) {
			string spriteName = _scheduleItem.HomeTeam + ".png";
			Util.SetImage(homeTeamLogo, spriteName);

			spriteName = _scheduleItem.AwayTeam + ".png";
			Util.SetImage(awayTeamLogo, spriteName);
		}
	}

	private void SetTeamNameLabels() {
		Util.SetLabel(homeTeamLabel, _scheduleItem.HomeTeam);
		Util.SetLabel(awayTeamLabel, _scheduleItem.AwayTeam);
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

	private void SetButton() {
		Util.SetButtonClickHandler(button,
		() => {
			ClickHandler(button.name);
		});
	}

	private void ClickHandler(string name) {
		GUIManager.GUIStateData data = new GUIManager.GUIStateData();
		data.type = GUIManager.GUIDataType.ScheduleItem;
		data.metadata = _scheduleItem;
		GUIManager.instance.SpawnPopup(data);
	}

	void OnDestroy() {
		Util.RemoveButtonClickHandlers(button);
	}
}

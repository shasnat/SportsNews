using UnityEngine;
using System.Collections;

public class SchedulePopupScript : MonoBehaviour, IScreen {

	public UnityEngine.UI.Button okButton;
	public UnityEngine.UI.Button closeButton;
	public UnityEngine.UI.Button bgButton;
	public UnityEngine.UI.Image homeTeamLogo;
	public UnityEngine.UI.Image awayTeamLogo;
	public UnityEngine.UI.Image homeBG;
	public UnityEngine.UI.Image awayBG;
	public UnityEngine.UI.Button homeTeamButton;
	public UnityEngine.UI.Button awayTeamButton;
	public UnityEngine.UI.Text homeTeamName;
	public UnityEngine.UI.Text awayTeamName;
	public UnityEngine.UI.Text homeTeamRecord;
	public UnityEngine.UI.Text awayTeamRecord;
	public UnityEngine.UI.Text betAmountLabel;
	public UnityEngine.UI.Slider betAmountSlider;
	public UnityEngine.UI.Text dateLabel;


	private ScheduleItem _scheduleItem;
	private int _betAmount = 0;

	private void Start() {
		Util.SetButtonClickHandler(okButton, OKClickHandler);
		Util.SetButtonClickHandler(closeButton, CloseClickHandler);
		Util.SetButtonClickHandler(bgButton, CloseClickHandler);
	}

	public void TransitionUpdate(GUIManager.GUIStateData data) {
		if(data != null && data.metadata != null && data.type == GUIManager.GUIDataType.ScheduleItem) {
			SetData(data.metadata as ScheduleItem);
		}
	}

	private void SetData(ScheduleItem scheduleItem) {
		if(scheduleItem != null) {
			_scheduleItem = scheduleItem;
			SetSlider();
			SetTeamLogos();
			SetTeamNames();
			SetTeamRecords();
			SetTeamBGs();
			SetTeamButtons();
		}
	}

	private void SetTeamBGs() {
		if(homeBG != null) {
			homeBG.color = Color.green;
		}
		if(awayBG != null) {
			awayBG.color = Color.grey;
		}
	}

	private void SetTeamButtons() {
		Util.SetButtonClickHandler(homeTeamButton,
		() => {
			SelectTeam(homeTeamButton.name);
		});
		Util.SetButtonClickHandler(awayTeamButton,
		() => {
			SelectTeam(awayTeamButton.name);
		});
	}

	private void SelectTeam(string buttonName) {
		if(buttonName.Equals("HomeTeam")) {
			if(homeBG != null) {
				homeBG.color = Color.green;
			}
			if(awayBG != null) {
				awayBG.color = Color.grey;
			}
		}
		else if(buttonName.Equals("AwayTeam")) {
			if(homeBG != null) {
				homeBG.color = Color.grey;
			}
			if(awayBG != null) {
				awayBG.color = Color.green;
			}
		}
	}

	private void SetTeamLogos() {
		string homeSpriteName = _scheduleItem.HomeTeam + "Logo";
		Sprite homeSprite = AssetManager.instance.GetSprite(homeSpriteName);
		string awaySpriteName = _scheduleItem.AwayTeam + "Logo";
		Sprite awaySprite = AssetManager.instance.GetSprite(awaySpriteName);
		Util.SetImage(homeTeamLogo, homeSprite);
		Util.SetImage(awayTeamLogo, awaySprite);
	}

	private void SetTeamNames() {
		Util.SetLabel(homeTeamName, _scheduleItem.HomeTeam);
		Util.SetLabel(awayTeamName, _scheduleItem.AwayTeam);
	}

	private void SetTeamRecords() {
		string homeRecordString = "4-5";
		string awayRecordString = "6-3";
		Util.SetLabel(homeTeamRecord, homeRecordString);
		Util.SetLabel(awayTeamRecord, awayRecordString);
	}

	private void SetSlider() {
		if(betAmountSlider != null) {
			betAmountSlider.onValueChanged.AddListener(UpdateBetAmount);
			betAmountSlider.value = betAmountSlider.minValue;
		}
	}

	private void UpdateBetAmount(float amount) {
		_betAmount = (int)amount;
		if(betAmountLabel != null) {
			betAmountLabel.text = "$" + _betAmount.ToString();
		}
	}

	private void OKClickHandler() {
		Destroy(gameObject);
	}

	private void CloseClickHandler() {
		Destroy(gameObject);
	}

	private void OnDestroy() {
		Util.RemoveButtonClickHandlers(okButton);
		Util.RemoveButtonClickHandlers(closeButton);
		Util.RemoveButtonClickHandlers(bgButton);
	}
}

using UnityEngine;
using System.Collections;

public class SchedulePopupScript : MonoBehaviour, IScreen {

	public UnityEngine.UI.Button okButton;
	public UnityEngine.UI.Button closeButton;

	private ScheduleItem _scheduleItem;

	private void Start() {
		SetOKButton();
		SetCloseButton();
	}

	public void TransitionUpdate(GUIManager.GUIStateData data) {
		if(data != null && data.metadata != null && data.type == GUIManager.GUIDataType.ScheduleItem) {
			SetData(data.metadata as ScheduleItem);
		}
	}

	private void SetData(ScheduleItem scheduleItem) {
		if(scheduleItem != null) {
			_scheduleItem = scheduleItem;
		}
	}

	private void SetOKButton() {
		if(okButton != null) {
			okButton.onClick.AddListener(() => {
				OKClickHandler();
			});
		}
	}

	private void OKClickHandler() {
		Destroy(gameObject);
	}

	private void SetCloseButton() {
		if(closeButton != null) {
			closeButton.onClick.AddListener(() => {
				CloseClickHandler();
			});
		}
	}

	private void CloseClickHandler() {
		Destroy(gameObject);
	}

	private void OnDestroy() {
		if(okButton != null) {
			okButton.onClick.RemoveAllListeners();
		}
		if(closeButton != null) {
			closeButton.onClick.RemoveAllListeners();
		}
	}
}

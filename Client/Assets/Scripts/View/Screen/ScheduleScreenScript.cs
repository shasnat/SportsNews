using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class ScheduleScreenScript : BaseScrollScreen {

	public UnityEngine.UI.Text title;
	public UnityEngine.UI.GridLayoutGroup scheduleGrid;
	public GameObject scheduleRowPrefab;

	void Start () {
		ScheduleManager.instance.GetScheduleFor("Jets", SetData, Fail);
	}

	private void SetData(List<ScheduleItem> scheduleItems) {
		if(scheduleItems != null && scheduleRowPrefab != null) {
			foreach(ScheduleItem scheduleItem in scheduleItems) {
				GameObject scheduleRowObject = Instantiate(scheduleRowPrefab);
				scheduleRowObject.transform.SetParent(scheduleGrid.transform, false);
				ScheduleRow scheduleRow = scheduleRowObject.GetComponent<ScheduleRow>();
				if(scheduleRow != null) {
					scheduleRow.ScrollContainer = this.ScrollContainer;
					scheduleRow.ScrollSnapContainer = this.ScrollSnapContainer;
					scheduleRow.SetData(scheduleItem);
				}
			}
		}
	}

	private void Fail() {
		Debug.Log("Schedule No data");
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkingMeterUI : MonoBehaviour
{

	ParkingMeterMono currentMeter;

	[SerializeField]
	private Text nameField;
	[SerializeField]
	private Text priceField;

	[SerializeField]
	private Text timeRemainingField;


	[SerializeField]
	private Text currentBalanceField;

    // Start is called before the first frame update
	public void UpdateUI(ParkingMeterMono meterInfo)
	{
		nameField.text = meterInfo.transform.name;
		priceField.text = string.Format("{0:C} /hr", meterInfo.parkingRate);
		currentBalanceField.text = string.Format("Collect - {0:C}", meterInfo.currentInMachine);
		currentMeter = meterInfo;
	}

	void FixedUpdate()
	{
		if (currentMeter != null && currentMeter.parkingRate > 0)
		{
			timeRemainingField.text = string.Format("{0:hh\\:mm}", TimeSpan.FromHours(currentMeter.currentRemaining / currentMeter.parkingRate));
		}
		else
		{
			timeRemainingField.text = "0";
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectParkingMeter : MonoBehaviour
{

	public static SelectParkingMeter Instance;

	public Transform[] parkingMeters;

	[SerializeField]
	public ParkingMeterUI UI_Increment;
	private double incrementValue = 0.10f;

	private double currentAmountOfFunds = 0.00f;
	[SerializeField]
	private Text currentAmountOfFundsTextField;

	private ParkingMeterMono activeParkingMeterSelecction;
	public ParkingMeterMono ActiveParkingMeterSelection
	{
		get
		{
			return activeParkingMeterSelecction;
		}
		set
		{
			//grody implementation
			if (value == null) {
				activeParkingMeterSelecction = value;
				UI_Increment.gameObject.SetActive(false);

				//disable UI 
				
			}
			else if(!(new List<Transform>(parkingMeters).Contains(value.transform)))
			{
				Debug.LogError("Invalid selection for parking meter. (Is it set up correctly? Is it included in managing list?)");
			}
			else
			{
				if (value != activeParkingMeterSelecction)
				{
					activeParkingMeterSelecction = value;
					UI_Increment.gameObject.SetActive(true);
					UI_Increment.UpdateUI(activeParkingMeterSelecction);
					//enable UI
					//move UI to screen position
					//populate and link UI
				}
			}
		}
	}
    // Start is called before the first frame update
    void Start()
    {
		Instance = this;
		UI_Increment.gameObject.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray,out hit, 500.0f))
			{
				if (hit.transform.tag.Equals("ParkingMeter"))
				{
					ActiveParkingMeterSelection = hit.transform.GetComponent<ParkingMeterMono>();

				}
			}
		}       
    }

	public void IncrementSelectedMeter()
	{
		if(activeParkingMeterSelecction != null)
		{
			activeParkingMeterSelecction.parkingRate += incrementValue;
			UI_Increment.UpdateUI(activeParkingMeterSelecction);
		}
	}

	public void DecrementSelectedMeter()
	{
		if(activeParkingMeterSelecction != null)
		{
			activeParkingMeterSelecction.parkingRate -= incrementValue;
			UI_Increment.UpdateUI(activeParkingMeterSelecction);
		}
	}

	public void CollectMoneyFromMeter()
	{
		if(activeParkingMeterSelecction != null)
		{
			currentAmountOfFunds += activeParkingMeterSelecction.currentInMachine;
			activeParkingMeterSelecction.currentInMachine = 0;
			UI_Increment.UpdateUI(activeParkingMeterSelecction);
			currentAmountOfFundsTextField.text = String.Format("{0:C2}", currentAmountOfFunds);
		}
	}

	public void IssueCitation()
	{
		if (activeParkingMeterSelecction.currentlyParkedCar != null && activeParkingMeterSelecction.currentRemaining <= 0)
		{
			currentAmountOfFunds += 50.0f;
			currentAmountOfFundsTextField.text = String.Format("{0:C2}", currentAmountOfFunds);
			CarController towedCar = activeParkingMeterSelecction.currentlyParkedCar;
			activeParkingMeterSelecction.currentlyParkedCar = null;
			GameObject.Destroy(towedCar.gameObject);
		}
		else
		{
			Debug.LogWarning("can't tow legal parking");
		}
	}
}

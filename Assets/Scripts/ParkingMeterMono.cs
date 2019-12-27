using System;
using UnityEngine;

[RequireComponent(typeof (Outline))]
public class ParkingMeterMono : MonoBehaviour
{

	
	private Outline outlineComponent;

	public double parkingRate = 0.10f;

	public double currentInMachine = 0.00f;
	public double currentRemaining = 0.00f;
	// Start is called before the first frame update

	[SerializeField]
	public CarController currentlyParkedCar = null;

	[SerializeField]
	private Light colorLight;
	[SerializeField]
	private Transform parkAnchor = null;
	void Start()
    {
		outlineComponent = GetComponent<Outline>();
		outlineComponent.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		//convert time to milliseconds
		//convert milleseconds to "hours" 
		if (currentRemaining > 0)
		{
			double hoursPass = TimeOfDay.RealTimeToGameTime(TimeSpan.FromSeconds(Time.deltaTime)).TotalHours;
			currentRemaining -= parkingRate * hoursPass;
			if(currentRemaining <= 0 && currentlyParkedCar == null)
			{
				currentRemaining = 0;
				//colorLight.color = Color.red;
				//newly out of time
			}
			
			if(currentRemaining <= 0)
			{
				colorLight.color = Color.red;
			}
			else
			{
				colorLight.color = Color.green;
			}
		}

		if(currentlyParkedCar != null)
		{
			currentlyParkedCar.timeToSpendAtMeter -= TimeOfDay.RealTimeToGameTime(TimeSpan.FromSeconds(Time.fixedDeltaTime));
			if(currentlyParkedCar.timeToSpendAtMeter.TotalSeconds <= 0)
			{
				currentlyParkedCar.isMoving = true;
				currentlyParkedCar.transform.position = parkAnchor.position;
				currentlyParkedCar = null;
			}
		}

	}
	void OnMouseOver()
	{
		outlineComponent.enabled = true;
	}

	void OnMouseExit()
	{
		outlineComponent.enabled = false;
	}
	
	public void AddMoney(double amount)
	{
		currentInMachine += amount;
		currentRemaining += amount;
	}

	public string GetTimeRemaining()
	{
		if (parkingRate > 0)
		{
			return string.Format("{0:mm\\:ss}",(currentRemaining / parkingRate));
		}
		else
		{
			return "0:00";
		}
	}
	public void ParkCar(CarController car)
	{
		currentlyParkedCar = car;
		car.transform.position = parkAnchor.position;
		car.isMoving = false;

		//need to check here
		double amountofMoney = (parkingRate * car.timeToSpendAtMeter.TotalHours);
		if(amountofMoney > car.moneyOnHand)
		car.moneyOnHand -= amountofMoney;
		AddMoney(amountofMoney);

		if (SelectParkingMeter.Instance.ActiveParkingMeterSelection != null  && SelectParkingMeter.Instance.ActiveParkingMeterSelection.Equals(this))
		{
			SelectParkingMeter.Instance.UI_Increment.UpdateUI(this);
		}
	}
}

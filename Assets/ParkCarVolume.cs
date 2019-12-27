using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkCarVolume : MonoBehaviour
{
	[SerializeField]
	ParkingMeterMono meter;
    // Start is called before the first frame update
    void Start()
    {
		        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.name);
		if (meter.currentlyParkedCar == null && other.tag.Equals("Car") && other.transform.GetComponent<CarController>().timeToSpendAtMeter.TotalSeconds > 0)
		{
			//grab the car
			Debug.Log("grabbed car");
			meter.ParkCar(other.transform.GetComponent<CarController>());

		}
		else
		{
			//don't grab the car
		}
	}
}

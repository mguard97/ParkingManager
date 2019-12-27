
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
	float spawnFrequency = 4.0f;

	[SerializeField]
	private GameObject carPrefab;

	public Transform start1, end1;
	public Transform start2, end2;

	//x is value of minutes stayed, y is probability (as 1/y)
	[SerializeField]
	public Vector2[] randomTimeLookup;

	//x is % of holders, y is probability (as 1/y)
	[SerializeField]
	public Vector2[] randomWealthLookup;

	bool spawnCars = true;
	IEnumerator SpawnCars(Transform start, Transform end)
	{
		while (spawnCars)
		{
			float variance = UnityEngine.Random.Range(-1.0f, 2.0f);
			//spawn car
			GameObject newCar = CreateCar();
			newCar.transform.position = start.position;
			newCar.GetComponent<CarController>().roadEndTarget = end;
			//Debug.Log("Spawned Car");
			//wait for next car
			yield return new WaitForSeconds(spawnFrequency + variance);
		}
	}

	void Start()
	{
		IEnumerator rightRoadRoutine = SpawnCars(start1, end1);
		IEnumerator leftRoadRoutine = SpawnCars(start2, end2);

		StartCoroutine(rightRoadRoutine);
		StartCoroutine(leftRoadRoutine);
	}

	GameObject CreateCar()
	{
		GameObject newCar = GameObject.Instantiate(carPrefab);
		CarController controller = newCar.GetComponent<CarController>();

		//general strategy for population:
		//several different types of users, inverse relationship with time.
		//15 min = 1/96 full day
		// 15 min - hr short trips 
		// 1 hr - 2 hr med trips
		// 3 hr - 6 hr
		// 6 hr +

		//int selection = Random.Range(0, randomTimeLookup.Length);
		//int actualAmountOfTicks =(int)( Random.Range(randomTimeLookup[selection].x, randomTimeLookup[selection + 1].x) * TimeOfDay.lengthOfDayInMilliSeconds/1440);

		float doesWantToStop = UnityEngine.Random.Range(0, 10);
		if (doesWantToStop > 3)
		{
			controller.timeToSpendAtMeter = TimeSpan.FromMinutes((int)GetEvenRandomLookup(randomTimeLookup));
		}
		else
		{
			controller.timeToSpendAtMeter = TimeSpan.FromMinutes(0);
		}
		//controller.timeToSpendAtMeter = actualAmountOfTicks;

		//different economic classes, bell curve
		// wealthy - 100's
		// middle class 20's
		// average < 10 
		// many < 2

		//selection = Random.Range(0, randomWealthLookup.Length);
		
		controller.moneyOnHand = GetEvenRandomLookup(randomWealthLookup);

		Debug.Log(controller.timeToSpendAtMeter + ", " + string.Format("{0:C}",controller.moneyOnHand));
		//controller.moneyOnHand = (decimal)(Random.Range(randomWealthLookup[selection].x, randomWealthLookup[selection + 1].x));
		return newCar;
	}

	double GetEvenRandomLookup(Vector2[] array_2D)
	{
		float sum = 0;
		for( int i = 0; i < array_2D.Length; i++)
		{
			sum += array_2D[i].y;
		}

		float preIndex = UnityEngine.Random.Range(0, sum);

		int currentIndex = 0;
		int currentSum = 0;
		do
		{
			currentSum += (int)array_2D[currentIndex].y;
			currentIndex++;
		} while (currentSum <= preIndex & currentIndex < array_2D.Length - 2);
		

		return UnityEngine.Random.Range(array_2D[currentIndex].x, randomWealthLookup[currentIndex + 1].x);
	}
}



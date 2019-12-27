using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOfDay : MonoBehaviour
{
	private static TimeOfDay instance;
	public static TimeOfDay Instance
	{
		get => instance;
	}
	[SerializeField]
	private Light directionalSunLight;
	[SerializeField]
	public static double lengthOfDayInSeconds = 300.0;

	[SerializeField]
	public static TimeSpan dayTimeStart = TimeSpan.FromHours(6);
	[SerializeField]
	public static TimeSpan dayTimeend = TimeSpan.FromHours(18);

	float midnightLightRotationOffset = -90.0f;
	public static TimeSpan lengthOfDayInRealTime
	{
		get => TimeSpan.FromSeconds(lengthOfDayInSeconds);
	}
	
	public Text TimeUpdateText;

	[SerializeField]
	public TimeSpan currentTime;
    // Start is called before the first frame update
    void Start()
    {
		instance = this;
		currentTime = TimeSpan.FromSeconds(0);
		Debug.Log(lengthOfDayInRealTime.TotalMilliseconds / TimeSpan.FromDays(1).TotalMilliseconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void FixedUpdate()
	{
		
		currentTime = TimeSpan.FromMilliseconds(currentTime.TotalMilliseconds + (RealTimeToGameTime(TimeSpan.FromSeconds(Time.fixedDeltaTime)).TotalMilliseconds));
	
		//Debug.Log(Time.fixedDeltaTime + " -> " + RealTimeToGameTime(TimeSpan.FromSeconds(Time.fixedDeltaTime)));
		//directionalSunLight.transform.rotation = Quaternion.Euler(new Vector3(midnightLightRotationOffset + (float)(currentTime.TotalMilliseconds) /(float)TimeSpan.FromDays(1).TotalMilliseconds * 360,0,0));
		
		TimeUpdateText.text = String.Format("{0:hh\\:mm}",TimeSpan.FromMilliseconds(currentTime.TotalMilliseconds % TimeSpan.FromDays(1).TotalMilliseconds));
	}


	public static TimeSpan RealTimeToGameTime(TimeSpan realTime)
	{
		return TimeSpan.FromMilliseconds(realTime.TotalMilliseconds/lengthOfDayInRealTime.TotalMilliseconds * TimeSpan.FromDays(1).TotalMilliseconds);
	}

	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class NightTimeLight : MonoBehaviour
{
    Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(TimeOfDay.Instance.currentTime.Hours > TimeOfDay.dayTimeStart.Hours && TimeOfDay.Instance.currentTime.Hours < TimeOfDay.dayTimeend.Hours)
        {
            //lights off
            myLight.enabled = false;
        }
        else
        {
            myLight.enabled = true;
        }
    }
}

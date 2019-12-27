
using UnityEngine;
using System;
public class CarController : MonoBehaviour
{
	//initial conditions
	[SerializeField]
	public double moneyOnHand;
	//may be 0 if not seeking spot, if more, will be 
	[SerializeField]
	public TimeSpan timeToSpendAtMeter;

	[SerializeField]
	public bool isMoving = true;

	float landSpeed = 2.0f;
	//if time to spend > money on hand, will put max in, and then keep car there for full time.


	public Transform roadEndTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void FixedUpdate()
	{
		if (isMoving && roadEndTarget != null)
		{
			transform.position += (roadEndTarget.position - transform.position).normalized * landSpeed * Time.fixedDeltaTime;
			transform.LookAt(roadEndTarget);
		}
	}
}

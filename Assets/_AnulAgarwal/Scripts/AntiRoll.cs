using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRoll : MonoBehaviour {

	[SerializeField]
	public WheelCollider WheelL;

	[SerializeField]
	public WheelCollider WheelR;

	[SerializeField]
	public float antiroll;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		WheelHit hit = new WheelHit ();
		float travelL = 1.0f;
		float travelR = 1.0f;
		bool groundedL = WheelL.GetGroundHit (out hit);
		if (groundedL)
			travelL = (-WheelL.transform.InverseTransformPoint (hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

		bool groundedR = WheelR.GetGroundHit (out hit);
		if (groundedR)
			travelR = (-WheelR.transform.InverseTransformPoint (hit.point).y - WheelR.radius) / WheelR.suspensionDistance;


		float antiRollForce = (travelL - travelR) * antiroll;

		if (groundedL)
			GetComponent<Rigidbody> ().AddForceAtPosition (WheelL.transform.up * -antiRollForce, WheelL.transform.position);
		
	

		if (groundedR)
			GetComponent<Rigidbody> ().AddForceAtPosition (WheelR.transform.up * -antiRollForce, WheelR.transform.position);	

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grease : MonoBehaviour {
	[SerializeField]
	public float steerReducePercentage;


	// Use this for initialization
	void Start () {
		
	}
	public void applyGrease(GameObject go){
		print (go.name);
		go.GetComponentInParent<VehicleBody> ().changeSteerAngle(steerReducePercentage);
		print ("applying some oilll.....");
	}

	public void removeGrease(GameObject go){

		go.GetComponentInParent<VehicleBody> ().GetComponentInChildren<VehicleBody> ().changeSteerAngle(0f);

	}
	// Update is called once per frame
	void Update () {
		
	}
}

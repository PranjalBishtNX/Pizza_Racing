using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBody : MonoBehaviour {

	[SerializeField]
	List<GameObject> attachmentPoints;

	[SerializeField]
	public int minLevel;
	[SerializeField]
	public int maxLevel;

	List<Wheel> wheels;
	List<PropulsionObject> objPower;

	[SerializeField]
	public int id;

	[SerializeField]
	public Rigidbody vehicleRb;

	[SerializeField]
//public	UnityStandardAssets.Vehicles.Car.CustomCarController cc;
	public	UnityStandardAssets.Vehicles.Car.CarController cca;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void callController(){

		cca.setInitialization ();

	}
	void OnTriggerEnter(Collider col){

		if (col.gameObject.layer == 8) {


			GetComponentInParent<Vehicle> ().doOnPepperoniEnter (col);

		}


	}
}

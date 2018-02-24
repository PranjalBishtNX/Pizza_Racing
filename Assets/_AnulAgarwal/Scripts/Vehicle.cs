using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

	[SerializeField]
	public VehicleBody vb;

	[SerializeField]
	public PropulsionObject po;

	[SerializeField]
	public List<Wheel> wheels;

	[SerializeField]
	public List<GameObject> spawnerList;

	[SerializeField]
	public GameManager gm;

	[SerializeField]
	public List<GameObject> attachmentPoints;

	[SerializeField]
	public List<Attacher> attachPoints;


	// Use this for initialization
	void Start () {
		
	}


	public void enableCarEditing(){

		foreach (GameObject go in attachmentPoints) {

			go.SetActive (true);
		}
	}

	public void disableCarEditing(){

		foreach (GameObject go in attachmentPoints) {

			go.SetActive (false);
		}
	}

	public void enableCarPower(){
		//enable rigidbody & gravity from all attached items
		//enable car movement
		//enable propulsion movement
		//Disable object rotation
		//Make camera follow car from a distance

		//Enable rigidbody
		//	playerVehicle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

		//Enable car movement
		GetComponent<Engine> ().enabled = true;

		//Enable engine power
		if(GetComponentInChildren<PropulsionObject>()!= null){
			GetComponentInChildren<PropulsionObject> ().enabled = true;
			GetComponentInChildren<PropulsionObject> ().GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			GetComponentInChildren<PropulsionObject> ().GetComponent<Rigidbody> ().useGravity = true;

		}
		//Disable object rotation
		GetComponent<MouseRotate> ().enabled = false;
		disableCarEditing ();
		//Change Cams
		//	editCam.enabled = false;
		//	mainCam.enabled = true;

	}

	public void disableCarPower(){

		//disable rigidbody & gravity from all applicable items
		//disable car movement 
		//disable propulsion movement
		//Enable object rotation


		//playerVehicle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<Engine> ().enabled = false;
		if(GetComponentInChildren<PropulsionObject>()!= null){
			GetComponentInChildren<PropulsionObject> ().enabled = false;

			GetComponentInChildren<PropulsionObject> ().GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			GetComponentInChildren<PropulsionObject> ().GetComponent<Rigidbody> ().useGravity = false;

		}

		GetComponent<MouseRotate> ().enabled = true;
		enableCarEditing ();

		//	editCam.enabled = true;
		//	mainCam.enabled = false;
	}
	// Update is called once per frame
	void Update () {
		
	}
}

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


	[SerializeField]
	public List<PropulsionObject> propObj;

	public int numberOfEngine;

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
		GetComponentInChildren<Rigidbody>().useGravity = true;
		transform.rotation = Quaternion.Euler (0, 90, 0);
		//Enable car movement
		GetComponent<Engine> ().enabled = true;
		//this is for power through wheels in case propulsion doesnt work
		GetComponent<Engine> ().enableCarToMove (numberOfEngine);


		//Enable engine power
	/*	if(GetComponentInChildren<PropulsionObject>()!= null){
			GetComponentInChildren<PropulsionObject> ().enabled = true;
			GetComponentInChildren<PropulsionObject> ().GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			GetComponentInChildren<PropulsionObject> ().GetComponent<Rigidbody> ().useGravity = true;

		}*/
		//Disable object rotation
		GetComponent<MouseRotate> ().enabled = false;
		disableCarEditing ();
//		gm.switchCam ();
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

		gm.switchCam ();
	}
	// Update is called once per frame
	void Update () {
		
	}
}

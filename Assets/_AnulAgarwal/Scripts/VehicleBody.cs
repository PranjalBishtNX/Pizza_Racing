using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBody : MonoBehaviour {


	[SerializeField]
	public int minLevel;
	[SerializeField]
	public int maxLevel;

	[SerializeField]
	List<Wheel> wheels;

	[SerializeField]
	List<PropulsionObject> objPower;

	[SerializeField]
	public int id;

	[SerializeField]
	public Rigidbody vehicleRb;

	[SerializeField]
//public	UnityStandardAssets.Vehicles.Car.CustomCarController cc;
	public	UnityStandardAssets.Vehicles.Car.CarController cca;

	public	UnityStandardAssets.Vehicles.Car.CarUserControl cuc;


	[SerializeField]
	public List<GameObject> attachP;

	private float speedChangeStartTime;
	private bool hasSpeedChanged;
	private float speedChangeDuration;

	[SerializeField]
	public float weight;

	[SerializeField]
	public Camera personalCam;
	// Use this for initialization
	void Start () {
		cuc = GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (hasSpeedChanged && speedChangeStartTime + speedChangeDuration < Time.time) {

			foreach (PropulsionObject po in GetComponentInParent<Vehicle>().propObj) {
				po.forceSpeed = po.forceSpeed / 4f;

			}
			hasSpeedChanged = false;

		}

	}

	public void updateAttachPoints(){
		GetComponentInParent<Vehicle> ().attachmentPoints.Clear ();
		foreach (GameObject go in attachP) {

			GetComponentInParent<Vehicle> ().attachmentPoints.Add (go);
		}
		print (attachP.Count);
	}


	public void changeSteerAngle(float reduceAngle){

		cca.steerReducer = reduceAngle;
	}
	public void changeCarSpeedForThisMuchTime(float speedChangePercentage, float duration){
		hasSpeedChanged = true;

		speedChangeStartTime = Time.time;
		//cca.speedReducer = speedChangePercentage;
		speedChangeDuration = duration;

		foreach (PropulsionObject po in GetComponentInParent<Vehicle>().propObj) {
			po.forceSpeed = po.forceSpeed * 4f;

		}
	}

	public void jumpCar(){

		vehicleRb.AddForce (Vector3.up * 15000f *2,ForceMode.Impulse);

	}
	public void callController(){

		cca.setInitialization ();

	}

	public void driftCar(){

		//Lock back wheel
		//Steer front wheel

	}
	public void enableCarPower(){
		//enable rigidbody & gravity from all attached items
		//enable car movement
		//enable propulsion movement
		//Disable object rotation
		//Make camera follow car from a distance

		//Enable rigidbody
		GetComponentInChildren<Rigidbody>().useGravity = true;
		transform.rotation = Quaternion.Euler (0, 0, 0);
		cca.enabled = true;
		cuc.enabled = true;
//		personalCam.enabled = true;
	
			
		//Disable object rotation
		PropulsionObject[] po = GetComponentsInChildren<PropulsionObject>();
		foreach (PropulsionObject pop in po) {

			pop.enabled = true;
		}
		GetComponent<MouseRotate> ().enabled = false;
	//	GetComponentInChildren<Camera> (true).enabled = true;
	//	disableCarEditing ();

	callController ();
		//		gm.switchCam ();
	}

	public void enableCarEditing(){

		foreach (GameObject go in GetComponentInParent<Vehicle>(). attachmentPoints) {

			go.SetActive (true);
		}
	}

	public void disableCarEditing(){

		foreach (GameObject go in GetComponentInParent<Vehicle>(). attachmentPoints) {

			go.SetActive (false);
		}
	}

	public void enableCarCam(){
//		Camera.main.enabled = false;
		personalCam.enabled = true;


	}
	public void disableCarPower(){

		//disable rigidbody & gravity from all applicable items
		//disable car movement 
		//disable propulsion movement
		//Enable object rotation

		//Camera.main.enabled = false;
		cca.enabled = false;
		cuc.enabled = false;
	//	personalCam.enabled = false;

		//playerVehicle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		GetComponent<Rigidbody> ().useGravity = false;
//		GetComponent<Engine> ().enabled = false;
		if(GetComponentInChildren<PropulsionObject>()!= null){
			GetComponentInChildren<PropulsionObject> ().enabled = false;

//			GetComponentInChildren<PropulsionObject> ().GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
//			GetComponentInChildren<PropulsionObject> ().GetComponent<Rigidbody> ().useGravity = false;

		}

		GetComponent<MouseRotate> ().enabled = true;
		//enableCarEditing ();

		//gm.switchCam ();
	}



	void OnTriggerEnter(Collider col){

		if (col.gameObject.layer == 8) {


			GetComponentInParent<Vehicle> ().doOnPepperoniEnter (col);

		}


	}
}

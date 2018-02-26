using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

	[SerializeField]
	public List<Wheel> wheelInfo;

	[SerializeField]
	public float maxSpeed;

	[SerializeField]
	public float maxSteering;

	public bool canMove;



	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {


	}

	public void removeWheel(Wheel whe){

		wheelInfo.Remove (whe);

		GetComponent<Dot_Truck_Controller> ().wheelInfo.Remove (whe);
	}
	void FixedUpdate(){
		if (canMove) {

			moveCar ();
		} else {


		}

	}
	public void enableCarToMove(int engineNo ){

		print (engineNo);
		canMove = true;

		maxSpeed = maxSpeed * engineNo;
		foreach (Wheel truck_Info in wheelInfo)
		{
			Dot_Truck dc = new Dot_Truck ();
			dc.wheel = truck_Info.wheel;
			dc.wheelMesh = truck_Info.wheelMesh;
			dc.steering = truck_Info.steering;
			dc.motor = truck_Info.motor;

			GetComponent<Dot_Truck_Controller> ().truck_Infos.Add (dc);

		}
		GetComponent<Dot_Truck_Controller> ().wheelInfo = wheelInfo;

		//GetComponent<Dot_Truck_Controller> ().maxMotorTorque = maxSpeed * engineNo;
	

	}

	public float maxMotorTorque;
	public float maxSteeringAngle;



	public void VisualizeWheel(Wheel wheelPair)
	{
		Quaternion rot;
		Vector3 pos;
		wheelPair.wheel.GetWorldPose ( out pos, out rot);
		wheelPair.wheelMesh.transform.position = pos;
		wheelPair.wheelMesh.transform.rotation = rot;
		/*wheelPair.rightWheel.GetWorldPose ( out pos, out rot);
		wheelPair.rightWheelMesh.transform.position = pos;
		wheelPair.rightWheelMesh.transform.rotation = rot;*/
	}
	public void moveCar(){





	}


}
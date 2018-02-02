using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

	[SerializeField]
	List<Wheel> wheelInfo;

	[SerializeField]
	public float maxSpeed;

	[SerializeField]
	public float maxSteering;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {


		//moveCar ();
	}

	public void VisualizeWheel(Wheel whee)
	{
		Quaternion rot;
		Vector3 pos;
		whee.wheel.GetWorldPose ( out pos, out rot);
		whee.wheelMesh.transform.position = pos;
		whee.wheelMesh.transform.rotation = rot;

	}

	void moveCar(){

		float speed =  maxSpeed * Input.GetAxis("Vertical");
		float steering = maxSteering * Input.GetAxis("Horizontal");
		float brakeTorque = Mathf.Abs(Input.GetAxis("Jump"));
		if (brakeTorque > 0.001) {
			brakeTorque = maxSpeed;
			speed = 0;
		} else {
			brakeTorque = 0;
		}

		foreach (Wheel whee in wheelInfo) {

			if (whee.steering == true) {
				//truck_Info.leftWheel.steerAngle = truck_Info.rightWheel.steerAngle = ((whee.reverseTurn)?-1:1)*steering;
				whee.wheel.steerAngle= ((whee.reverseTurn)?-1:1)*steering;

			}

			if (whee.motor == true)
			{
				whee.wheel.motorTorque = speed;
				//truck_Info.rightWheel.motorTorque = motor;
			}

			whee.wheel.brakeTorque = brakeTorque;

			//	whee.wheel.brakeTorque = brakeTorque;
			VisualizeWheel(whee);

		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel: MonoBehaviour {

	public WheelCollider wheel;
	public GameObject wheelMesh;
	public int level;
	public bool motor;
	public bool steering;
	public float steeringHelp;
	public bool reverseTurn; 
	public int id;


	public float speed;
	public float turningRadius;
}

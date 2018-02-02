using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wheel {

	public WheelCollider wheel;
	public GameObject wheelMesh;

	public bool motor;
	public bool steering;
	public bool reverseTurn; 
}

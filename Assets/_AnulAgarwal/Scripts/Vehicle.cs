using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

	[SerializeField]
	public VehicleBody vb;

	[SerializeField]
	public PropulsionObject po;

	[SerializeField]
	List<Wheel> wheels;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

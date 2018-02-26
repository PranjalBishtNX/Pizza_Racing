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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

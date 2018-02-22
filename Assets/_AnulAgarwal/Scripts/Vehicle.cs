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
	
	// Update is called once per frame
	void Update () {
		
	}
}

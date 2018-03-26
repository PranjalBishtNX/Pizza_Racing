using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {
	[SerializeField]
	public float floatStrength;
	// Use this for initialization
	void Start () {
		
	}
	public void flyBalloon(){
		print("fying");
		GetComponent<Rigidbody> ().AddForce (Vector3.up * floatStrength);

	}
	// Update is called once per frame
	void Update () {
		
	}
}

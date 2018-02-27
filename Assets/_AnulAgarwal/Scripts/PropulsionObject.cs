using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsionObject : MonoBehaviour {

	Rigidbody rb;

	[SerializeField]
	public float forceSpeed;


	[SerializeField]
	public int id;

	[SerializeField]
	public int level;

	Quaternion rot;

	[SerializeField]
	Transform pos;


	Vector3 sPos;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
	//	rot = transform.rotation;
	//	transform.localScale = new Vector3 (0.1f, 0.2f, 0.1f);
		sPos= transform.position;
		sPos.x -= 1f;
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<Rigidbody> ().AddForceAtPosition (transform.forward * forceSpeed ,pos.position);
		//transform.localRotation = rot;
		//rb.AddForce (transform.forward * forceSpeed *-1f,ForceMode.Force);
	}
}

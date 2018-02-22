using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBuilderSelectorBox : MonoBehaviour {
	[SerializeField]
	CarBuilder cb;



	public GameObject obj;
	// Use this for initialization
	void Start () {
		
	}
	public void enableCollider(){

		GetComponent<BoxCollider> ().enabled = true;
	}
	public void disableCollider(){

		GetComponent<BoxCollider> ().enabled = false;

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {

				if (hit.collider == GetComponent<Collider> ()) {
					cb.attachObject (obj);
					print ("aaa");

				}
			}
		}
	}

}

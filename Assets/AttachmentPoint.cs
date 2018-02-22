using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour {

	[SerializeField]
	public GameManager gm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {

				if (hit.collider == GetComponent<Collider> ()) {
					gm.displayBuildingOptions (this.gameObject);


				}
			}
		}
	}
	public void setGameManager(GameManager gma){

		gm = gma;
	}



}

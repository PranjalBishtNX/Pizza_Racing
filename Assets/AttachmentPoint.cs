using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour {

	[SerializeField]
	public GameManager gm;

	[SerializeField]
	public float id;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				print (hit.collider.tag);

				if (hit.collider==GetComponent<Collider>()) {
				//	print ("hitt");
					//BUGGGG
					gm.displayBuildingOptions (this.gameObject);

				}
			}
		}
	}
	public void setGameManager(GameManager gma){

		gm = gma;
	}



}

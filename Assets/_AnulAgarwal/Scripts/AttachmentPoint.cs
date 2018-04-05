using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour {

	[SerializeField]
	public GameManager gm;

	[SerializeField]
	public float id;

	[SerializeField]
	int spawnIndex;

	[SerializeField]
	public bool isRight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				print (hit.collider.gameObject.layer);

				if (hit.collider==GetComponent<Collider>()) {
					print ("hitt");
					//BUGGGG
					gm.displayBuildingOptions (this.gameObject,spawnIndex);

				}
			}
		}
	}
	public void setGameManager(GameManager gma){
		print ("wheeew");
		gm = gma;
	}



}

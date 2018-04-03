using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public enum Type {Balloon, Breakable, Grease

	};

	[SerializeField]
	Obstacle.Type type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	 

	void OnCollisionEnter(Collision col){
		if (col.gameObject.layer == 9) {

		
			if (type == Obstacle.Type.Balloon) {
		GetComponent<Balloon> ().flyBalloon ();
				//make this obstacle fly
			}
			if (type == Obstacle.Type.Breakable) {
				//Slow car
				//Destroy Object with Animation
				GetComponent<Breakable>().breakObject(col.gameObject);
				Destroy (gameObject);

			}



		}

	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 9) {


			if (type == Obstacle.Type.Grease) {
				//make drifting tough
				print ("Aaa");

				GetComponent<Grease>().applyGrease(col.gameObject);

			}
		}


	}
	void OnTriggerExit(Collider col){

		if (col.gameObject.layer == 9) {


		
			if (type == Obstacle.Type.Grease) {
				//make drifting tough
				GetComponent<Grease>().removeGrease(col.gameObject);

			}


		}
	}
}

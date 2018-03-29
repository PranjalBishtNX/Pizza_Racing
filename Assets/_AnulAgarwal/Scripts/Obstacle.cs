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
				col.gameObject.GetComponent<Vehicle> ().changeCarSpeedForThisMuchTime (30, 3);
				Destroy (gameObject);

			}
			if (type == Obstacle.Type.Grease) {
				//make drifting tough

			}


		}

	}
}

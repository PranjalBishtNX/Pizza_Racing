using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

	[SerializeField]
	float slowTime;

	[SerializeField]
	float slowPercentage;
	// Use this for initialization
	void Start () {
		
	}


	public void breakObject(GameObject go){
		//Destroy object after animation
		//Slow car on impact
		go.GetComponent<Vehicle>().changeCarSpeedForThisMuchTime(slowPercentage,slowTime);
		print ("slowinggg");
	}
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public void changeScene(string name){

		Application.LoadLevel (name);
	}
	// Update is called once per frame
	void Update () {
		
	}
}

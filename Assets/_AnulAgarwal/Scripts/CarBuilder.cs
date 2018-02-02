using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBuilder : MonoBehaviour {

	[SerializeField]
	GameObject carEditorPanel;

	[SerializeField]
	GameManager gm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

 	
	public void editCar(){

		carEditorPanel.SetActive (true);
		gm.togglePause ();
	}
}

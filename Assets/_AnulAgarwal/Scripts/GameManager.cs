using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	bool isGamePaused;
	[SerializeField]
	UICanvas uic;

	[SerializeField]
	List<GameObject> listOfPanels;
	// Use this for initialization
	void Start () {

		isGamePaused = false;
	}

	public void closeAllPopupsAndResume(){


		foreach (GameObject g in listOfPanels) {


			g.SetActive (false);
		}
		listOfPanels [0].SetActive (true);
		togglePause ();

	}
	public void closeAllAndOpenThis(GameObject goa){
		togglePause ();
		foreach (GameObject g in listOfPanels) {


			g.SetActive (false);
		}

		goa.SetActive (true);
	}
	public void changeScene(string scene){


		Time.timeScale = 1f;
	}



	public void togglePause(){

		if (isGamePaused) {

			isGamePaused = false;
			Time.timeScale = 1f;


		} else {

			isGamePaused = true;
			Time.timeScale = 0f;

		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}

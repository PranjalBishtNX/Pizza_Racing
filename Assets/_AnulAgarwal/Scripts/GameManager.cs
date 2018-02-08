using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class GameManager : MonoBehaviour {

	bool isGamePaused;
	[SerializeField]
	UICanvas uic;

	[SerializeField]
	List<GameObject> listOfPanels;

	[SerializeField]
	Vehicle playerVehicle;

	[SerializeField]
	CarBuilder cb;
	// Use this for initialization
	void Start () {

		isGamePaused = false;
	}
	private Save createSaveGameObject(){

		Save save = new Save ();
		save.vehicleChasis = playerVehicle.vb.id;
		save.vehiclePropulsion = playerVehicle.po.id;
		return save;
	}
	public void LoadGame(){


		if(File.Exists(Application.persistentDataPath+"/gamesave.save")){

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fil = File.Open (Application.persistentDataPath + "/gamesave.save", FileMode.Open);
			Save save = (Save)bf.Deserialize (fil);
			fil.Close ();
		
			cb.replaceParts (save.vehicleChasis, save.vehiclePropulsion);
			print ("Loaded: Game");

		}

	}
	public void SaveGame(){

		Save save = createSaveGameObject ();
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/gamesave.save");
		bf.Serialize (file, save);
		file.Close ();
		print ("Saved: Game");
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
		if (Input.GetKeyDown (KeyCode.Escape)) {

			SaveGame ();
		}
		if (Input.GetKeyDown (KeyCode.L)) {

			LoadGame ();
		}
	}
}

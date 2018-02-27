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

	[SerializeField]
	GameObject carBPanel;

	[SerializeField]
	Camera editCam;

	[SerializeField]
	Camera mainCam;

	[SerializeField]
	GameObject pausePanel;


	// Use this for initialization
	void Start () {
	AttachmentPoint[] ap = FindObjectsOfType<AttachmentPoint> ();
		foreach (AttachmentPoint pa in ap) {


			pa.setGameManager(this);
		}
		isGamePaused = false;
		//LoadGame ();
	}

	public void switchCam(){

		if (editCam.enabled) {
			editCam.gameObject.SetActive(false);
			mainCam.gameObject.SetActive(true);
		} else {
			editCam.gameObject.SetActive(true);
			mainCam.gameObject.SetActive(false);
		}
	}
	private Save createSaveGameObject(){

		Save save = new Save ();
		if(playerVehicle.vb!=null)
		save.vehicleChasis = playerVehicle.vb.id;
		if (playerVehicle.propObj.Count>0) {


		
		
			foreach (PropulsionObject po in playerVehicle.propObj) {

				//save.propO = playerVehicle.propObj;
				save.vehiclePropulsion =po.id;

				CarPart cpp = new CarPart ();


				cpp.objID = po.id;
				BuilderPointAttacher bpa= cb.pointList.Find (d => d.gameObj ==po.gameObject);

				cpp.attachID = bpa.attachmentPoint.GetComponent<AttachmentPoint> ().id;
				save.carParts.Add (cpp);
			}
		}
		if(playerVehicle.wheels.Count>0)
		foreach (Wheel wh in playerVehicle.wheels) {
			CarPart cp = new CarPart ();
	

				BuilderPointAttacher bpa= cb.pointList.Find (d => d.gameObj == wh.gameObject);
				cp.attachID = bpa.attachmentPoint.GetComponent<AttachmentPoint> ().id;

			cp.objID = wh.id;
		
			save.carParts.Add (cp);
		}
	


	//	save.vehicleWheels = playerVehicle.wheels;
		return save;
	}
	public void displayBuildingOptions(GameObject attachP, int spawnIndex){


		togglePause ();
		carBPanel.SetActive (true);
		cb.setCurrentAttachmentPoint (attachP);
		cb.displayPart (spawnIndex);
	}



	public void LoadGame(){


		if(File.Exists(Application.persistentDataPath+"/gamesave.save")){

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fil = File.Open (Application.persistentDataPath + "/gamesave.save", FileMode.Open);
			Save save = (Save)bf.Deserialize (fil);
			fil.Close ();
		
		//	cb.replaceParts (save.vehicleChasis, save.vehiclePropulsion);
			cb.loadCar(save.carParts, save.vehicleChasis, save.vehiclePropulsion,save.propO);
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
	public void enterEditMode(){
		
		playerVehicle.disableCarPower ();


	}
	public void enterPlayMode(){
		//playerVehicle.GetComponentInChildren<Rigidbody> ().useGravity = true;
		SaveGame ();

		Application.LoadLevel("Level");


	}
	public void changeScene(string scene){


		Time.timeScale = 1f;
	}

	public void togglePauseMenu(){

		if (isGamePaused) {

		
			pausePanel.SetActive (false);

		} else {

		
			pausePanel.SetActive (true);


		}

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

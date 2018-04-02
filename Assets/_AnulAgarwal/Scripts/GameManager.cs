using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

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

	[SerializeField]
	Slider powerSlider;

	[SerializeField]
	float maxPowerBarValue;

	[SerializeField]
	public bool canDoUltimate;


	public Vehicle.Character currentCharacter;


	// Use this for initialization
	void Start () {
	AttachmentPoint[] ap = FindObjectsOfType<AttachmentPoint> ();
		foreach (AttachmentPoint pa in ap) {


			pa.setGameManager(this);
		}
		isGamePaused = false;
		powerSlider.maxValue = maxPowerBarValue;
		powerSlider.value = 0f;
		//LoadGame ();
	}
	public void increasePowerBar(float value){

		powerSlider.value += value;
		checkForPower ();
	}
	void enableUltimate(){

		canDoUltimate = true;

	}
	void checkForPower(){
		if (powerSlider.value >= maxPowerBarValue) {

			enableUltimate ();
			powerSlider.value = 0f;

		}


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
				cpp.type = 2;

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
				cp.type = 1;

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


	public void LoadGame(int slotNumber){


		if(File.Exists(Application.persistentDataPath+"/gamesave" + slotNumber.ToString() + ".save")){

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fil = File.Open (Application.persistentDataPath + "/gamesave" + slotNumber.ToString() + ".save", FileMode.Open);
			Save save = (Save)bf.Deserialize (fil);
			fil.Close ();

			//	cb.replaceParts (save.vehicleChasis, save.vehiclePropulsion);
			cb.loadCar(save.carParts, save.vehicleChasis, save.vehiclePropulsion,save.propO);
			print ("Loaded: Game");

		}

	}

	public void SaveGame(){
		//Have to save current player character
		// & save level progression
		Save save = createSaveGameObject ();
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/gamesave.save");
		bf.Serialize (file, save);
		file.Close ();
		print ("Saved: Game");

	}


	public void SaveGame(int slotNumber){
		//Have to save current player character
		// & save level progression


		Save save = createSaveGameObject ();
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath +"/gamesave" + slotNumber.ToString() + ".save");
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

		Application.LoadLevel (scene);
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
	void resetPowerBar(){

		powerSlider.value = 0f;

	}

	public void doUltimate(){

		if (canDoUltimate) {
			if (currentCharacter == Vehicle.Character.Booster) {
				playerVehicle.doBoosterPizzaUltimate ();

			} else if (currentCharacter == Vehicle.Character.Heavy) {

				playerVehicle.doHeavyPizzaUltimate ();

			} else if (currentCharacter == Vehicle.Character.Hopper) {


				playerVehicle.doHopperPizzaUltimate ();
			}
			canDoUltimate = false;
			resetPowerBar ();
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
		if (Input.GetKeyDown (KeyCode.Space)) {

			doUltimate ();

		}
	}
}

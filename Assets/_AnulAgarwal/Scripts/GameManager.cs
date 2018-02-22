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

	// Use this for initialization
	void Start () {
	AttachmentPoint[] ap = FindObjectsOfType<AttachmentPoint> ();
		foreach (AttachmentPoint pa in ap) {


			pa.setGameManager(this);
		}
		isGamePaused = false;
		//LoadGame ();
	}
	private Save createSaveGameObject(){

		Save save = new Save ();
		save.vehicleChasis = playerVehicle.vb.id;
		save.vehiclePropulsion = playerVehicle.po.id;
		foreach (Wheel wh in playerVehicle.wheels) {
			CarPart cp = new CarPart ();
			cp.posX = wh.wheelMesh.transform.parent.localPosition.x;
			cp.posY = wh.wheelMesh.transform.parent.localPosition.y;
			cp.posZ = wh.wheelMesh.transform.parent.localPosition.z;

			print (cp.posX);
			print (cp.posY);
			print (cp.posZ);

			cp.rotX = wh.wheelMesh.transform.localRotation.x;
			cp.rotY = wh.wheelMesh.transform.localRotation.y;
			cp.rotZ = wh.wheelMesh.transform.localRotation.z;

	
			cp.objID = wh.id;
	
			save.carParts.Add (cp);
		}
		CarPart cpp = new CarPart ();
		cpp.posX = playerVehicle.po.transform.localPosition.x;
		cpp.posY = playerVehicle.po.transform.localPosition.y;
		cpp.posZ = playerVehicle.po.transform.localPosition.z;

		cpp.rotX = playerVehicle.po.transform.localRotation.x;
		cpp.rotY = playerVehicle.po.transform.localRotation.y;
		cpp.rotZ = playerVehicle.po.transform.localRotation.z;

		cpp.objID = playerVehicle.po.id;

		save.carParts.Add (cpp);


	//	save.vehicleWheels = playerVehicle.wheels;
		return save;
	}
	public void displayBuildingOptions(GameObject attachP){


		togglePause ();
		carBPanel.SetActive (true);
		cb.setCurrentAttachmentPoint (attachP);

	}



	public void LoadGame(){


		if(File.Exists(Application.persistentDataPath+"/gamesave.save")){

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fil = File.Open (Application.persistentDataPath + "/gamesave.save", FileMode.Open);
			Save save = (Save)bf.Deserialize (fil);
			fil.Close ();
		
		//	cb.replaceParts (save.vehicleChasis, save.vehiclePropulsion);
			cb.loadCar(save.carParts, save.vehicleChasis, save.vehiclePropulsion);
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

		//disable rigidbody & gravity from all applicable items
		//disable car movement 
		//disable propulsion movement
		//Enable object rotation


	}
	public void enterPlayMode(){

		//enable rigidbody & gravity from all attached items
		//enable car movement
		//enable propulsion movement
		//Disable object rotation
		//Make camera follow car from a distance

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

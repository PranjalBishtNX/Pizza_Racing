﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarBuilder : MonoBehaviour {

	[SerializeField]
	GameObject carEditorPanel;

	[SerializeField]
	GameManager gm;

	[SerializeField]
	GameListManager glm;

	[SerializeField]
	public Vehicle vehicle;


	public Vehicle tmpvehicle;
	public bool isInSequence;

	[SerializeField]
	public Button changeCar;


	[SerializeField]
	public Button changePropulsionB;


	[SerializeField]
	public Button changeWheel;

	bool canChangeWheel;

	bool canChanegPropulsion;

	VehicleBody tmp_vehicle;
//	[SerializeField]
//	VehicleBody orig_vehicle;
	public bool canEditVehicle;
	PropulsionObject tmp_propulsion;
	PropulsionObject orig_propulsion;

	int body_orig_id, propulsion_orig_id;
	[SerializeField]
	List<PropulsionObject> tmp_po = new List<PropulsionObject> ();

	[SerializeField]
	List<GameObject> tmp_wheel = new List<GameObject>();

	[SerializeField]
	List<GameObject> tmpSpawnedObj = new List<GameObject> ();

	[SerializeField]
	public GameObject currentAttachmentPoint;

	[SerializeField]
public	List<BuilderPointAttacher> pointList;

	public int typeNumber;
	// Use this for initialization
	void Start () {
		
		body_orig_id = glm.vehicleBody.FindIndex (d => d.id == vehicle.vb.id);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setCurrentAttachmentPoint(GameObject go){


		currentAttachmentPoint = go;
	}
	public void initiateChangeSequence(){

		isInSequence = true;
	
		initiateBodyChangeSequence ();
	}

	public void initiateBodyChangeSequence(){
		changePropulsionB.interactable = false;
		changeWheel.interactable = false;
		propulsion_orig_id= glm.propulsionObj.FindIndex (d => d.id == vehicle.po.id);


	}
	public void editCar(){

		carEditorPanel.SetActive (true);
		gm.togglePause ();
	}
	public void changeWheels(){


	
	}


	public void clearOldCar(){
		if(pointList.Count>0)
		foreach (BuilderPointAttacher bp in pointList) {
			
				Destroy (bp.gameObj);
		

		}
		pointList.Clear ();
	}
	public void attachObject(GameObject objec){
		//attach object from temporary spawned to the attachment point
		//attach and store this temporary for everytime the player changes attachment point
		//Destroy game object from tmpspanwed objects list and instantiate it near the car
		//If other object is selected revert the changes and spawn
		GameObject obj = Instantiate(objec, currentAttachmentPoint.transform.position,currentAttachmentPoint.transform.rotation);
		print (objec.transform.localRotation);
		tmpSpawnedObj.Remove (obj);
		obj.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody>().transform);
		List<BuilderPointAttacher> tmp_bp = new List<BuilderPointAttacher> ();
		foreach (BuilderPointAttacher bp in pointList) {
			if (bp.attachmentPoint == currentAttachmentPoint) {
				if (bp.gameObj.GetComponent<Wheel> () != null) {

					vehicle.wheels.Remove (bp.gameObj.GetComponent<Wheel> ());
				}
		//		bp.gameObj.SetActive (false);
				Destroy (bp.gameObj);
				//pointList.Remove (bp);
				tmp_bp.Add (bp);

			}

		}
		foreach (BuilderPointAttacher bpaa in tmp_bp) {
			pointList.Remove (bpaa);

		}

		BuilderPointAttacher bpa = new BuilderPointAttacher ();
		bpa.attachmentPoint = currentAttachmentPoint;
		bpa.gameObj = obj;
		pointList.Add (bpa);

		if (typeNumber == 1) {

			vehicle.po = obj.GetComponent<PropulsionObject> ();

		} else {
			
			vehicle.GetComponent<Engine> ().wheelInfo.Add (obj.GetComponent<Wheel>());
			vehicle.wheels.Add (obj.GetComponent<Wheel>());

		}

		//ADd to vehicle settings
		resetParts ();
		gm.closeAllPopupsAndResume();
		disableCol ();

	}

	public void saveChanges(){




	}
	public void resetParts(){

		foreach (GameObject go in tmpSpawnedObj) {

			Destroy (go);
		}
		tmpSpawnedObj.Clear ();

	}

	public void disableCol(){
	foreach (GameObject go in vehicle.spawnerList) {

		go.GetComponent<CarBuilderSelectorBox> ().disableCollider ();
	}
}


	public void displayPart(int value){
		resetParts ();
		updateListToChasis ();
		typeNumber = value;
		if (value == 1) {
			//Engine
			int i = 0;
	

			while (i < tmp_po.Count && i < vehicle.spawnerList.Count) {

				PropulsionObject propul = Instantiate (tmp_po [i], vehicle.spawnerList[i].transform.position, vehicle.spawnerList[i].transform.rotation) as PropulsionObject;
				propul.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				propul.transform.SetParent (vehicle.spawnerList [i].transform);
				vehicle.spawnerList [i].GetComponent<CarBuilderSelectorBox> ().obj = propul.gameObject;
				vehicle.spawnerList [i].GetComponent<CarBuilderSelectorBox> ().enableCollider ();
				tmpSpawnedObj.Add (propul.gameObject);
				i++;

			}

		} else if (value == 2) {
			//Wheels
			int i = 0;
			while (i < glm.wheel.Count && i < vehicle.spawnerList.Count) {

				GameObject wheel = Instantiate (glm.wheel [i], vehicle.spawnerList[i].transform.position, vehicle.spawnerList[i].transform.rotation) ;
				tmpSpawnedObj.Add (wheel);
				vehicle.spawnerList [i].GetComponent<CarBuilderSelectorBox> ().enableCollider ();

				wheel.transform.SetParent (vehicle.spawnerList [i].transform);
				vehicle.spawnerList [i].GetComponent<CarBuilderSelectorBox> ().obj = wheel.gameObject;

				i++;

			}

		}

	}


	public void changePropulsion(int change){

		//bool canExit;
		int currentIndex=tmp_po.FindIndex (d => d.id == vehicle.po.id);
		int maxRange =tmp_po.Count;



		int index =	toggleNumber (currentIndex,maxRange , change);
 		

		orig_propulsion = vehicle.po;
	
		PropulsionObject po = Instantiate (tmp_po [index ], vehicle.po.transform.position, vehicle.po.transform.rotation) as PropulsionObject;

		po.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody>().transform);

		Destroy (vehicle.po.gameObject);

		vehicle.po =po;
			//	canExit = true;

		
	}
	int toggleNumber(int curVal, int maxVal, int change){
		if (change > 0) {
			if (curVal + change >= maxVal) {

				curVal = 0;
			} else {

				curVal += change;
			}

		} else {
			if (curVal + change < 0) {

				curVal = maxVal-1;
			
			} else {
		
				curVal = curVal+ change;
			}
		}
	

		return curVal;
	}
	public void updateListToChasis(){

		//Propulsion
		tmp_po.Clear ();


		foreach (PropulsionObject po in glm.propulsionObj) {

			if (po.level <= vehicle.vb.maxLevel && po.level >= vehicle.vb.minLevel) {

				tmp_po.Add (po);
			}

		}

		//Wheel
		tmp_wheel.Clear();
		foreach (GameObject whee in glm.wheel) {
			
				tmp_wheel.Add (whee);


		}

	}

	public void saveBody(){
		updateListToChasis ();
		changePropulsionB.interactable = true;
		changeWheel.interactable = true;

		changePropulsion (1);

	}

	public void cancelChanges(){

		//Cancel Car
		Destroy(vehicle.vb.gameObject);

		VehicleBody go = Instantiate (glm.vehicleBody[body_orig_id], vehicle.vb.transform.position, vehicle.vb.transform.rotation) as VehicleBody;

		go.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody>().transform);
		vehicle.vb = go;



		//Cancel Propulsion Changes
		Destroy (vehicle.po.gameObject);

		PropulsionObject po = Instantiate (glm.propulsionObj[propulsion_orig_id],vehicle.po.transform.position, vehicle.po.transform.rotation) as PropulsionObject;
		po.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody>().transform);
		vehicle.po = orig_propulsion;

	}

//	[SerializeField]
//	List<CarPart> cpp;
	public void loadCar(List<CarPart> cp, int vehicleBody, int po){
		clearOldCar ();
//		cpp = cp;
		if (cp.Count > 0) {
			//Check if object is null or not
			if (vehicle.vb != null) {
				//	Destroy (vehicle.vb.gameObject);
			}
			//	vehicle.vb = Instantiate (glm.vehicleBody.Find (d => d.id == vehicleBody), glm.vehicleBody.Find (d => d.id == vehicleBody).transform.position,glm.vehicleBody.Find (d => d.id == vehicleBody).transform.rotation) as VehicleBody;
			//		vehicle.vb.transform.SetParent (vehicle.transform);

		
			if (vehicle.po != null) {
				Destroy (vehicle.po.gameObject);
			} else if(po!=0){
				GameObject pointAttached= vehicle.attachmentPoints.Find(d=> d.GetComponent<AttachmentPoint>().id==cp.Find (da => da.objID == po).attachID);

				GameObject go= Instantiate (glm.propulsionObj.Find (d => d.id == po).gameObject, pointAttached.transform.position, pointAttached.transform.rotation);
				vehicle.po = go.GetComponent<PropulsionObject> ();

				vehicle.po.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody> ().transform);
				vehicle.po.transform.localPosition = pointAttached.transform.position;
				BuilderPointAttacher bp = new BuilderPointAttacher ();
				bp.attachmentPoint = pointAttached;
				bp.gameObj = go;
				pointList.Add (bp);

			}
			foreach (CarPart cpa in cp) {
				foreach (GameObject go in glm.wheel) {
					if (go.GetComponent<Wheel> ().id == cpa.objID) {
						GameObject pointAttached= vehicle.attachmentPoints.Find(d=> d.GetComponent<AttachmentPoint>().id==cp.Find (da => da.objID == cpa.objID).attachID);

						vehicle.wheels.Add (go.GetComponent<Wheel> ());
						//need to check id and select object
						GameObject goa = Instantiate (go, pointAttached.transform.position, pointAttached.transform.rotation);
						goa.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody> ().transform);
						//not spawning on the desired position
					//	goa.transform.localPosition = pointAttached.transform.position;
						BuilderPointAttacher bp = new BuilderPointAttacher ();
						bp.attachmentPoint = pointAttached;
						bp.gameObj = goa;
						pointList.Add (bp);
					}

				}

	
		
				//	go.transform.localPosition = new Vector3 (cpa.posX, cpa.posY, cpa.posZ);

			}

		}

	}
	public void replaceParts(int vehicleBody, int po){
		Destroy (vehicle.vb.gameObject);

		vehicle.vb =Instantiate (glm.vehicleBody.Find(d=> d.id==vehicleBody), vehicle.vb.transform.position, vehicle.vb.transform.rotation) as VehicleBody;
		vehicle.vb.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody>().transform);

		Destroy (vehicle.po.gameObject);

		vehicle.po = Instantiate (glm.propulsionObj.Find(d=> d.id==po), vehicle.po.transform.position, vehicle.po.transform.rotation) as PropulsionObject;

		vehicle.po.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody>().transform);

	}

	public void replaceBody(int id){
		Destroy (vehicle.vb.gameObject);

		vehicle.vb =Instantiate (glm.vehicleBody.Find(d=> d.id==id), vehicle.vb.transform.position, vehicle.vb.transform.rotation) as VehicleBody;
		vehicle.vb.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody>().transform);

	}
	public void replaceProp(int id){
		Destroy (vehicle.po.gameObject);

		vehicle.po = Instantiate (glm.propulsionObj.Find(d=> d.id==id), vehicle.po.transform.position, vehicle.po.transform.rotation) as PropulsionObject;

		vehicle.po.transform.SetParent (vehicle.transform);

	}
	public void changeBody(int change){
		initiateBodyChangeSequence ();
//		orig_vehicle = vehicle.vb;

	
		int index=	toggleNumber(glm.vehicleBody.FindIndex (d => d.id == vehicle.vb.id),glm.vehicleBody.Count, change);


		VehicleBody go = Instantiate (glm.vehicleBody [index], vehicle.vb.transform.position, vehicle.vb.transform.rotation) as VehicleBody;
		go.transform.SetParent (vehicle.GetComponentInChildren<VehicleBody>().transform);
		//	vehicle.vb.gameObject = glm.vehicleBody [1].gameObject;

		Destroy(vehicle.vb.gameObject);

		vehicle.vb = go;

	}
}

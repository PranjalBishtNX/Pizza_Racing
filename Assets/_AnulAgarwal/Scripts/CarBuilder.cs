using System.Collections;
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
	[SerializeField]
	VehicleBody orig_vehicle;

	PropulsionObject tmp_propulsion;
	PropulsionObject orig_propulsion;

	int body_orig_id, propulsion_orig_id;
	[SerializeField]
	List<PropulsionObject> tmp_po = new List<PropulsionObject> ();
	// Use this for initialization
	void Start () {
		body_orig_id = glm.vehicleBody.FindIndex (d => d.id == vehicle.vb.id);

	}
	
	// Update is called once per frame
	void Update () {
		
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




	public void saveChanges(){




	}


	public void changePropulsion(int change){

		bool canExit;
		int currentIndex=tmp_po.FindIndex (d => d.id == vehicle.po.id);
		int maxRange =tmp_po.Count;



		int index =	toggleNumber (currentIndex,maxRange , change);
 		

		orig_propulsion = vehicle.po;
	
		PropulsionObject po = Instantiate (tmp_po [index ], vehicle.po.transform.position, vehicle.po.transform.rotation) as PropulsionObject;

		po.transform.SetParent (vehicle.transform);

		Destroy (vehicle.po.gameObject);

		vehicle.po =po;
				canExit = true;

		
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


	public void saveBody(){
		tmp_po.Clear ();
		changePropulsionB.interactable = true;
		changeWheel.interactable = true;
		foreach (PropulsionObject po in glm.propulsionObj) {

			if (po.level <= vehicle.vb.maxLevel && po.level >= vehicle.vb.minLevel) {

				tmp_po.Add (po);
			}

		}

		changePropulsion (1);

	}

	public void cancelChanges(){

		//Cancel Car
		Destroy(vehicle.vb.gameObject);

		VehicleBody go = Instantiate (glm.vehicleBody[body_orig_id], vehicle.vb.transform.position, vehicle.vb.transform.rotation) as VehicleBody;

		go.transform.SetParent (vehicle.transform);
		vehicle.vb = go;



		//Cancel Propulsion Changes
		Destroy (vehicle.po.gameObject);

		PropulsionObject po = Instantiate (glm.propulsionObj[propulsion_orig_id],vehicle.po.transform.position, vehicle.po.transform.rotation) as PropulsionObject;
		po.transform.SetParent (vehicle.transform);
		vehicle.po = orig_propulsion;

	}

	public void replaceParts(int vehicleBody, int po){
		Destroy (vehicle.vb.gameObject);

		vehicle.vb =Instantiate (glm.vehicleBody.Find(d=> d.id==vehicleBody), vehicle.vb.transform.position, vehicle.vb.transform.rotation) as VehicleBody;
		vehicle.vb.transform.SetParent (vehicle.transform);

		Destroy (vehicle.po.gameObject);

		vehicle.po = Instantiate (glm.propulsionObj.Find(d=> d.id==po), vehicle.po.transform.position, vehicle.po.transform.rotation) as PropulsionObject;

		vehicle.po.transform.SetParent (vehicle.transform);

	}

	public void replaceBody(int id){
		Destroy (vehicle.vb.gameObject);

		vehicle.vb =Instantiate (glm.vehicleBody.Find(d=> d.id==id), vehicle.vb.transform.position, vehicle.vb.transform.rotation) as VehicleBody;
		vehicle.vb.transform.SetParent (vehicle.transform);

	}
	public void replaceProp(int id){
		Destroy (vehicle.po.gameObject);

		vehicle.po = Instantiate (glm.propulsionObj.Find(d=> d.id==id), vehicle.po.transform.position, vehicle.po.transform.rotation) as PropulsionObject;

		vehicle.po.transform.SetParent (vehicle.transform);

	}
	public void changeBody(int change){
		initiateBodyChangeSequence ();
		orig_vehicle = vehicle.vb;

	
		int index=	toggleNumber(glm.vehicleBody.FindIndex (d => d.id == vehicle.vb.id),glm.vehicleBody.Count, change);


		VehicleBody go = Instantiate (glm.vehicleBody [index], vehicle.vb.transform.position, vehicle.vb.transform.rotation) as VehicleBody;
		go.transform.SetParent (vehicle.transform);
		//	vehicle.vb.gameObject = glm.vehicleBody [1].gameObject;

		Destroy(vehicle.vb.gameObject);

		vehicle.vb = go;

	}
}

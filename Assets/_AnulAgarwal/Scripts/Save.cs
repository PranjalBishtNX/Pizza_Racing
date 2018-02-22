using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Save {
	public int vehicleChasis;
	public List<Wheel> vehicleWheels;
	public int vehiclePropulsion;
	public List<CarPart> carParts = new List<CarPart> ();
//	public GameObject go;

}

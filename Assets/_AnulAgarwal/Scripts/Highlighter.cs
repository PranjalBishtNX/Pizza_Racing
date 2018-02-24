using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {
	[SerializeField]
	Material highlightMat;

	Material origMat;
	// Use this for initialization
	void Start () {
		origMat = GetComponent<MeshRenderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseEnter()
	{
		print ("enter");
		GetComponent<MeshRenderer>().material= highlightMat;
	}

	void OnMouseExit()
	{
		GetComponent<MeshRenderer>().material = origMat;
	}
}

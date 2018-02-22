using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseOver()
	{
		GetComponentInChildren<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
	}

	void OnMouseExit()
	{
		GetComponentInChildren<Renderer>().material.shader = Shader.Find("Diffuse");
	}
}

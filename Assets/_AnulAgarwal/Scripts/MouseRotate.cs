using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour {
	public float speed = 1f;
	public float sensX= 15f;
	public float sensY= 15f;

	[SerializeField]
	Transform cam;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float rotX = Input.GetAxis ("Mouse X") * sensX * 10;
		float rotY = Input.GetAxis ("Mouse Y") * sensY * -10;

		if (Input.GetMouseButton (0)) {
			
			transform.Rotate (cam.up, -Mathf.Deg2Rad * rotX, Space.World);
			transform.Rotate (cam.right, -Mathf.Deg2Rad * rotY, Space.World);	

		}
	}
}

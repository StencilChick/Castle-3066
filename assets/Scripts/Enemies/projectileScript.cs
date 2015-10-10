using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {

	public float speed = 1.0f;
	//public float maxDistance = 10.0f;
	//float d =0.0f;
	
	public bool mouseAim = false;
	public Vector3 aim; //aim is only used if MouseAim is false

	public Camera mainCamera;
	// Use this for initialization
	void Start () {
		//mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>(); //unecessary, maincamera inherited from spawner

		if (mouseAim) {//if mouseAim, replaces aim with a vector pointing to mouse
			Vector3 mousePt = Input.mousePosition;
			mousePt.z -= mainCamera.transform.position.z;
			Vector3 aimPt = mainCamera.ScreenToWorldPoint (mousePt);
			aim = aimPt - transform.position;
		} else {
			aim = Vector3.up;
		}

		//remove any z axis movement and normalize vector
		aim.z = 0;
		aim.Normalize ();//*/

		//Debug.Log ("Mouse Position: "+Input.mousePosition+"       transform: "+transform.position+"                 Aim: "+aim);
	}
	void Start(Vector3 aimVector){
		aim = aimVector;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (speed*aim);
		/*
		d += speed;
		if (d >= maxDistance) {
			Destroy(gameObject);
		}//*/

		//destroy object if it leaves camera view
		Vector3 p = mainCamera.WorldToViewportPoint (transform.position);
		if (p.x > 1 || p.x < 0 || p.y > 1 || p.y < 0) {
			Destroy(gameObject);
		}
	}
}

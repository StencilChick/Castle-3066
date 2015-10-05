using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {

	public float speed = 1.0f;
	public float maxDistance = 10.0f;
	float d =0.0f;

	public Vector3 aim;

	public Camera mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

		Vector3 mousePt = Input.mousePosition;
		mousePt.z -= mainCamera.transform.position.z;
		Vector3 aimPt = mainCamera.ScreenToWorldPoint (mousePt);
		//aim  = gameObject.transform.up;
		//Event e = Event.current;
		aim  = aimPt - transform.position;
		//aim = e.mousePosition;

		aim.z = 0;
		//aim.y *= -1;
		//aim.x -= 900;
		//aim.y -= 900;
		aim.Normalize ();//*/

		Debug.Log ("Mouse Position: "+Input.mousePosition+"       transform: "+transform.position+"                 Aim: "+aim);
	}
	void Start(Vector3 aimVector){
		aim = aimVector;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (speed*aim);
		d += speed;
		if (d >= maxDistance) {
			Destroy(gameObject);
		}
	}
}

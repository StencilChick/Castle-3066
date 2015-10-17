using UnityEngine;
using System.Collections;

public class MouseMover : MonoBehaviour {

	public Camera c;
	public float c_dist=10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 mousePt = c.ViewportToWorldPoint (Input.mousePosition);
		Vector3 mousePt = Input.mousePosition;
		mousePt.z = c_dist;
		//Vector3 mousePt = c.ScreenToWorldPoint (Input.mousePosition);
		mousePt = c.ScreenToWorldPoint (mousePt);
		//mousePt.z = 0;
		gameObject.transform.position = mousePt;
		//Debug.Log (Input.mousePosition);
		Debug.Log (mousePt);
	}
}

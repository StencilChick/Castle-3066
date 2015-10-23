using UnityEngine;
using UnityEngine;
using System.Collections;

public class MouseMover : MonoBehaviour {
	
	public Camera mainCamera;
	public float c_dist;
	public bool placeError = false;
	
	Color originalColor;
	Color c;

	GameManager gm;
	Builder builder;
	
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		c_dist = -mainCamera.transform.position.z;
		c = GetComponent<Renderer>().material.color;
		originalColor = c;
		gm=Object.FindObjectOfType<GameManager>();
		builder=Object.FindObjectOfType<Builder>();
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 mousePt = c.ViewportToWorldPoint (Input.mousePosition);
		Vector3 mousePt = Input.mousePosition;
		mousePt.z = c_dist;
		//Vector3 mousePt = c.ScreenToWorldPoint (Input.mousePosition);
		mousePt = mainCamera.ScreenToWorldPoint (mousePt);
		//mousePt.z = 0;
		//Debug.Log (Input.mousePosition);
		//Debug.Log (mousePt);
		
		Builder b = Object.FindObjectOfType<Builder> ();
		
		//only move object on grid
		if (mousePt.y > b.gridHeight-1) {
			mousePt.y = b.gridHeight-1;
		}
		if (mousePt.y < 0) {
			mousePt.y = 0;
		}
		
		if (mousePt.x > b.gridWidth/2 -1) {
			mousePt.x = b.gridWidth/2 -1;
		}
		if (mousePt.x < -b.gridWidth/2 +1) {
			mousePt.x = -b.gridWidth/2 +1;
		}
		
		mousePt.x = Mathf.Round(mousePt.x);
		mousePt.y = Mathf.Round(mousePt.y);
		gameObject.transform.position = mousePt;

		int x = (int)builder.GetGridX((int)mousePt.x);
		int y = (int)mousePt.y;

		//MouseMover mm = placer.GetComponent<MouseMover>();
		if(builder.GetFromGrid(x,y) != null){
			placeError = true;
		}
		else if(gm.GetMoney() < GetComponent<Block>().cost){
			placeError = true;
		}
		else if(y > 0 && builder.GetFromGrid(x,y-1) == null){
			placeError = true;
		}
		else{
			placeError = false;
		}
		
		if (placeError) {
			c.r = 1.0f;
			c.b = 0.1f;
			c.g = 0.1f;
		} else {
			c = originalColor;
		}
		GetComponent<Renderer>().material.SetColor("_Color",c);
	}
}

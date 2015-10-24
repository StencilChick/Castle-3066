using UnityEngine;
using UnityEngine;
using System.Collections;

public class MouseMover : MonoBehaviour {
	
	public Camera mainCamera;
	public float c_dist;
	public bool placeError = false;
	
	Color[] originalColors;
	Color c;

	Material[] mats;

	GameManager gm;
	Builder builder;
	
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		c_dist = -mainCamera.transform.position.z;
		builder=Object.FindObjectOfType<Builder>();
		gm=Object.FindObjectOfType<GameManager>();

		mats = GetComponent<Renderer>().materials;
		originalColors = new Color [mats.Length];
		for(int i = 0; i < mats.Length; i++) {
			originalColors[i] = mats[i].color;
		}
		/*
		originalColor = c;
		*/

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
		///*
		/// 
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
			foreach(Material m in mats){
				c = m.color;
				c.r = 1.0f;
				c.b = 0.1f;
				c.g = 0.1f;
				m.SetColor("_Color", c);
				//Debug.Log("PlaceError");
			}
		} else {
			for(int i = 0; i < mats.Length; i++) {
				mats[i].SetColor("_Color", originalColors[i]);
			}
		}
		GetComponent<Renderer> ().materials = mats;
		//GetComponent<Renderer>().material.SetColor("_Color",c);
		//*/
	}
}

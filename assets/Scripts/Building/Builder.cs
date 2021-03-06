﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Builder : MonoBehaviour {

	GameManager manager;

	GameObject[,] blocks; 

	GameObject placeholder;
	GameObject[] placeholders;
	int selectIndex = 0;
	GUIContent[] buttons;

	public int gridHeight = 4;
	public int gridWidth = 18;

	int selectWidth;
	int selectHeight;

	public float hudScale = 1;

	GameObject placer; //holds an instance of the placeholder at mouse location

	// Use this for initialization
	void Start () {
		manager = Object.FindObjectOfType<GameManager>();

		blocks = new GameObject[gridWidth,gridHeight]; 

		placeholders = Resources.LoadAll<GameObject>("Blocks");
		placeholder = placeholders[0];
		buttons = new GUIContent[placeholders.Length];
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i] = new GUIContent(placeholders[i].name + " $"+placeholders[i].GetComponent<Block>().cost);
		}
		selectWidth = (int)Mathf.Min(128*placeholders.Length*hudScale, Screen.width-125);
		selectHeight = (int)(Mathf.Ceil(placeholders.Length * hudScale / Mathf.Floor(Screen.width*1.0f/128/hudScale)) * 28*hudScale);


		GameObject cheese = (GameObject)Instantiate(Resources.Load<GameObject>("Cheese"));
		blocks [GetGridX(0), 0] = cheese; 
		cheese.transform.position = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			// place bricks
			Vector3 pos = GetMousePos();
			
			if (PosIsOnGrid (pos)) { //user is clicking within a valid space

				int x = GetGridX((int)pos.x);
				int y = (int)pos.y;

				if(blocks[x,y] == null){ //is position empty?

					if(pos.y == 0){ //building on ground?
						if (manager.SubtractMoney(placeholder.GetComponent<Block>().cost)) {
							SpawnBlock (pos);
						}
					}
					else if(blocks[x,y-1] != null){ //building on top of another block?
						if (manager.SubtractMoney(placeholder.GetComponent<Block>().cost)) {
							SpawnBlock (pos);
						}
					}
				}
			}
		} 
		if (Input.GetMouseButtonDown(1)) {
			// remove bricks
			Vector3 pos = GetMousePos();
			
			if (PosIsOnGrid (pos)) { //user is clicking within a valid space

				int x = GetGridX((int)pos.x);
				int y = (int)pos.y;

				if(blocks[x,y] != null){ //can't remove what isn't there
					//check if object is at max height or else dosen't have anything built ontop of it
					if(!( (pos.y != gridHeight-1) && (blocks[x,y+1] != null)) ){
						CheeseScript c = blocks[x,y].GetComponent(typeof(CheeseScript)) as CheeseScript;
						//prevent sale if the object is cheese
						if(c == null){
							manager.AddMoney(blocks[x,y].GetComponent<Block>().GetResaleValue());

							Destroy (blocks[x,y]);
							blocks[x,y] = null;
						}
					}
				}
			}
		}

		if (enabled) {
			if (placer == null) {
				placer = (GameObject)Instantiate (placeholder);
				placer.AddComponent<MouseMover> ();
				Destroy(placer.GetComponent("ShooterScript"));
				//Debug.Log("Update");
			}
		}
	}
	
	void OnGUI() {
		if (enabled) {
			if (GUI.Button(new Rect(Screen.width/2 - 218*hudScale/2, Screen.height/2 - 57*hudScale, 218*hudScale, 52*hudScale), "<b>Begin Level</b>")) {
				Destroy(placer);
				manager.BeginLevel();
			}

			int oldIndex = selectIndex;
			selectIndex = GUI.SelectionGrid(new Rect(5, 5, selectWidth, selectHeight), selectIndex, buttons, placeholders.Length);
			if (oldIndex != selectIndex) {
				Destroy(placer);
				placeholder = placeholders[selectIndex];
			}
			
			GUI.Box(new Rect(5, selectHeight + 15, selectWidth, 56), placeholder.GetComponent<Block>().description);
		}
	}
	
	
	Vector3 GetMousePos() { //returns the coordinate of the mouse cursor based on the main camera, rounded to the nearest integer
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
		pos.x = Mathf.Round(pos.x); //- gridWidth/2;
		pos.y = Mathf.Round(pos.y);
		
		return pos;
	}
	
	void SpawnBlock(Vector3 pos){ //spawns a block of type [placeholder], block is assigned to location pos and to corresponding grid index
		GameObject block = (GameObject)Instantiate(placeholder);
		block.transform.position = pos;
		
		blocks[GetGridX ((int)pos.x),(int)pos.y] = block;
	}
	
	public int GetGridX(int x){ //gets the x value for coordinate on grid
		//because arrays can not have negative index, the x value in the grid is a bit off from actual coordinate, y value is normal
		return x + gridWidth / 2;
	}

	bool PosIsOnGrid(Vector3 pos){ //returns true if the location is a space on the grid
		return (pos.y >= 0 && pos.y < gridHeight && Mathf.Abs (pos.x) < gridWidth / 2);
	}

	//returns the game object occupying location on grid
	public GameObject GetFromGrid(int x, int y){
		return blocks[x,y];
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Builder : MonoBehaviour {

	GameManager manager;

	GameObject[,] blocks; 

	GameObject placeholder;
	GameObject[] placeholders;
	int selectIndex = 0;
	GUIContent[] buttons;

	int gridHeight = 4;
	int gridWidth = 18;

	int selectWidth;
	int selectHeight;

	// Use this for initialization
	void Start () {
		manager = Object.FindObjectOfType<GameManager>();

		blocks = new GameObject[gridWidth,gridHeight]; 

		placeholders = Resources.LoadAll<GameObject>("Blocks");
		placeholder = placeholders[0];
		buttons = new GUIContent[placeholders.Length];
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i] = new GUIContent(placeholders[i].name);
		}
		selectWidth = Mathf.Min(128*placeholders.Length, Screen.width-80);
		selectHeight = Mathf.CeilToInt(placeholders.Length * 1.0f / Mathf.Floor(Screen.width*1.0f/128)) * 28;


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
					if (manager.SubtractMoney(placeholder.GetComponent<Block>().cost)) {
						if(pos.y == 0){ //building on ground?
							SpawnBlock (pos);
						}
						else if(blocks[x,y-1] != null){ //building on top of another block?
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
						manager.AddMoney(blocks[x,y].GetComponent<Block>().GetResaleValue());

						Destroy (blocks[x,y]);
						blocks[x,y] = null;
					}
				}
			}
		}
	}
	
	void OnGUI() {
		if (enabled) {
			if (GUI.Button(new Rect(Screen.width-138, Screen.height - 57, 128, 52), "<b>Begin Level</b>")) {
				manager.BeginLevel();
			}

			int oldIndex = selectIndex;
			selectIndex = GUI.SelectionGrid(new Rect(5, 5, 128*placeholders.Length, 28), selectIndex, buttons, placeholders.Length);
			if (oldIndex != selectIndex) {
				placeholder = placeholders[selectIndex];
			}
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


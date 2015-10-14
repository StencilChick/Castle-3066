using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Builder : MonoBehaviour {
	
	List<Vector3> takenSpaces;
	GameObject placeholder;
	
	GameObject[,] blocks; 
	
	int gridHeight = 4;
	int gridWidth = 18;
	
	// Use this for initialization
	void Start () {
		takenSpaces = new List<Vector3>();
		blocks = new GameObject[gridWidth,gridHeight]; 
		placeholder = Resources.Load<GameObject>("Block");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			// place bricks
			Vector3 pos = GetMousePos();
			
			if (pos.y >= 0 && pos.y < gridHeight && Mathf.Abs(pos.x) < gridWidth/2) {
				if(blocks[GetGridX((int)pos.x),(int)pos.y] == null){ //is position empty?
					if(pos.y == 0){ //building on ground?
						SpawnBlock (pos);
					}
					else if(blocks[GetGridX ((int)pos.x),(int)pos.y-1] != null){ //building on top of another block?
						SpawnBlock (pos);
					}
				}
				/*
				if (!takenSpaces.Contains(pos) && (takenSpaces.Contains(pos - Vector3.up) || pos.y == 0)) {
					GameObject block = (GameObject)Instantiate(placeholder);
					block.transform.position = pos;

					blocks[pos.x,pos.y] = block;
					takenSpaces.Add(pos);
				}
				*/
			}
		} 
		if (Input.GetMouseButtonDown(1)) {
			// remove bricks
			Vector3 pos = GetMousePos();
			
			if (pos.y >= 0 && pos.y < gridHeight && Mathf.Abs(pos.x) < gridWidth/2) { //user is clicking within a valid space
				GameObject block = blocks[(int)pos.x,(int)pos.y];
				if(block != null){ //can't remove what isn't there
					//check if object is at max height or else dosen't have anything built ontop of it
					if(!(pos.y != gridHeight-1 && blocks[(int)pos.x, (int)pos.y+1] != null) ){
						Destroy (block);
						blocks[(int)pos.x,(int)pos.y] = null;
					}
				}
			}
			/*
			if (takenSpaces.Contains(pos) && !takenSpaces.Contains(pos + Vector3.up)) {
				GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
				for (int i = 0; i < blocks.Length; i++) {
					if (blocks[i].transform.position == pos) {
						takenSpaces.Remove(blocks[i].transform.position);
						Destroy(blocks[i]);
						break;
					}
				}
			}
			*/
		}
		
		/*
		// for testing
		if (Input.GetKeyDown("space")) {
			GameObject bulletPlaceholder = Resources.Load<GameObject>("Bullet");

			GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
			for (int i = 0; i < blocks.Length; i++) {
				if (blocks[i].name == "Archer(Clone)") {
					GameObject bullet = (GameObject)Instantiate(bulletPlaceholder);
					bullet.transform.position = blocks[i].transform.position + 2*Vector3.up;
				}
			}
		}
		//*/
	}
	
	void OnGUI() {
		int selectionGridIndex = 0;
		string[] buttons = {"Block", "Mortar", "Crossbow"};
		selectionGridIndex = GUI.SelectionGrid (new Rect(5, 5, 128*3, 28),selectionGridIndex,buttons,3);
		/*
		if (GUI.Button(new Rect(5, 5, 128, 28), "Block")) {
			placeholder = Resources.Load<GameObject>("Block");
		}
		
		if (GUI.Button(new Rect(145, 5, 128, 28), "Mortar")) {
			placeholder = Resources.Load<GameObject>("Morter");
		}
		
		if (GUI.Button(new Rect(285, 5, 128, 28), "Crossbow")) {
			placeholder = Resources.Load<GameObject>("Crossbow");
		}*/
	}
	
	
	Vector3 GetMousePos() {
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
		pos.x = Mathf.Round(pos.x); //- gridWidth/2;
		pos.y = Mathf.Round(pos.y);
		
		return pos;
	}
	
	void SpawnBlock(Vector3 pos){
		GameObject block = (GameObject)Instantiate(placeholder);
		block.transform.position = pos;
		
		blocks[GetGridX ((int)pos.x),(int)pos.y] = block;
		takenSpaces.Add(pos);
	}
	
	int GetGridX(int x){
		return x + gridWidth / 2;
	}
}


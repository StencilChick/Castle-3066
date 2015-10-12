using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Builder : MonoBehaviour {
	
	List<Vector3> takenSpaces;
	GameObject placeholder;

	public bool enabled = true;

	// Use this for initialization
	void Start () {
		takenSpaces = new List<Vector3>();
		placeholder = Resources.Load<GameObject>("Block");
	}
	
	// Update is called once per frame
	void Update () {
		if (enabled) {
			if (Input.GetMouseButtonDown(0)) {
				// place bricks
				Vector3 pos = GetMousePos();

				if (pos.y >= 0 && pos.y < 4 && Mathf.Abs(pos.x) < 9) {
					if (!takenSpaces.Contains(pos) && (takenSpaces.Contains(pos - Vector3.up) || pos.y == 0)) {
						GameObject block = (GameObject)Instantiate(placeholder);
						block.transform.position = pos;

						takenSpaces.Add(pos);
					}
				}
			} 
			if (Input.GetMouseButtonDown(1)) {
				// remove bricks
				Vector3 pos = GetMousePos();

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
			}
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
		if (enabled) {
			if (GUI.Button(new Rect(Screen.width-138, 5, 128, 28), "Begin Level")) {
				manager.BeginLevel();
			}

			if (GUI.Button(new Rect(5, 5, 128, 28), "Block")) {
				placeholder = Resources.Load<GameObject>("Block");
			}

			if (GUI.Button(new Rect(145, 5, 128, 28), "Morter")) {
				placeholder = Resources.Load<GameObject>("Morter");
			}

			if (GUI.Button(new Rect(285, 5, 128, 28), "Crossbow")) {
				placeholder = Resources.Load<GameObject>("Crossbow");
			}
		}
	}


	Vector3 GetMousePos() {
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
		pos.x = Mathf.Round(pos.x);
		pos.y = Mathf.Round(pos.y);

		return pos;
	}
}

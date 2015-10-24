using UnityEngine;
using System.Collections;

public class Treasury : MonoBehaviour {
	GameManager manager;
	float multamount = .1f;

	// Use this for initialization
	void Start () {

		manager = Object.FindObjectOfType<GameManager>();
		manager.modifyMult(multamount);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
		manager = Object.FindObjectOfType<GameManager> ();
		manager.modifyMult (-1 * multamount);
	}
}

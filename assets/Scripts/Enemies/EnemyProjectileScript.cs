using UnityEngine;
using System.Collections;

public class EnemyProjectileScript : MonoBehaviour {

	private float fallingSpeed = 1.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * fallingSpeed  * Time.deltaTime);
	}
}

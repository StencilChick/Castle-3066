using UnityEngine;
using System.Collections;

public class MoveFoward : MonoBehaviour {

	public float speed = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, 0, speed * Time.deltaTime);

		// cull
		if (Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 10 || Mathf.Abs(transform.position.z) > 10) {
			Destroy(gameObject);
		}
	}
}

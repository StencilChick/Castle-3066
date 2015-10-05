using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public float rate = 3;
	float rateTick;

	GameObject placeHolder;

	int flip;

	// Use this for initialization
	void Start () {
		rateTick = rate;
		flip = 0;

		placeHolder = Resources.Load<GameObject>("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		rateTick -= Time.deltaTime;

		if (rateTick <= 0) {
			rateTick = rate;

			GameObject enemy = (GameObject)Instantiate(placeHolder);
			enemy.transform.position = new Vector3(9 * (-1 + 2*flip), 6 + flip, 0);
			enemy.transform.Rotate(0, 180 * (flip-1), 0);

			flip++;
			if (flip >= 2) flip = 0;
		}
	}
}

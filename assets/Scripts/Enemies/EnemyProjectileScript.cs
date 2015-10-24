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

		if (this.transform.position.y < -5)
			Destroy(this);
	}

	void OnTriggerEnter(Collider col){

		Debug.Log("test");

		if( col.gameObject.name == "Block(Clone)" || col.gameObject.name == "Cheese(Clone)" || col.gameObject.name == "Crossbow(Clone)" || col.gameObject.name == "Morter(Clone)" || col.gameObject.name == "Armory(Clone)")
		{
			Destroy(col.gameObject);
		}

		if( col.gameObject.name != "Enemy(Clone)" && col.gameObject.name != "base" && col.gameObject.name != "EnemyProjectile(Clone)")
		{
			Destroy(gameObject);
		}
	}
}

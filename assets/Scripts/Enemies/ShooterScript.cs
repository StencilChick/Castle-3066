using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour {

	public float fireRate = 3.0f;
	float timer = 0.0f;
	public Object projectile;
	public Vector3 spawnPt;

	public Vector3 aim;

	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		aim = gameObject.transform.up;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		Vector3 mousePt = Input.mousePosition;
		mousePt.z -= mainCamera.transform.position.z;

		transform.rotation  = Quaternion.LookRotation(mainCamera.ScreenToWorldPoint (mousePt) - transform.position);
		transform.Rotate (0, 90, 0, Space.World);
		if (timer >= fireRate) {
			timer -= fireRate;

			//Object proj = GameObject.Instantiate (projectile, Vector3.zero,Quaternion.identity);
			//proj.transform.position();
			//GameObject proj = (GameObject)Instantiate (projectile, transform.position+spawnPt,this.transform.rotation);
			GameObject proj = (GameObject)Instantiate (projectile, transform.position+spawnPt,Quaternion.identity);

			//projectileScript p = proj.getComponent("projectileScript");
			//proj.transform.rotation = aim;
		}
	}
}

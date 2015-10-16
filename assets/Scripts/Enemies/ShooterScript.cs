using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour {
	
	public bool controlFire = false; //can user control fire
	public float fireRate = 3.0f; //fire rate is only used if control fire is false
	public bool mouseAim = false; //aims at mouse
	float timer = 0.0f;
	public Object projectile;
	public Vector3 spawnPt;

	public float multiplier = 1; //attackspeed multiplier
	
	Vector3 aim;

	Camera mainCamera;
	//GameObject proj; //last spawned projectile
	//projectileScript pScript; //last spawned projectile's script

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		aim = gameObject.transform.up;
	}
	
	// Update is called once per frame
	void Update () {


		if (controlFire) { //fire on left-click
			//if (Input.GetKeyDown("space")) {
			if (Input.GetMouseButtonDown(0)) {
				if(timer == 0.0f){
					//proj = (GameObject)Instantiate (projectile, transform.position + spawnPt, Quaternion.identity);
					spawnProjectile ();
					timer = fireRate;
				}
			}
			//wait for [fireRate] seconds before can fire again
			if (timer > 0) {
				timer -= Time.deltaTime * multiplier;
			}
			if(timer < 0){
				timer = 0;
			}
		} else { //fire on interval
			timer += Time.deltaTime * multiplier;
			if (timer >= fireRate) {
				timer -= fireRate;
				
				//proj = (GameObject)Instantiate (projectile, transform.position + spawnPt, Quaternion.identity);
				spawnProjectile ();
			}
		}
		//pScript = proj.GetComponent<projectileScript> ();
		//pScript.mainCamera = mainCamera; //projectile inherits main camera reference

		if (mouseAim) { //aim at mouse if nessesary
			Vector3 mousePt = Input.mousePosition;
			mousePt.z -= mainCamera.transform.position.z;

			Vector3 mouseAngle = (mainCamera.ScreenToWorldPoint (mousePt) - transform.position);

			Quaternion rot = Quaternion.LookRotation(mouseAngle);
			//rot*= Quaternion.FromToRotation(Vector3.forward, Vector3.up);
			rot*= Quaternion.FromToRotation(Vector3.up, Vector3.forward);
			rot*= Quaternion.FromToRotation(Vector3.right, Vector3.forward);
			transform.rotation = rot;

			if(transform.forward.z < 0){
				transform.Rotate(0,180,0);
			}
			//Debug.Log(transform.forward);
			//Debug.Log(transform.up);
		}
	}

	//synconizes projectile properties with shooter's properties, namely Mouse aim and main camera
	private void spawnProjectile(){
		//GameObject proj = (GameObject)Instantiate (projectile, transform.position + spawnPt, Quaternion.identity);
		GameObject proj = (GameObject)Instantiate (projectile, transform.position + spawnPt, transform.rotation);
		//proj.transform.Rotate (-90, 90, 0);

		//GameObject proj = (GameObject)Instantiate (projectile, transform.position + spawnPt);
		projectileScript pScript = proj.GetComponent<projectileScript> ();
		pScript.mouseAim = mouseAim;
		if (mouseAim) {
			//proj.rotate(transform.rotation);
		}
		pScript.mainCamera = mainCamera;
	}
}

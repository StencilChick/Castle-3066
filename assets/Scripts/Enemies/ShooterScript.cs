using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour {
	
	public bool controlFire = false; //can user control fire
	public float fireRate = 3.0f; //fire rate is only used if control fire is false
	public bool mouseAim = false; //aims at mouse
	float timer = 0.0f;
	public Object projectile;
	public Vector3 spawnPt;
	
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


		if (controlFire) { //fire on space press
			if (Input.GetKeyDown("space")) {
				if(timer == 0.0f){
					//proj = (GameObject)Instantiate (projectile, transform.position + spawnPt, Quaternion.identity);
					spawnProjectile ();
					timer = fireRate;
				}
			}
			//wait for [fireRate] seconds before can fire again
			if (timer > 0) {
				timer -= Time.deltaTime;
			}
			if(timer < 0){
				timer = 0;
			}
		} else { //fire on interval
			timer += Time.deltaTime;
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
			
			transform.rotation = Quaternion.LookRotation (mainCamera.ScreenToWorldPoint (mousePt) - transform.position);
			transform.Rotate (0, 0, 0, Space.World);

			//pScript.mouseAim = mouseAim; // projectile is told to mouse aim as well
		}
	}

	//synconizes projectile properties with shooter's properties, namely Mouse aim and main camera
	private void spawnProjectile(){
		GameObject proj = (GameObject)Instantiate (projectile, transform.position + spawnPt, Quaternion.identity);
		//proj.transform.Rotate (-90, 90, 0);

		//GameObject proj = (GameObject)Instantiate (projectile, transform.position + spawnPt);
		projectileScript pScript = proj.GetComponent<projectileScript> ();
		pScript.mouseAim = mouseAim;
		pScript.mainCamera = mainCamera;
	}
}

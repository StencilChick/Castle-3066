using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour {
	
	public bool controlFire = false; //can user control fire
	public float fireRate = 3.0f; //fire rate is only used if control fire is false
	public bool mouseAim = false; //aims at mouse
	float timer = 0.0f;
	public Object projectile;
	public Vector3 spawnPt;

	int x;
	int y;

	public float multiplier = 1; //attackspeed multiplier
	
	Vector3 aim;

	Camera mainCamera;
	Builder builder;
	//GameObject proj; //last spawned projectile
	//projectileScript pScript; //last spawned projectile's script

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		aim = gameObject.transform.up;

		builder = GameObject.Find ("Builder").GetComponent<Builder>();

	}
	
	// Update is called once per frame
	void Update () {
		int x = builder.GetGridX((int)transform.position.x);
		int y = (int)transform.position.y;
		multiplier = 1.0f;
		if (x > 0) {
			GameObject block = builder.GetFromGrid(x-1,y);
			if(block!= null){
				ArmoryScript a = block.GetComponent(typeof(ArmoryScript)) as ArmoryScript;
				if(a!=null){
					//Debug.Log("Multiplied");
					multiplier *= a.multiplier;
				}
			}
		}
		if (x < builder.gridWidth-1) {
			GameObject block = builder.GetFromGrid(x+1,y);
			if(block!= null){
				ArmoryScript a = block.GetComponent(typeof(ArmoryScript)) as ArmoryScript;
				if(a!=null){
					//Debug.Log("Multiplied");
					multiplier *= a.multiplier;
				}
			}
		}

		if (y > 0) {
			GameObject block = builder.GetFromGrid(x,y-1);
			if(block!= null){
				ArmoryScript a = block.GetComponent(typeof(ArmoryScript)) as ArmoryScript;
				if(a!=null){
					//Debug.Log("Multiplied");
					multiplier *= a.multiplier;
				}
			}
		}
		if (y < builder.gridHeight-1) {
			GameObject block = builder.GetFromGrid(x,y+1);
			if(block!= null){
				ArmoryScript a = block.GetComponent(typeof(ArmoryScript)) as ArmoryScript;
				if(a!=null){
					//Debug.Log("Multiplied");
					multiplier *= a.multiplier;
				}
			}
		}

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

			float angle = Vector3.Angle( transform.up, mouseAngle); //determine angle between up vector and mouse
			float sign = Mathf.Sign(Vector3.Dot(transform.forward, Vector3.Cross(transform.up, mouseAngle))); //get sign for rotation
			angle *=sign; 

			transform.Rotate(0,0,angle); //rotate by angle around Z axis

		//	Debug.Log(transform.forward);
		//	Debug.Log(transform.up);
			//Debug.Log(transform.);
		/*
			Vector3 point =  (mainCamera.ScreenToWorldPoint (mousePt) - transform.position);
			//point = Quaternion.Euler(0,90,0)*point;
			//transform.rotation = Quaternion.LookRotation ((mainCamera.ScreenToWorldPoint (mousePt) - transform.position) );
			transform.rotation = Quaternion.LookRotation (point, Vector3(0,0,1));
			transform.Rotate (00, 90, 00, Space.World);
		*/
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

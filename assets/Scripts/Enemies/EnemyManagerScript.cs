using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManagerScript : MonoBehaviour {
	
	public GameObject enemy;				//A GameObject that holds the prefab of the enemy model and script
	public GameObject enemyProjectile;		//A GameObject that holds the prefab of the enemy projectile model and script

	private float enemyGap = 1.25f;			//The gap between each enemy
	
	private int enemyNumber = 0;			//The number of enemies currently on screen
	public int enemiesKilled = 0;			//The number of enemies killed so far

	public float fireChance;				//The chance that an enemy is going to attack during a specific frame
	public int firingEnemy;					//The randomly chosen enemy that will shoot

	GameManager manager;				// reference to the game manager

	bool levelActive = false;
	
	
	
	public List<GameObject> enemyList = new List<GameObject>();

	Vector3 startPos;
	
	// Use this for initialization
	void Start () {

		manager = Object.FindObjectOfType<GameManager>();
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (levelActive) {

			//Random chance to fire a projectile
			int test = (int)Random.Range (1, 1 / fireChance);
			if (test == 1) {
				firingEnemy = Random.Range (0, enemyList.Count + 1);
				
				while (enemyList[firingEnemy] == null && enemyList.Count+1 != enemiesKilled) {
					firingEnemy = Random.Range (0, enemyList.Count + 1);
				}
				
				GameObject enemyProjectileClone = (GameObject)Instantiate (enemyProjectile, enemyList [firingEnemy].transform.position, enemyList [firingEnemy].transform.rotation);
			}

			if (Object.FindObjectsOfType<EnemyScript>().Length == 0) {
				levelActive = false;
				manager.EndLevel();
			}
		}

	}
	
	public void enemyKilled() {
		Debug.Log ("enemy killed");
		enemiesKilled++;
		
		foreach (GameObject e in enemyList) {
			if (e != null)
			{
				e.GetComponent<EnemyScript>().moveFaster();
			}
		}
	}


	public void CreateEnemies() {
		transform.position = startPos;

		//Create 5 seperate rows of 11 enemies
		int count = 5;
		for (int j = 1; j <= count; j++)
		{
			int count2 = 11;
			for (int i = 1; i <= count2; i++)
			{
				GameObject enemyClone = (GameObject)Instantiate(enemy, this.transform.position, this.transform.rotation);
				enemyNumber++;
				enemyList.Add(enemyClone);
				transform.Translate(enemyGap, 0.0f, 0.0f);
			}
			transform.Translate(-enemyGap * count2, -1.4f, 0.0f);
		}

		levelActive = true;
	}

}

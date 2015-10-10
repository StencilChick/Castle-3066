using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManagerScript : MonoBehaviour {
	
	public GameObject enemy;			//A GameObject that holds the prefab of the enemy model and script
	
	private float enemyGap = 1.25f;		//The gap between each enemy
	
	private int enemyNumber = 0;		//The number of enemies currently on screen
	public int enemiesKilled = 0;		//The number of enemies killed so far
	
	
	
	public List<GameObject> enemyList = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		
		
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
	}
	
	// Update is called once per frame
	void Update () {
		
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
}

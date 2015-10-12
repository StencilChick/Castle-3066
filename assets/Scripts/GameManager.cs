using UnityEngine;
using System.Collections;

// thing that holds the global-ish variables and functions
public class GameManager : MonoBehaviour {

	Builder builder;
	EnemyManagerScript enemyManager;

	int money = 200;

	// Use this for initialization
	void Awake () {
		builder = Object.FindObjectsOfType<Builder>()[0];
		enemyManager = Object.FindObjectsOfType<EnemyManagerScript>()[0];
	}

	void OnGUI() {
		GUI.Label(new Rect(5, 38, 128, 28), "$" + money);
	}


	public void BeginLevel() {
		builder.enabled = false;
		enemyManager.CreateEnemies();
	}
	public void EndLevel() {
		builder.enabled = true;
	}


	public void AddMoney(int v) {
		money += v;
	}
	public bool SubtractMoney(int v) {
		if (money < v) return false;

		money -= v;
		return true;
	}

	public int GetMoney() {
		return money;
	}
}

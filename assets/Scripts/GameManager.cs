using UnityEngine;
using System.Collections;

// thing that holds the global-ish variables and functions
public class GameManager : MonoBehaviour {

	Builder builder;
	EnemyManagerScript enemyManager;
	float moneyMult = 1.0f;
	int money = 200;

	// Use this for initialization
	void Awake () {
		builder = Object.FindObjectsOfType<Builder>()[0];
		enemyManager = Object.FindObjectsOfType<EnemyManagerScript>()[0];
	}

	void OnGUI() {
		GUI.Label(new Rect(Screen.width - 125, 10, 125, 40), "<size=30>$" + money + "</size>");
	}


	public void BeginLevel() {
		builder.enabled = false;
		enemyManager.CreateEnemies();
	}
	public void EndLevel() {
		builder.enabled = true;
	}


	public void AddMoney(int v) {
		float imon = v * moneyMult;
		money += (int) Mathf.Ceil(imon);
	}
	public bool SubtractMoney(int v) {
		if (money < v) return false;

		money -= v;
		return true;
	}
	public void modifyMult(float m){
		moneyMult += m;
	}

	public int GetMoney() {
		return money;
	}
}

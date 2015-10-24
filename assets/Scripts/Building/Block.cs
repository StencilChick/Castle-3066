using UnityEngine;
using System.Collections;

// holds values for health and cost and whatnot
public class Block : MonoBehaviour {

	public int maxHealth = 1;
	int health;

	public int cost = 10;
	
	public string description = "A block";

	void Start() {
		health = maxHealth;
	}

	public int GetResaleValue() {
		return Mathf.FloorToInt(health * 1.0f / maxHealth) * cost;
	}
}

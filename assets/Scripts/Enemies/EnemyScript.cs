using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	
	public GameObject manager;			//A reference to the EnemyManager of this enemy
	
	public Vector3 startPos;			//The starting position of this enemy
	
	public float movementSpeed;			//Keeps track of how fast this enemy is currently moving
	public float fallingSpeed;			//Keeps track of how far down the enemy falls
	public float movementIncrease;		//Keeps track of how much faster the enemies become when one of them is killed
	public float fallIncrease;			//Keeps track of how much faster the enmies fall when one is killed
	
	private bool movingRight = true;	//Keeps track of the direction the enemy is moving on the x axis
	private bool isActive = true;		//Keeps track of whether or not this enemy is activated
	private bool isDead = false;		//Keep track of if this enemy has been killed yet

	public int moneyDrop = 15;
	
	
	// Use this for initialization
	void Start () {
		
		startPos = this.transform.position;
		manager = GameObject.Find ("enemyManager");
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//If this enemy has been activated, but not yet killed...
		if(isActive)
		{
			
			
			//If the enemy is currently moving to the right, move right
			if(movingRight)
			{
				this.MoveRight();
			}
			//Otherwise, move left
			else{
				this.MoveLeft();
			}
			
			//If the enemy is far enough away from its starting point, move it down and reverse direction
			if(this.transform.position.x - startPos.x > 4.0f)
			{
				this.MoveDown();
				movingRight = false;
			}
			
			//If the enemy is back at the start position, move it down and reverse direction
			if(this.transform.position.x <= startPos.x)
			{
				this.MoveDown();
				movingRight = true;
			}
			
		}
	}
	
	
	void MoveRight () {
		transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
	}
	
	void MoveLeft () {
		transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
	}
	
	void MoveDown () {
		transform.Translate(Vector3.down * fallingSpeed  * Time.deltaTime);
	}
	
	public void moveFaster (){
		movementSpeed += movementIncrease;
		fallingSpeed += fallIncrease;
	}
	
	//void OnCollisionEnter(Collision col){
	void OnTriggerEnter(Collider col){
		manager.GetComponent<EnemyManagerScript>().enemyKilled();
		Object.FindObjectsOfType<GameManager>()[0].AddMoney(moneyDrop);
		Debug.Log ("Trigger entered");
		//Destroy (gameObject);
		Destroy (col.gameObject);
		//Destroy(col.collider.gameObject);
		Destroy(gameObject);
	}
}
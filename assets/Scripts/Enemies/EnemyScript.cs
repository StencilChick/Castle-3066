using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	 
	public Vector3 startPos;			//The starting position of this enemy

	public float movementSpeed;			//Keeps track of how fast this enemy is currently moving
	private bool movingRight = true;	//Keeps track of the direction the enemy is moving on the x axis
	private bool isActive = true;		//Keeps track of whether or not this enemy is activated
	private bool isDead = false;		//Keep track of if this enemy has been killed yet


	// Use this for initialization
	void Start () {

		startPos = this.transform.position;
		movementSpeed = 1.0f;
		
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
		transform.Translate(Vector3.down * movementSpeed * 3.0f * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		Destroy (gameObject);
		Destroy (other.gameObject);
	}
}

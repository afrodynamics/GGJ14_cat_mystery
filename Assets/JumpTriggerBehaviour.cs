using UnityEngine;
using System.Collections;

public class JumpTriggerBehaviour : MonoBehaviour {
	
	public bool grounded = true;

	// If we have a collision with the terrain, set grounded to true
	
	void OnCollisionEnter ( Collision coll ) {
		grounded = true;
		Debug.Log("collision entered: ground = " + ( grounded ) );
		// NOTE: This will NOT work for generic GameObjects!
	}
	
	void OnCollisionExit ( Collision coll ) {
		grounded = false;
		Debug.Log("collision exited: ground = " +  ( grounded ));
	}

	public bool canJump() {
		return grounded;
	}
}

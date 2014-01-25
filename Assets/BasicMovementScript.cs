using UnityEngine;
using System.Collections;

public class BasicMovementScript : MonoBehaviour {

	public float moveSpeed = 3.0f;
	public float moveForceMultiplier = 9.0f;
	public float jumpForce = 300.0f;
	public GameObject myJumpTrigger;
	
	private JumpTriggerBehaviour jumpTrig;

	// Start
	void Start() {

		jumpTrig = myJumpTrigger.GetComponent<JumpTriggerBehaviour>();

	}

	// Jump
	void Jump() {

		Vector3 jumpVect = transform.TransformDirection ( rigidbody.transform.up ) * jumpForce;
		rigidbody.velocity = ( jumpVect ); // TODO: Somehow get rigidbody.AddForce() to work            

	}

	// FixedUpdate called once per physics engine update
	void FixedUpdate() {

		// Rotate camera

		if (Input.GetAxis ("Horizontal") != 0) {
			// Rotate around the z-axis to turn camera view
			rigidbody.transform.Rotate (new Vector3 (0, moveSpeed * Input.GetAxis ("Horizontal"), 0)); // lies
		}
			
		// Move forward/backward
		if (Input.GetAxis ("Vertical") > 0) {
		
			rigidbody.AddForce( new Vector3(moveForceMultiplier * rigidbody.transform.forward.x, 
		      	                           	moveForceMultiplier * rigidbody.transform.forward.y,
		      	                           	moveForceMultiplier * rigidbody.transform.forward.z));
		} 
		else if (Input.GetAxis ("Vertical") < 0) {

			rigidbody.AddForce( new Vector3 ( -moveForceMultiplier * rigidbody.transform.forward.x, 
		                                 	  -moveForceMultiplier * rigidbody.transform.forward.y,
		                                 	  -moveForceMultiplier * rigidbody.transform.forward.z));
		}

		// Jump

		if ( Input.GetKey(KeyCode.Space) && jumpTrig.canJump() ) {
			Jump();
		}

		// Not a problem now, but may be soon. Prevents us from infinitely accelerating past moveSpeed
		rigidbody.velocity = Vector3.ClampMagnitude( rigidbody.velocity, moveSpeed );
		myJumpTrigger.transform.Translate( rigidbody.velocity );  // TODO: trigger collider isn't moving with us

	}


}

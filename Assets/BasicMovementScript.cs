using Leap;
using UnityEngine;
using System.Collections;

public class BasicMovementScript : MonoBehaviour {
	
	public float moveSpeed = 3.0f;
	public float moveForceMultiplier = 9.0f;
	public float jumpForce = 300.0f;
	public GameObject myJumpTrigger;
	Controller m_leapController;

	private float rotation;		// Tracks if we should be turning left, right, or not turning.
	private float movement; 	// Tracks if we should be moving forward, backwards, or not moving.
	private float rotationMultiplier; // We have to reduce the positional value returned by Leap Motion that we use to rotate left and right.
	private JumpTriggerBehaviour jumpTrig;

	// Start
	void Start() {

		jumpTrig = myJumpTrigger.GetComponent<JumpTriggerBehaviour>();
		m_leapController = new Controller();
		
		rotation = 0;
		movement = 0;
		
		rotationMultiplier = 0.1f; // This value may need to be tweaked

	}

	// Jump
	void Jump() {

		Vector3 jumpVect = transform.TransformDirection ( rigidbody.transform.up ) * jumpForce;
		rigidbody.velocity = ( jumpVect ); // TODO: Somehow get rigidbody.AddForce() to work 
	}

	// Handles all Leap controls.
	void LeapControls()
	{
		/*Frame frame = m_leapController.Frame();

		// We only do stuff if both hands are in view.
		// No three handed players allowed.
		if (frame.Hands.Count == 2) {
			Hand leftHand = frame.Hands [0];
			Hand rightHand = frame.Hands [1];
			
			if (leftHand.PalmPosition.x > rightHand.PalmPosition.x) {
					leftHand = rightHand;
					rightHand = frame.Hands [0];
			}

			// Basic movement
			{ // removed if (grounded) -- not needed
				// We need the average value of the two hands' positions.
				rotation = ( leftHand.PalmPosition.ToUnityScaled().x + rightHand.PalmPosition.ToUnityScaled().x ) / 2.0f;
				movement = ( leftHand.PalmPosition.ToUnityScaled().z + rightHand.PalmPosition.ToUnityScaled().z ) / 2.0f;

				if ( Mathf.Abs(rotation) > 0.25f ) {
					// Rotate around the z-axis to turn camera view
					rigidbody.transform.Rotate (new Vector3 (0, moveSpeed * rotation * rotationMultiplier, 0)); // lies
					
				}
				
				if (movement > -2.0  && movement < 0.0) {
					rigidbody.velocity = new Vector3 (moveSpeed * rigidbody.transform.forward.x, 
					                                  moveSpeed * rigidbody.transform.forward.y,
					                                  moveSpeed * rigidbody.transform.forward.z);
				} 
				else if (movement < -3.0) {
					rigidbody.velocity = new Vector3 (-moveSpeed * rigidbody.transform.forward.x, 
					                                  -moveSpeed * rigidbody.transform.forward.y,
					                                  -moveSpeed * rigidbody.transform.forward.z);
				}	
			}
		}*/
	}

	// FixedUpdate called once per physics engine update
	void FixedUpdate() {

		// Rotate camera

		LeapControls();

		// Keyboard controls.

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

using Leap;
using UnityEngine;
using System.Collections;

public class BasicMovementScript : MonoBehaviour {

	Controller m_leapController;

	public float moveSpeed;
	public float jumpForce;

	private bool grounded = true;
	private bool falling = false;

	private float rotation;		// Tracks if we should be turning left, right, or not turning.
	private float movement; 	// Tracks if we should be moving forward, backwards, or not moving.

	private float rotationMultiplier; // We have to reduce the positional value returned by Leap Motion that we use to rotate left and right.

	// Use this for initialization
	void Start () {
		m_leapController = new Controller();

		moveSpeed = 3.0f;
		jumpForce = 1;

		rotation = 0;
		movement = 0;

		rotationMultiplier = 0.1f; // This value may need to be tweaked.

	}

	// Handles all Leap controls.
	void LeapControls()
	{
		Frame frame = m_leapController.Frame();

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
			if (grounded) {
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

			
		}


	}




	// FixedUpdate called once per physics engine update
	void FixedUpdate() {

		LeapControls();

		// Keyboard controls.
		if (grounded) {
			if (Input.GetAxis ("Horizontal") != 0.0) {
				// Rotate around the z-axis to turn camera view
				rigidbody.transform.Rotate (new Vector3 (0, moveSpeed * Input.GetAxis ("Horizontal"), 0)); // lies

			}

			if (Input.GetAxis ("Vertical") > 0.0) {
				rigidbody.velocity = new Vector3 (moveSpeed * rigidbody.transform.forward.x, 
			                                  moveSpeed * rigidbody.transform.forward.y,
			                                  moveSpeed * rigidbody.transform.forward.z);
			} 
			else if (Input.GetAxis ("Vertical") < 0.0) {
				rigidbody.velocity = new Vector3 (-moveSpeed * rigidbody.transform.forward.x, 
			                                  -moveSpeed * rigidbody.transform.forward.y,
			                                  -moveSpeed * rigidbody.transform.forward.z);
			}
				
		}

		if (Input.GetKey (KeyCode.Space) && grounded) {
			Jump();
		}

		if (rigidbody.velocity.y < 0) {
			falling = true;
		}

		if(falling == true && rigidbody.velocity.y == 0)
		{
			grounded = true;
			falling = false;
		}

		// Not a problem now, but may be soon. Prevents us from infinitely accelerating past moveSpeed
		rigidbody.velocity = Vector3.ClampMagnitude( rigidbody.velocity, moveSpeed );

	}

	void Jump(){
		grounded = false;

		rigidbody.velocity = new Vector3 (jumpForce * rigidbody.transform.up.x, 
		                                  jumpForce * rigidbody.transform.up.y, 
		                                  jumpForce * rigidbody.transform.up.z);




	


	}
}

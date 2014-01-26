using Leap;
using UnityEngine;
using System.Collections;

public class BasicMovementScript : MonoBehaviour {

	Controller m_leapController;

	public float moveSpeed;
	public float jumpForce;
	public float maxSpeed;

	private bool grounded = true;
	private bool falling = false;

	private float rotation;		// Tracks if we should be turning left, right, or not turning.
	private float movement; 	// Tracks if we should be moving forward, backwards, or not moving.

	private float rotationMultiplier; // We have to reduce the positional value returned by Leap Motion that we use to rotate left and right.

	private float lastPalmY; // Tracks what the previous Y position of the hands was.
	private bool performingJumpMotion; // Tracks if the player is in the middle of performing a jump gesture.
	private float jumpUpDistance; // Tracks how far you have we have made upward movements for the jump gesture.
	private float jumpDownDistance; // Tracks how far you have made downwards movements the jump gesture.
	private float jumpMotionDistance; // How far the user should have to move their hands to make the jump gesture.

	// Use this for initialization
	void Start () {
		m_leapController = new Controller();

		moveSpeed = 4.0f;
		jumpForce = 10;
		maxSpeed = 4.0f;

		rotation = 0;
		movement = 0;

		rotationMultiplier = 0.1f; // This value may need to be tweaked.

		performingJumpMotion = false;
		jumpUpDistance = 0.0f;
		jumpDownDistance = 0.0f;
		jumpMotionDistance = 1.0f; // This value will probably need tweaking.

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
				float currentPalmY = ( leftHand.PalmPosition.ToUnityScaled().y + rightHand.PalmPosition.ToUnityScaled().y ) / 2.0f;

				if ( Mathf.Abs(rotation) > 0.25f ) {
					// Rotate around the z-axis to turn camera view
					transform.Rotate(0, moveSpeed * rotation * rotationMultiplier, 0);
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
				/*
				if (axes == RotationAxes.MouseXAndY)
				{
					float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
					
					//rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
					//rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
					
					transform.localEulerAngles = new Vector3(0, rotationX, 0);
				}
				else if (axes == RotationAxes.MouseX)
				{
					transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
				}
				else
				{
					//rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
					//rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
					
					transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
				}
				*/


				// Look for the jump gesture.  Functionify it?
				// If the user's hands have moved up, flag it with performingJumpMotion and record how far up they have moved.
				if ( currentPalmY > lastPalmY )
				{
					if (!performingJumpMotion)
					{
						performingJumpMotion = true;
					}
					jumpUpDistance += currentPalmY - lastPalmY;

				}
				// If the user's hands have moved down and they performed the prerequisite upward portion of the jump movement
				// then record how far down they have moved or jump if they have moved down far enough.
				else if (currentPalmY < lastPalmY && performingJumpMotion && jumpUpDistance >= jumpMotionDistance)
				{
					//Perform jump action
					if ( jumpDownDistance >= jumpMotionDistance ) {
						performingJumpMotion = false;
						Jump();
					}
					else {
						jumpDownDistance += lastPalmY - currentPalmY;
					}
				}
				// If they aren't moving up, or they are moving down without having moved up enough then reset all of the tracking information.
				else {
					performingJumpMotion = false;
					jumpUpDistance = 0.0f;
					jumpDownDistance = 0.0f;
				}

				lastPalmY = currentPalmY;
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
						rigidbody.velocity += new Vector3 (moveSpeed * rigidbody.transform.forward.x, 
	                                  0,
	                                  moveSpeed * rigidbody.transform.forward.z);
				} else if (Input.GetAxis ("Vertical") < 0.0) {
						rigidbody.velocity += new Vector3 (-moveSpeed * rigidbody.transform.forward.x, 
	                                  0,
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
		rigidbody.velocity = Vector3.ClampMagnitude( rigidbody.velocity, maxSpeed );

	}

	void Jump(){
		grounded = false;

		rigidbody.velocity += new Vector3 (0, jumpForce, 0);




	


	}
}

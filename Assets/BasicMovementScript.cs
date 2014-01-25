using UnityEngine;
using System.Collections;

public class BasicMovementScript : MonoBehaviour {

	public float moveSpeed = 3.0f;
	public float jumpForce = 1;

	private bool grounded = true;
	private bool falling = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

	// FixedUpdate called once per physics engine update
	void FixedUpdate() {

		// Rotate camera
		if (grounded) {
			if (Input.GetAxis ("Horizontal") != 0) {
				// Rotate around the z-axis to turn camera view
				rigidbody.transform.Rotate (new Vector3 (0, moveSpeed * Input.GetAxis ("Horizontal"), 0)); // lies

			}

			if (Input.GetAxis ("Vertical") > 0) {
				rigidbody.velocity = new Vector3 (moveSpeed * rigidbody.transform.forward.x, 
			                                  moveSpeed * rigidbody.transform.forward.y,
			                                  moveSpeed * rigidbody.transform.forward.z);
			} 
			else if (Input.GetAxis ("Vertical") < 0) {
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

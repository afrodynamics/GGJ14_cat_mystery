using Leap;
using UnityEngine;
using System.Collections;

public class PawBehavior : MonoBehaviour {
	Controller m_leapController;

	public float pawSpeed = 100;
	private bool showPaw;

	// Use this for initialization
	void Start () {
		m_leapController = new Controller();

		gameObject.renderer.material.color = Color.black;
		gameObject.renderer.enabled = false;
		rigidbody.collider.enabled = false;

		showPaw = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LeapControls()
	{
		Hand leftHand, rightHand;
		rightHand = null;
		Frame frame = m_leapController.Frame();
		bool noHand = false;
		if (frame.Hands.Count == 2) {
			leftHand = frame.Hands [0];
			rightHand = frame.Hands [1];

			if (leftHand.PalmPosition.x > rightHand.PalmPosition.x) {
				leftHand = rightHand;
				rightHand = frame.Hands [0];
			}
		} 
		else if (frame.Hands.Count == 1) {
			rightHand = frame.Hands[0];
		}
		else {
			noHand = true;
		}

		if (!noHand && rightHand.PalmPosition.z <= -1)
		{
			showPaw = true;
		}
		else {
			showPaw = false;
		}

	}


	void FixedUpdate()
	{
		LeapControls ();
					
		//transform.position = Camera.main.transform.TransformPosition (Vector3.forward);
		if (Input.GetKey (KeyCode.LeftShift)) {
			showPaw = true;
		}

		if (showPaw) {
			gameObject.renderer.enabled = true;
			rigidbody.renderer.enabled = true;
			rigidbody.collider.enabled = true;
			// Rotate around the z-axis to turn camera view

			/*rigidbody.velocity += new Vector3(pawSpeed * rigidbody.transform.forward.x,
			                       pawSpeed * rigidbody.transform.forward.y,
			                       pawSpeed * rigidbody.transform.forward.z);
			*/
		}
		else
		{
			gameObject.renderer.enabled = false;
			rigidbody.renderer.enabled = false;
			rigidbody.collider.enabled = false;
		}

	}
}

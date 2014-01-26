using Leap;
using UnityEngine;
using System.Collections;

public class PawBehavior : MonoBehaviour {
	Controller m_leapController;

	public float pawSpeed = 100;
	private bool showPaw;

	public float localx, localy, localz;
	public float handx, handy, handz;

	private const float xOffset = 0.5f;
	private const float yOffset = -3.0f;
	private const float zOffset = 1.0f;


	// Use this for initialization
	void Start () {
		m_leapController = new Controller();

		gameObject.renderer.material.color = Color.black;
		gameObject.renderer.enabled = false;
		rigidbody.collider.enabled = false;

		localx = gameObject.transform.localPosition.x;
		localy = gameObject.transform.localPosition.y;
		localz = gameObject.transform.localPosition.z;



		showPaw = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LeapControls()
	{
		Hand hand = null;
		Frame frame = m_leapController.Frame();
		bool noHand = false;
		// Only allow motion controls with one hand.
		if (frame.Hands.Count == 1) {
			hand = frame.Hands [0];
		} 
		else {
			noHand = true;
		}

		if (!noHand && hand.Fingers.Count <= 2 && hand.Fingers.Count > 0)
		{
			showPaw = true;

			localx = hand.PalmPosition.ToUnityScaled().x + xOffset;
			localy = hand.PalmPosition.ToUnityScaled().y + yOffset;
			localz = hand.PalmPosition.ToUnityScaled().z + zOffset;

			if (localy > -0.35f) {
				localy = -0.35f;
			}
			if (localz > 0.8f) {
				localz = 0.8f;
			}

			gameObject.transform.localPosition = new Vector3 (localx, localy, localz);


			handx = hand.PalmPosition.ToUnityScaled().x;
			handy = hand.PalmPosition.ToUnityScaled().y;
			handz = hand.PalmPosition.ToUnityScaled().z;
		}
		else {
			showPaw = false;
		}

	}

	void OnCollisionEnter(Collision collision)	{
		//if (collision.gameObject.name == "Ball") {
			collision.gameObject.rigidbody.velocity += new Vector3(collision.gameObject.rigidbody.velocity.x * 50,
			                                                       collision.gameObject.rigidbody.velocity.y * 50,
			                                                       collision.gameObject.rigidbody.velocity.z * 50);
		//}
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

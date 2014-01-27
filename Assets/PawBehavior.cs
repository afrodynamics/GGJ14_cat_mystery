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

	GameObject pawEnd;

	// Use this for initialization
	void Start () {
		m_leapController = new Controller();

		gameObject.renderer.material.color = Color.black;
		gameObject.renderer.enabled = false;
		rigidbody.collider.enabled = false;

		localx = gameObject.transform.localPosition.x;
		localy = gameObject.transform.localPosition.y;
		localz = gameObject.transform.localPosition.z;


		pawEnd = GameObject.Find("PawEnd");
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

			if (localy > 0.6f) {
				localy = 0.6f;
			}
			if (localy < -0.1f) {
				localy = -0.1f;
			}
			if (localz > 0.6f) {
				localz = 0.6f;
			}

			if (localx < 0.4f) {
				localx = 0.4f;
			}


			// I'm trying to get it so that when you move your hand forward, the paw moves forward reletive to itself.
			// This may or may not go horribly wrong.
			// It should work because of math.
		/* Yeah, this didn't work.  Maybe it can be fixed someday, but ignore it for now.
		 * It stops rotating relative to the camera, so when you turn it doesn't turn with you.
			Vector3 newPosition = new Vector3( gameObject.transform.forward.x * localz,
			                                 gameObject.transform.forward.y * localz,
			                                 gameObject.transform.forward.z * localz );

			newPosition += new Vector3( gameObject.transform.right.x * localx,
			                          gameObject.transform.right.y * localx,
			                          gameObject.transform.right.z * localx );

			newPosition += new Vector3( gameObject.transform.up.x * localy, 
			                          gameObject.transform.up.y * localy, 
			                          gameObject.transform.up.z * localy );
			                         
			gameObject.transform.localPosition = newPosition;
		*/
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
			collision.gameObject.rigidbody.velocity += new Vector3(collision.gameObject.rigidbody.velocity.x * 10,
			                                                       collision.gameObject.rigidbody.velocity.y * 10,
			                                                       collision.gameObject.rigidbody.velocity.z * 10);
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
			pawEnd.renderer.enabled = true;
			pawEnd.collider.enabled = true;

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
			pawEnd.renderer.enabled = false;
			pawEnd.collider.enabled = false;
		}

	}
}

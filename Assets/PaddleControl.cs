using Leap;
using UnityEngine;
using System.Collections;

public class PaddleControl : MonoBehaviour {
	Controller m_leapController;

	// Use this for initialization
	void Start () {
		m_leapController = new Controller();

	}

	void LeapControls() {
		Frame frame = m_leapController.Frame();

		if (frame.Hands.Count == 1) {

			Hand hand = frame.Hands[0];

			transform.forward = new Vector3 ( hand.PalmNormal.x, hand.PalmNormal.y, hand.PalmNormal.z);

		}
	}

	void FixedUpdate() {
		LeapControls ();
	}
}

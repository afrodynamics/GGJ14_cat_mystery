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

	}

	void FixedUpdate() {
		LeapControls ();
	}
}

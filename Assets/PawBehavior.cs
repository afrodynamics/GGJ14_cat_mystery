using UnityEngine;
using System.Collections;

public class PawBehavior : MonoBehaviour {
	public float pawSpeed = 100;

	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = Color.black;
		gameObject.renderer.enabled = false;
		rigidbody.collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{

		//transform.position = Camera.main.transform.TransformPosition (Vector3.forward);
		if (Input.GetKey(KeyCode.LeftShift)) {
			gameObject.renderer.enabled = true;
			rigidbody.renderer.enabled = true;
			// Rotate around the z-axis to turn camera view
			/*
			rigidbody.velocity += new Vector3(pawSpeed * rigidbody.transform.forward.x,
			                       pawSpeed * rigidbody.transform.forward.y,
			                       pawSpeed * rigidbody.transform.forward.z);
			*/
		}
		else
		{
			gameObject.renderer.enabled = false;
			rigidbody.renderer.enabled = false;
		}

	}
}

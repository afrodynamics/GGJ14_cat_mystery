using UnityEngine;
using System.Collections;

public class ObjectiveColor : MonoBehaviour {

	public int objectiveNumber;
	public GameManager gm;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision col) {
		if (col.transform.tag == "MainCamera" && gm.triggersLeft == objectiveNumber) {
			gm.triggersLeft--;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (gm.triggersLeft == objectiveNumber) {
			gameObject.renderer.material.color = Color.green;
		} else if (gm.triggersLeft < objectiveNumber) {
			gameObject.renderer.material.color = Color.red;
		}

	
	}
}

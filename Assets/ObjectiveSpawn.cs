using UnityEngine;
using System.Collections;

public class ObjectiveSpawn : MonoBehaviour {

	public int objectiveNumber;
	public GameManager gm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (gm.triggersLeft == objectiveNumber) {
			gameObject.renderer.enabled = true;
			gameObject.rigidbody.isKinematic = false;
			//gameObject.rigidbody.WakeUp();
		} 
	
	}
}

using UnityEngine;
using System.Collections;

public class ObjectivePlaySound : MonoBehaviour {

	public int objectiveNumber;
	public GameManager gm;


	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision col) {
		if (col.transform.tag == "MainCamera" ) { //&& gm.triggersLeft == objectiveNumber) {

			//AudioSource.
			this.audio.Play ();	

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

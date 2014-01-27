using UnityEngine;
using System.Collections;

public class BedScript : MonoBehaviour {
	public GameManager gm;

	void OnCollisionEnter(Collision col) {
		if ( gm.triggersLeft == 1) {
			gm.triggersLeft--;
		}
	}
	// Use this for initialization
	void Start () {
		//gameObject.renderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		if (gm.triggersLeft == 1) {
			gameObject.renderer.material.color = Color.green;
		}
	}
}

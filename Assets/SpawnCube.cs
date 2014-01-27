using UnityEngine;
using System.Collections;

public class SpawnCube : MonoBehaviour {
	public GameManager gm;

	// Use this for initialization
	void Start () {
		//gameObject.renderer.enabled = false;
		//gameObject.collider.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (gm.triggersLeft == 1) 
		{
			gameObject.renderer.material.color = Color.green;
			gameObject.renderer.enabled = true;
			gameObject.collider.enabled = true;

		}
	}
}

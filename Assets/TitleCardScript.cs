using UnityEngine;
using System.Collections;

public class TitleCardScript : MonoBehaviour {
	public int loadlevel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
			AutoFade.LoadLevel (loadlevel, 3, 2, Color.black);
	}
	
}

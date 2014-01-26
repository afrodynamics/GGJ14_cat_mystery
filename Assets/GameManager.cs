using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int triggersLeft = 3;
	public int loadlevel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (triggersLeft == 0)
		{
			AutoFade.LoadLevel(loadlevel,2,5,Color.black);	
		}
	}
}

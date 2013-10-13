using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {
	
	public string nextLevel;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter() {
		if (nextLevel != null && nextLevel.Length != 0) {
			Debug.Log("Loading Level: " + nextLevel);
			Application.LoadLevel(nextLevel);
		}
	}
}

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LoadScene() {
		Debug.Log("Loading level..");
		Application.LoadLevel("Level0");	
	}
}

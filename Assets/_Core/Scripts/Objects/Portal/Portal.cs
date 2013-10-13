using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	public GameObject exit = null;
	public int exitDirection = 1;
	public bool waitForExit = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider c) {
		if (exit != null && waitForExit == false) {
			GameObject go = c.gameObject;
			Portal p = exit.GetComponent<Portal>();
			p.waitForExit = true;
			go.transform.position = exit.transform.position;
			
		}
	}
	
	public void OnTriggerExit(Collider c) {
		waitForExit = false;	
	}
}

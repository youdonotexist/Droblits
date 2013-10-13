using UnityEngine;
using System.Collections;

public class SlingshotExitTrigger : MonoBehaviour {
	public GameObject slingshot; 
	
	void OnTriggerExit(Collider c) {
		Slingshot ss = slingshot.GetComponent<Slingshot>();
		ss.PlayerLeftTrigger(c.gameObject);
	}
}

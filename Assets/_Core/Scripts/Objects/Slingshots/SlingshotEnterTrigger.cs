using UnityEngine;
using System.Collections;

public class SlingshotEnterTrigger : MonoBehaviour {
	public GameObject slingshot; 
	
	void OnTriggerEnter(Collider c) {
		Slingshot ss = slingshot.GetComponent<Slingshot>();
		ss.PlayerEnteredTrigger(c.gameObject);
		gameObject.collider.enabled = false;
	}
}

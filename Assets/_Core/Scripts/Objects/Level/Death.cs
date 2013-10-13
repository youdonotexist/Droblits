using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	public GameObject spawn;
	
	void OnTriggerEnter(Collider c) {
		GameObject go = c.gameObject;
		
		if (spawn != null && c.gameObject.tag.Equals("Player")) {
			go.rigidbody.isKinematic = false;
			go.rigidbody.velocity = Vector3.zero;
			go.transform.position = spawn.transform.position;
		}
	}
}

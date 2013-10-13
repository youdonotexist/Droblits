using UnityEngine;
using System.Collections;

public class MovingSpikePlatform : MonoBehaviour {
	public GameObject start;
	public GameObject end;
	
	bool atEnd = false;
	
	
	// Use this for initialization
	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash("position", end.transform.position, "duration", 0.5f, "oncomplete", "OnComplete"));
		atEnd = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnComplete() {
		if (atEnd == false) {
			iTween.MoveTo(gameObject, iTween.Hash("position", end.transform.position, "duration", 0.5f, "oncomplete", "OnComplete"));
			atEnd=true;
		}
		else {
			iTween.MoveTo(gameObject, iTween.Hash("position", start.transform.position, "duration", 0.5f, "oncomplete", "OnComplete"));
			atEnd = false;
		}
	}
}

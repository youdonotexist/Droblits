using UnityEngine;
using System.Collections;

using Vectrosity;

public class SceneTracker : MonoBehaviour
{
	public enum MOUSE_STATE {
		FIRST_DOWN,
		HELD,
		MOVING,
		JUST_RELEASED,
		RELEASED
	}
	
	MOUSE_STATE mouse_state = MOUSE_STATE.RELEASED;
	float x;
	float y;
	
	//Visual
	VectorLine line = null;
	public Material lineMaterial = null;
	
	//Vector3 firstTouch = Vector3.zero;
	//Vector3 secondTouch = Vector3.zero;
	
	GameObject firstObject = null;
	GameObject secondObject = null;
	
	GameObject currentTramp = null;
	
	GameObject controlledDroblit = null;
	
	ArrayList tramps = new ArrayList();
	
	public GameObject slingShots = null;
	
	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1;
		controlledDroblit = GameObject.Find("Droblit");
	}
	
	

	// Update is called once per frame
	void Update ()
	{
		getMouseState();
		
		//Debug.Log("Mouse state: " + mouse_state.ToString());
		
		if (mouse_state == SceneTracker.MOUSE_STATE.FIRST_DOWN) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			LayerMask nodeMask = 1 << LayerMask.NameToLayer("Nodes");
			
			RaycastHit o = new RaycastHit();
			if (Physics.Raycast(ray, out o, Mathf.Infinity, LayerMask.NameToLayer("Drobits")) == false) {
				firstObject = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/PinkNode"));
				
				currentTramp = new GameObject();
				firstObject.transform.parent = currentTramp.transform;
				
				Vector3 pos = ray.origin; pos.z = -10.0f;
				firstObject.transform.position = pos;
				
				if (tramps.Count > 3) {
					GameObject go = (GameObject) tramps[0];
					tramps.RemoveAt(0);
					GameObject.Destroy(go);
				}
				
				tramps.Add(currentTramp);
				
				//Time.timeScale = 0;
			}
		}
		else if (mouse_state == SceneTracker.MOUSE_STATE.JUST_RELEASED) {
			if (firstObject != null) {
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				
				RaycastHit o = new RaycastHit();
				if (Physics.Raycast(ray, out o) == false) {
					Vector3 pos = ray.origin; pos.z = -10.0f;
					if (Vector3.Distance(pos, firstObject.transform.position) > 1.0f) {
						secondObject = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/PinkNode"));
						secondObject.transform.position = pos;
					
						secondObject.transform.parent = currentTramp.transform;
					
						buildRope();
					}
					else {
						if (currentTramp != null) {
							tramps.Remove(currentTramp);
							GameObject.Destroy(currentTramp);
						}
					}
				}
				else {
					if (currentTramp != null) {
						tramps.Remove(currentTramp);
						GameObject.Destroy(currentTramp);
					}
				}
				
				//Time.timeScale = 1;
				
			}
			
			line.points3 = new Vector3[] {Vector3.zero, Vector3.zero};
			line.Draw3D();
			
			firstObject = null;
			secondObject = null;
			currentTramp = null;
		}
		else if (mouse_state == SceneTracker.MOUSE_STATE.HELD) {
			if (line == null) {
				line = new Vectrosity.VectorLine("DrawBand", new Vector3[] {Vector3.zero, Vector3.zero}, lineMaterial, 10.0f);
			}
			
			if (firstObject != null) {
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				Vector3 pos = ray.origin; pos.z = -10.0f;
				
				line.points3 = new Vector3[] {firstObject.transform.position, pos};
				line.Draw3D ();
			}
			
		}
		
		if (Input.GetKey (KeyCode.A)) {
			Vector3 vec = Camera.mainCamera.transform.position;
			vec.x -= 0.2f;
			Camera.mainCamera.transform.position = vec;
		}
		if (Input.GetKey (KeyCode.D)) {
			Vector3 vec = Camera.mainCamera.transform.position;
			vec.x += 0.2f;
			Camera.mainCamera.transform.position = vec;
		}
		if (Input.GetKey (KeyCode.W)) {
			Vector3 vec = Camera.mainCamera.transform.position;
			vec.y += 0.2f;
			Camera.mainCamera.transform.position = vec;
		}
		if (Input.GetKey (KeyCode.S)) {
			Vector3 vec = Camera.mainCamera.transform.position;
			vec.y -= 0.2f;
			Camera.mainCamera.transform.position = vec;
		}
		
		/*for (int i = 0; i < tramps.Count; i++) {
			GameObject go = (GameObject) tramps[i];
			Band b = go.GetComponentInChildren<Band>();
			if (b != null) {
			Vector3 scale = b.transform.localScale;
			
				if (i == 0) { scale.x = 0.08f; scale.z = 0.08f; }
				if (i == 1) { scale.x = 0.09f; scale.z = 0.09f; }
				if (i == 2) { scale.x = 0.1f; scale.z = 0.1f; }
				if (i == 3) { scale.x = 0.2f; scale.z = 0.2f; }
			
				b.transform.localScale = scale;
			}
			
		}*/
	}
	
	public Vector3 checkPointA() {
		RaycastHit pA;
		
		if (Physics.Raycast(firstObject.transform.position, 
		                (secondObject.transform.position - firstObject.transform.position).normalized,
		                out pA, Mathf.Infinity)) {
			if (pA.rigidbody == firstObject.rigidbody)
				return pA.point;
		}	
		
		return Vector3.zero;
	}
	
	public Vector3 checkPointB() {
		RaycastHit pB;
		
		if (Physics.Raycast(secondObject.transform.position, 
		                (firstObject.transform.position - secondObject.transform.position).normalized,
		                out pB, Mathf.Infinity)) {
			if (pB.rigidbody == secondObject.rigidbody)
				return pB.point;
		}
		
		return Vector3.zero;
	}
	
	public void buildRope() {
		
		Vector3 pA = firstObject.transform.position;//checkPointA();
		Vector3 pB = secondObject.transform.position;//checkPointB();
		
		pA.z = 0.0f;
		pB.z = 0.0f;
		
		if (pA != Vector3.zero && pB != Vector3.zero) {
			GameObject pre = (GameObject) Resources.Load("Prefabs/Band");
			GameObject go = (GameObject) Instantiate(pre);
			
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = new Vector3(0.5f, 1.0f, 0.5f);
			
			Vector3 pos = Vector3.Lerp(pA, pB, 0.5f); pos.z = 0.0f;
			Vector3 localScale = go.transform.localScale;
			float dist = Vector3.Distance(pA,pB);
			localScale.y = dist * 0.5f;
			
			go.transform.localScale = localScale;
			go.transform.position = pos;
			go.transform.parent = currentTramp.transform;
			
			float xDiff = pA.x - pB.x;
			if (xDiff != 0.0f)
				go.transform.up = (xDiff > 0 ? pB-pA : pA-pB);
			else
				go.transform.up = (pA.y - pB.y > 0 ? pA - pB : pB - pA);
		}
	}
	
	public MOUSE_STATE getMouseState() {
		bool mouseDown 	= Input.GetMouseButtonDown(0);
		bool mouseUp 	= Input.GetMouseButtonUp(0);
		
		if (mouseDown == true) {
			mouse_state = SceneTracker.MOUSE_STATE.FIRST_DOWN;
			return mouse_state;
		}
		
		if (mouseUp == true) {
			mouse_state = SceneTracker.MOUSE_STATE.JUST_RELEASED;
			return mouse_state;
		}
		
		
		if (!mouseUp && !mouseDown) {
			if (mouse_state == SceneTracker.MOUSE_STATE.FIRST_DOWN)
				mouse_state = SceneTracker.MOUSE_STATE.HELD;
			else if (mouse_state == SceneTracker.MOUSE_STATE.JUST_RELEASED)
				mouse_state = SceneTracker.MOUSE_STATE.RELEASED;	
		}
		
		return mouse_state;
			
		//Debug.Log("Mouse State: " + mouse_state);
	}
	
	public void deleteBand(GameObject band) {
		GameObject parent = band.transform.parent.gameObject;
		tramps.Remove(parent);
		GameObject.Destroy(parent);
	}
}


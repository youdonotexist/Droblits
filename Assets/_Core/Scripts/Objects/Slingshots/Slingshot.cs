using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	public GameObject tracker = null;
	private SceneTracker trackerComponent = null;
	
	public GameObject enterTrigger = null;
	public GameObject exitTrigger = null;
	
	public GameObject endNode;
	public GameObject beginNode;
	
	public float maxForce = 1000.0f;
	public float maxDist = 2.0f;
	
	private GameObject controlledObject = null;
	
	private Vector3 tempDir = Vector3.zero;
	
	bool mouseControlled = false;
	
	private LineRenderer lr = null;
	
	// Use this for initialization
	void Start () {
		lr = gameObject.AddComponent<LineRenderer>();
		lr.SetVertexCount(3);
		lr.SetPosition(0, beginNode.transform.position);
		lr.SetPosition(1, Vector3.Lerp(endNode.transform.position, beginNode.transform.position, 0.5f));
		lr.SetPosition(2, endNode.transform.position);
		lr.SetWidth(0.1f, 0.1f);
		
		lr.material = (Material) Resources.Load("Materials/Rope");
		
		trackerComponent = tracker.GetComponent<SceneTracker>();
	
	}
	
	// Update is called once per frame
	void Update () {
		handleInput();
		if (controlledObject != null) {
			lr.SetPosition(1, controlledObject.transform.position);
		}
		else {
			lr.SetPosition(1, Vector3.Lerp(endNode.transform.position, beginNode.transform.position, 0.5f));
		}
		
		if (tempDir != Vector3.zero)
			Debug.DrawRay(Vector3.Lerp(endNode.transform.position, beginNode.transform.position, 0.5f), tempDir);
	}
	
	public void setControlledObject(GameObject go) {
		if (controlledObject != null)
			controlledObject.rigidbody.isKinematic = false;
		
		controlledObject = go;
		
		if (controlledObject != null) {
			controlledObject.rigidbody.isKinematic = true;
			controlledObject.transform.position = Vector3.Lerp(beginNode.transform.position, endNode.transform.position, 0.5f);
		}
	}
	
	private void handleInput() {
	if (controlledObject != null) {
	SceneTracker.MOUSE_STATE mouse_state = trackerComponent.getMouseState();
	if (mouse_state == SceneTracker.MOUSE_STATE.FIRST_DOWN) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit o = new RaycastHit();
			LayerMask drobMask = 1 << LayerMask.NameToLayer("Droblits");
			
			if (Physics.Raycast(ray, out o, Mathf.Infinity, drobMask)) {
				GameObject temp = o.collider.gameObject;
				//Debug.Log("We hit something..");
				if (temp.tag.Equals("Player")) {
					setControlledObject(o.collider.gameObject);
					mouseControlled = true;
				}	
			}
		}
		else if (mouse_state == SceneTracker.MOUSE_STATE.JUST_RELEASED) {
			if (controlledObject != null && mouseControlled == true) {
				Vector3 slingshotBase = Vector3.Lerp(beginNode.transform.position, endNode.transform.position, 0.5f);
				Vector3 shottedPos = controlledObject.transform.position;
				
				Vector3 dir = ((slingshotBase - shottedPos).normalized);
				float dist	= Vector3.Distance(slingshotBase, shottedPos);
				dir = dir * ((dist/maxDist) * maxForce);
				tempDir = dir;
				
				//Debug.Log("Shot out with: " + dir.ToString());
				
				controlledObject.rigidbody.isKinematic = false;
				controlledObject.rigidbody.AddForce(dir);
				
				mouseControlled = false;
				
				setControlledObject(null);
			}
		}
		else if (mouse_state == SceneTracker.MOUSE_STATE.HELD) {
			if (controlledObject != null && mouseControlled == true) {
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				
				Vector3 slingshotBase = Vector3.Lerp(beginNode.transform.position, endNode.transform.position, 0.5f);
				Vector3 intendedPos = ray.origin; intendedPos.z = 0.0f;
				
				Vector3 dir = intendedPos - slingshotBase;
				float length = dir.magnitude;
				
				if (length > maxDist) {
					Vector3 normalizeDir = dir.normalized;
					normalizeDir.Scale(new Vector3(maxDist, maxDist, 1.0f));
					Vector3 f = slingshotBase + normalizeDir;
					controlledObject.transform.position = f;
				}
				else {
					controlledObject.transform.position = intendedPos;	
				}
			}
		}	
		}
	}
	
	public void PlayerLeftTrigger(GameObject player) {
		//if (controlledObject != null) {
			enterTrigger.collider.enabled = true;
		//}
	}
	
	public void PlayerEnteredTrigger(GameObject player) {
		if (controlledObject == null)
			setControlledObject(player);
	}
}

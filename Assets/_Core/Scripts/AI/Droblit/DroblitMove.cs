using UnityEngine;
using System.Collections;

public class DroblitMove : MPAgent {
	
	private Vector2 forward;
	private bool falling = false;
	
	private float lastTime = 0.0f;
	
	Vector3 lastPosition;
	
	// Use this for initialization
	void Start () {
		forward = new Vector2(1.0f, 0.0f);
		lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		lastTime += Time.deltaTime;
		if (lastTime > 10.0f) {
			PackedSprite ps = gameObject.GetComponent<PackedSprite>();
			ps.PlayAnim(1);
			lastTime = 0.0f;
		}
		
		bool moving = Mathf.Abs(transform.position.x - lastPosition.x) > Mathf.Epsilon;
		falling = transform.position.x > lastPosition.x;
		
		if (rigidbody.isKinematic == false) {
			if (moving) {
				Debug.Log ("Moving: ");
				forward.x = Mathf.Sign(transform.position.x - lastPosition.x);
			}
			else {
				forward.x *= -1.0f;
				Debug.Log ("Not moving");
			}
		}
		
		Vector3 ls = transform.localScale; 
		ls.x = (forward.x >= 0.0f ? 1.0f : -1.0f); 
		transform.localScale = ls;
	}
	
	void FixedUpdate() {
		if (falling == false && rigidbody.isKinematic == false) {
			rigidbody.MovePosition(transform.position + new Vector3(forward.x * 3.0f * Time.deltaTime, 0.0f, 0.0f));
		}
		
		lastPosition = transform.position;
	}
	
	//public BehaveResult TickWalkAction (Tree sender) {
	//	if (falling == false && rigidbody.isKinematic == false) {
	//		Vector2 speed = forward * 3.0f;
	//		rigidbody.velocity = new Vector3(speed.x, 0.0f, 0.0f);
	//	}
	//	return BehaveResult.Running;
	//}
	
	public void OnCollisionEnter(Collision c) {
		//if (c.gameObject.tag.Equals("Bounds")) {
		//	forward = forward * -1;
		//	Vector3 v = rigidbody.velocity; v.x = 0.0f; rigidbody.velocity = v;
		//}
	}
	
	public void OnCollisionStay(Collision c) {
		/*if (wallCollide == c.collider) {
			timeSinceCollision += Time.deltaTime;;
			if (timeSinceCollision > 0.5f) {
				forward = forward * -1;
				Vector3 v = rigidbody.velocity; v.x = 0.0f; rigidbody.velocity = v;
				timeSinceCollision = 0.0f;
			}
		}*/
	}
	
	public void OnCollisionLeave (Collision c) {
		/*if (wallCollide == c.collider) {
			wallCollide = null;
			timeSinceCollision = 0.0f;
		}*/
	}
}

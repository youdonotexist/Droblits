using UnityEngine;
using System.Collections;
using Vectrosity;

public class Band : MonoBehaviour {
	Vector3 head = Vector3.zero;
	Vector3 coll = Vector3.zero;
	//private float power = 0.0f;
	// Use this for initialization
	void Start () {
		/*if (this.tag.Equals("PinkNode"))
			power = 500.0f;
		else if (this.tag.Equals("BrownNode"))
			power = 700.0f;*/
	}
	
	// Update is called once per frame
	void Update () {
		if (head != Vector3.zero)
			Debug.DrawLine(transform.position + (head * 50), transform.position);
		
		if (coll != Vector3.zero)
			Debug.DrawLine(transform.position + (coll * 50), transform.position);
	}
	
	void OnCollisionEnter(Collision collision) {
		GameObject go = collision.collider.gameObject;
		
		if (go.name.Equals("Droblit")) {
			Vector3 collisionPoint = collision.contacts[0].point;
			
			//head = ((transform.position + transform.up) - transform.position).normalized;
			//coll = (collisionPoint - transform.position).normalized;
			
			Vector3 right = transform.right;
			Vector3 left = (transform.right * -1.0f);
			Vector3 hit = (go.transform.position - transform.position).normalized;
			
			float dRight = Vector3.Distance(right, hit);
			float dLeft = Vector3.Distance(left, hit);
			
			Vector3 direction = ((Mathf.Abs(dLeft) > Mathf.Abs(dRight)) ? transform.right : (transform.right * -1.0f));
			
			Debug.Log ("Normal: " + collision.contacts[0].normal);
			Debug.Log ("Right: " + transform.right);
			
			/*Debug.Log("Heading: " + head.ToString());
			Debug.Log("Coll: " + coll.ToString());
			
			float dp = Utilities.AngleDir(transform.up, coll, new Vector3(0, 0, -1));
			
			Debug.Log("Bounce " + (dp < 0 ? "Right" : "Left"));*/
			VectorLine v= new Vectrosity.VectorLine("Go", new Vector3[] {transform.position, transform.position + (collision.contacts[0].normal * -10.0f)}, null, 10);
			v.Draw3D();
			
			
			//go.rigidbody.AddForce(direction * 500.0f);
			go.rigidbody.AddForce(collision.contacts[0].normal * -500.0f);
			
			SceneTracker st = Camera.main.GetComponent<SceneTracker>();
			if (st != null)
				st.deleteBand(gameObject);
		}
	}
}

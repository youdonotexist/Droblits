using UnityEngine;
using System.Collections;

public class Utilities : MonoBehaviour {

	//returns -1 when to the left, 1 to the right, and 0 for forward/backward
	public static float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp  	= Vector3.Cross(fwd, targetDir);
		float dir  		= Vector3.Dot(perp, up);
		
		if (dir > 0.0f) {
			return 1.0f;
		} else if (dir < 0.0f) {
			return -1.0f;
		} else {
			return 0.0f;
		}
	}

}

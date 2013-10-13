using UnityEngine;
//using UnityEditor;
using System.Collections;

public class Node : MonoBehaviour {
	
	public GameObject[] otherNodes;
	Vector2 scrollPosition;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*void OnEnable() {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("PinkNode");
		otherNodes = new GameObject[gos.Length];
		for (int i = 0; i < gos.Length; i++)
		{
			GameObject go = gos[i];
			if (go != gameObject)
				otherNodes[i] = go;
		}
    }
	
	public void OnInspectorGUI()
	{
    	scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
		GameObject[] gos = GameObject.FindGameObjectsWithTag("PinkNode");
		
		Debug.Log("Found items " + gos.Length.ToString());
		
        foreach( GameObject go in gos )
        {
              EditorGUILayout.ObjectField(go.name, go, typeof( GameObject ), true);
        } 

    	EditorGUILayout.EndScrollView();
	}*/
}

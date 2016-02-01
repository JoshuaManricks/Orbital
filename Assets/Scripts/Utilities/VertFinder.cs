using UnityEngine;
using System.Collections;

public class VertFinder : MonoBehaviour {

	public GameObject vertMarker;


	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		int i = 0;
		while (i < vertices.Length) {
			GameObject go = Instantiate(vertMarker) as GameObject;
			go.transform.position = vertices[i]*140f;
			//vertices[i] += Vector3.up * Time.deltaTime;
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

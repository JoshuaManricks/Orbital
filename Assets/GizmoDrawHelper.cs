using UnityEngine;
using System.Collections;

public class GizmoDrawHelper : MonoBehaviour {
	
	public GizmoShape shape;
	public Color color;
	public float size = 1;

	public bool onSelected = false;

	void OnDrawGizmos() {
		if (!onSelected) {
			Gizmos.color = color;
			if (shape == GizmoShape.Sphere) Gizmos.DrawSphere(transform.position, size);
			if (shape == GizmoShape.Cube) Gizmos.DrawCube(transform.position, new Vector3(size, size, size));
		}
	}

	void OnDrawGizmosSelected() {
		if (onSelected) {
			Gizmos.color = color;
			if (shape == GizmoShape.Sphere) Gizmos.DrawSphere(transform.position, size);
			if (shape == GizmoShape.Cube) Gizmos.DrawCube(transform.position, new Vector3(size, size, size));
		}
	}


}

public enum GizmoShape {
	Sphere,
	Cube
}
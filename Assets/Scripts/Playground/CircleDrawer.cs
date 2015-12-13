using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]
[ExecuteInEditMode]
public class CircleDrawer : MonoBehaviour {

	public Color lineColor;
	public float lineWidth;
	public float r;
	public float theta_scale = 0.2f;
	// Use this for initialization
	void Start () {
		int size = Mathf.CeilToInt((2.0f * Mathf.PI) / theta_scale)+1;

		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetColors(lineColor, lineColor);
		lineRenderer.SetWidth(lineWidth,lineWidth);
		lineRenderer.SetVertexCount(size);
		lineRenderer.useWorldSpace = false;

		int i = 0;
		for(float theta = 0; theta <= 2 * Mathf.PI; theta += theta_scale) {
			float x = r*Mathf.Cos(theta);
			float y = r*Mathf.Sin(theta);

			Vector3 pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
			i+=1;
		}
		lineRenderer.SetPosition(size-1, new Vector3(r,0,0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

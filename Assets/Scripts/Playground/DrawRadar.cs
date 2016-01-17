using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (TrailRenderer))]
public class DrawRadar : MonoBehaviour
{
	public float radius = 3f;
	public float width = 1f;
	public int numPart = 1;
	public float speed;
	public List<Transform> parts;
	public Ship ship;

	private float realRaduis;
	private float inverseCount;

	void Start ()
	{   
		for(int i = 0; i < numPart; ++i){
			var part = (GameObject)Instantiate(Resources.Load("DrawRadarPart"));
            part.layer = gameObject.layer;
			parts.Add(part.transform);
			part.transform.SetParent(transform);
			part.transform.localPosition = Vector3.zero;
			var renderer = part.GetComponent<CircleDrawer>();
			renderer.lineWidth = width;
			Color color;
			if(ship.fleet)
				color = new Color(ship.fleet.color.r,ship.fleet.color.g,ship.fleet.color.b,0.5f);
			else
				color = Color.green;
			renderer.lineColor = color;
			if(numPart != 1){
				renderer.drawRate = (1-0.5f)/numPart;
			}

			realRaduis = radius - width/2f;
			renderer.r = radius;
		}
		inverseCount = 1/(float)parts.Count;
	}

	void Update ()
	{   
		for(int i = 0; i < parts.Count; ++i){
			float radian = Time.fixedTime*speed + 2*i*Mathf.PI*inverseCount;
			var part = parts[i];
			part.rotation = Quaternion.AngleAxis(radian * Mathf.Rad2Deg,Vector3.forward);
//			Vector3 pos = new Vector3(Mathf.Cos(radian)*realRaduis,Mathf.Sin(radian)*realRaduis,0);
//			part.localPosition = pos;
		}
	}
}
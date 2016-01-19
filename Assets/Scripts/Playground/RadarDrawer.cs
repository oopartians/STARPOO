using UnityEngine;
using System.Collections;

public class RadarDrawer : MonoBehaviour {

    public Radar radar;
    public Ship ship;
    public MeshRenderer renderer;
    public Shader shader;

    public Light halo;

    Material material;

	void Start () {
        renderer.material.shader = shader;

        if (ship.fleet != null)
        {
            renderer.material.SetColor("_Color", ship.fleet.team.color);
        }
        else
        {
            renderer.material.SetColor("_Color", Color.green);
        }

        transform.localScale = Vector3.one * radar.radarRadius * 2;
        renderer.enabled = true;

	}

	void Update(){
		transform.rotation = Quaternion.identity;
	}
	
}

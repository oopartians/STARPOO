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

        renderer.material.SetColor("_Color", ship.fleet.team.color);

        transform.localScale = Vector3.one * radar.radarRadius * 2;
        renderer.enabled = true;

        halo.range = radar.radarRadius*2;
        halo.color = ship.fleet.team.color;

	}

	void Update(){
		transform.rotation = Quaternion.identity;
	}
	
}

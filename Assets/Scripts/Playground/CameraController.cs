using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float zoomMax;
	public float zoomMin;
	public float zoomSpeed = 1f;
	public float moveSpeed = 0.4f;
	public float farClipZ = 7;

	Camera cam;
    bool clicked;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
        cam.farClipPlane = -transform.localPosition.z + farClipZ;
		//if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			//transform.Translate(Vector3.forward*zoomSpeed);
			//cam.farClipPlane = -transform.localPosition.z + farClipZ;
            //cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
		//} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			//transform.Translate(Vector3.back*zoomSpeed);
            //cam.farClipPlane = -transform.localPosition.z + farClipZ;
       // }
        cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel")*zoomSpeed;

        if (Input.GetMouseButton(0))
        {
            transform.Translate(Vector3.left * Input.GetAxis("Mouse X"));
            transform.Translate(Vector3.down * Input.GetAxis("Mouse Y"));
        }
	}
}

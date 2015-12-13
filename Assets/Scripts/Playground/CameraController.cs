using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float zoomMax;
	public float zoomMin;
	public float zoomSpeed = 1f;
	public float moveSpeed = 0.4f;

	Camera cam;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			transform.Translate(Vector3.forward*zoomSpeed);
			cam.farClipPlane = -transform.localPosition.z + 2;
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			transform.Translate(Vector3.back*zoomSpeed);
			cam.farClipPlane = -transform.localPosition.z + 2;
		}


		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(Vector3.right * moveSpeed);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(Vector3.left * moveSpeed);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate(Vector3.up * moveSpeed);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate(Vector3.down * moveSpeed);
		}
	}
}

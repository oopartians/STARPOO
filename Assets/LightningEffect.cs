using UnityEngine;
using System.Collections;

public class LightningEffect : MonoBehaviour {

	public GameObject eff;

	// Use this for initialization
	void Start () {
		eff.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator ShowLightning(){
		float randomRotation = Random.Range(30,60);
		int i = 0;

		eff.SetActive(true);
		while ( i++ < 4){
			randomRotation = Random.Range(30,60);
			eff.transform.rotation = Quaternion.AngleAxis(randomRotation,Vector3.forward);

			yield return null;
		}
		eff.SetActive(false);
	}

	public void Show()
	{
		StartCoroutine(ShowLightning());
	}
}

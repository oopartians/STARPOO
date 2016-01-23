using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public GameObject pauseMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!NetworkValues.isNetwork){
			if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)){
				if(!GameValueSetter.paused){
					GameValueSetter.paused = true;
					pauseMenu.SetActive(true);
				}
				else{
					GameValueSetter.paused = false;
					pauseMenu.SetActive(false);
				}
			}
		}
	}

	public void Unpause(){
		GameValueSetter.paused = false;
	}
}

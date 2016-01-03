using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ButtonMoveScene : MonoBehaviour {
	public string sceneName;
	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (MoveScene);
	}

    void MoveScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

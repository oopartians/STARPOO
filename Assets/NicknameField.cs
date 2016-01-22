using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NicknameField : MonoBehaviour {
    public InputField inputField;

	void Start () {
	    inputField.text = PlayerPrefs.GetString("name","");
	}
	
}

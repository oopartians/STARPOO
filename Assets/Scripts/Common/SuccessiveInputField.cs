using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(InputField))]
public class SuccessiveInputField : MonoBehaviour {
	public bool focusWithEnterKey;
	[Serializable]
	public class UnityEventString : UnityEvent<string>{};
	public UnityEventString onReturn;
	InputField inputField;
	// Use this for initialization
	void Start () {
		inputField = GetComponent<InputField>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			if(inputField.text.Length > 0){
				onReturn.Invoke(inputField.text);
                inputField.text = "";
                inputField.ActivateInputField();
			}
			if(focusWithEnterKey){
				inputField.ActivateInputField();
			}
		}
	}
}

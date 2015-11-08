using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JavascriptPannel : MonoBehaviour {
	public string name{
		get{
			return _name;
		}
		set{
			_name = value;
			GetComponentInChildren<Text>().text = value;
		}
	}
	public string path;

	string _name;
}

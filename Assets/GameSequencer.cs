using UnityEngine;
using System.Collections;
using System.IO;

public class GameSequencer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] scripts = LoadJavascripts ();
		MakeFleets (scripts);
	}

	string[] LoadJavascripts(){

		string[] javascriptPaths = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Script\", "*.js");
		for (int i = 0; i < javascriptPaths.Length; i++)
			javascriptPaths [i] = javascriptPaths [i];

		return javascriptPaths;
	}

	void MakeFleets(string[] javascriptPaths){
		for(int i = 0; i < javascriptPaths.Length; i++)
		{
			GameObject fleet = (GameObject)Instantiate(Resources.Load("Fleet"));
			Debug.Log(javascriptPaths[i]);
			fleet.GetComponent<Fleet>().javascriptPath = javascriptPaths[i];
		}
	}
}

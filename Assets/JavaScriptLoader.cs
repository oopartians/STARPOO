using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class JavascriptLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] javascriptPaths = LoadJavascripts ();

		foreach (string path in javascriptPaths) {
			GameObject scriptPannelObj = (GameObject)Instantiate(Resources.Load("ScriptPannel"));
			scriptPannelObj.transform.SetParent(transform);
			scriptPannelObj.transform.localScale = Vector3.one;

			scriptPannelObj.name =  Path.GetFileNameWithoutExtension(path);
			var scriptPannel = scriptPannelObj.GetComponent<JavascriptPannel>();

			Debug.Log(scriptPannel.name);
			scriptPannel.name = scriptPannelObj.name;
			scriptPannel.path = path;
		}
	}


	string[] LoadJavascripts(){
		
		string[] javascriptPaths = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Script\", "*.js");
		for (int i = 0; i < javascriptPaths.Length; i++)
			javascriptPaths [i] = javascriptPaths [i];
		
		return javascriptPaths;
	}
}

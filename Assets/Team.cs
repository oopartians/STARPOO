using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team {
	public string name;
	public TeamStat stat;
	public Color color;

	public List<Fleet> fleets{get{return _fleets;}}

	List<Fleet> _fleets = new List<Fleet>();
	List<string> jsPaths = new List<string>();

	public void AddJSPath(string path){
		jsPaths.Add(path);
	}

	public List<string> GetJSPaths(){
		return jsPaths;
	}

	public void ReportDestroy(Fleet fleet){

	}

	public void AddFleet(Fleet fleet){
		fleet.color = color;
		_fleets.Add(fleet);
	}

}

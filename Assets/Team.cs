using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team {
	public string name;
	public TeamStat stat;
	public Color color;
	public float positionAngle;
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

	public void CompleteAddFleets(){
		int i = 0;
		foreach (Fleet fleet in _fleets) {
			fleet.positionAngle = positionAngle + ((i++) - (_fleets.Count-1)/2) * 20;
		}
	}

}

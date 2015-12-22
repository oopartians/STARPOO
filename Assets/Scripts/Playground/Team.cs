using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Jurassic.Library;

public class Team : MonoBehaviour {
	public string name;
	public TeamStat stat;
	public Color color;
	public float positionAngle;
	public List<Fleet> fleets{get{return _fleets;}}
	public TeamAIInformation aiInfor;
    private int destroyedfleetcount = 0;

    List<Fleet> _fleets = new List<Fleet>();
	List<string> jsPaths = new List<string>();

	public void AddJSPath(string path){
		jsPaths.Add(path);
	}

	public List<string> GetJSPaths(){
		return jsPaths;
	}

	public void ReportDestroy(Fleet fleet)
	{
	    destroyedfleetcount++;
        if (_fleets.Count == destroyedfleetcount)
        {
            Match.ReportDestroy(fleet.team);
        }
    }

	public void MakeFleets(){
		foreach(string path in jsPaths){
			GameObject fleetObject = (GameObject)Instantiate(Resources.Load("Fleet"));
			Fleet fleet = fleetObject.GetComponent<Fleet>();
			FleetAILoader fleetAILoader = fleetObject.GetComponent<FleetAILoader>();
			fleetAILoader.SetJavaScriptPath(path);
			fleet.team = this;
			fleet.color = color;
			fleet.javascriptPath = path;
			_fleets.Add(fleet);
		}
	}

	public void InitFleetsAngle(){
		int i = 0;
		foreach (Fleet fleet in _fleets) {
			fleet.positionAngle = positionAngle + ((i++) - (_fleets.Count-1)/2) * 20;
		}
	}


	void Awake(){
		aiInfor = gameObject.GetComponent<TeamAIInformation> ();
	}

}

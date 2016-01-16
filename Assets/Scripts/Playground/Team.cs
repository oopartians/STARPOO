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
	public bool destroyedByTimePenalty = false;

    List<Fleet> _fleets = new List<Fleet>();
    List<JSInfo> jsInfos = new List<JSInfo>();

	public void AddJSInfo(JSInfo jsInfo){
        jsInfos.Add(jsInfo);
	}

    public List<JSInfo> GetJSInfos()
    {
		return jsInfos;
	}

	public void ReportDestroy(Fleet fleet)
	{
	    destroyedfleetcount++;
        if (_fleets.Count == destroyedfleetcount)
        {
			if (fleet.destroyedByTimePenalty)
				this.destroyedByTimePenalty = true;
            Match.ReportDestroy(fleet.team);
        }
    }

	public void MakeFleets(){
		foreach(JSInfo info in jsInfos){
			GameObject fleetObject = (GameObject)Instantiate(Resources.Load("Fleet"));
			Fleet fleet = fleetObject.GetComponent<Fleet>();
			FleetAILoader fleetAILoader = fleetObject.GetComponent<FleetAILoader>();
			fleetAILoader.code = info.code;
			fleetAILoader.isMine = info.isMine;
			fleet.team = this;
			fleet.color = info.color;
			fleet.jsName = info.name;
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

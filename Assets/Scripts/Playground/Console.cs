using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Console : MonoBehaviour {

	public void ExcuteCommand(){

	}

	List<FleetAILoader> commandableFleets = new List<FleetAILoader>();

	Toggle selectedToggle;
	FleetAILoader selectedAI;

	// Use this for initialization
	void Start () {
		foreach(Team team in Match.teams){
			foreach(Fleet fleet in team.fleets){
				commandableFleets.Add(fleet.GetComponent<FleetAILoader>());
				//TODO:create btn and addListener :
				/*
				var toggle = new Toggle();
				toggle.onValueChanged.AddListener((bool isOn)=>{
					if(isOn){
						if(selectedToggle != toggle){
							selectedToggle.isOn = false;
							selectedToggle = toggle;
						}
						selectedAI = fleet.GetComponent<FleetAILoader>();
					}
					else{
						if(selectedToggle == toggle){
							selectedToggle = null;
						}
					}
				});*/


			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

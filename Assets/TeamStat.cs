using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamStat : MonoBehaviour {

	public Text textName;
	public Text textKillEnemy;
	public Text textKillAlly;
	public Team team;

	// Use this for initialization
	void Start () {
		textKillEnemy.text = "0";
		textKillAlly.text = "0";
		textName.text = team.name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

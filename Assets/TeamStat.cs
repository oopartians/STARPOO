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
		Color color = new Color (team.color.r, team.color.g, team.color.b, 0.5f);
		GetComponent<RawImage> ().color = color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

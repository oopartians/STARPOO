using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class PlaygroundUpdater : MonoBehaviour {
	public TimeCounter timeCounter;
	public Console console;

	void Start(){
	}

	void FixedUpdate(){
		//if(NetworkValues.currentTick >= NetworkValues.acceptedTick && NetworkValues.isNetwork) return;

		foreach (Team team in Match.teams) {
			foreach (Fleet fleet in team.fleets) {
				    fleet.aiLoader.FixedUpdate2();
			}
		}

		console.FixedUpdate2();

		foreach (Team team in Match.teams) {
			foreach (Fleet fleet in team.fleets) {
				foreach (Ship ship in fleet.ships) {
					ship.FixedUpdate2();
				}
			}
		}
		foreach (Bullet bullet in Bullet.list) {
			bullet.FixedUpdate2();
		}

		timeCounter.FixedUpdate2();

		++NetworkValues.currentTick;

        if (NetworkValues.isNetwork)
        {
            if (NetworkValues.currentTick + NetworkValues.networkTickTerm >= NetworkValues.acceptedTick && NetworkValues.requestedTick <= NetworkValues.acceptedTick)
            {
                NetworkValues.requestedTick = NetworkValues.acceptedTick + NetworkValues.networkTickTerm;
                Debug.Log(NetworkValues.acceptedTick + ":" + NetworkValues.currentTick);
                Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.REQUESTTICK, (NetworkValues.requestedTick).ToString()));
            }
        }
	}
}
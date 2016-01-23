using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class PlaygroundUpdater : MonoBehaviour {
	public TimeCounter timeCounter;
	public Console console;

	void Start(){
	}

	void FixedUpdate(){
		if(!NetworkValues.isNetwork && GameValueSetter.paused) return;
		if(NetworkValues.currentTick >= NetworkValues.acceptedTick && NetworkValues.isNetwork) return;

		foreach (Team team in Match.teams) {
			foreach (Fleet fleet in team.fleets) {
				if(fleet == null) continue;
			    fleet.aiLoader.FixedUpdate2();
			}
		}

		console.FixedUpdate2();

		foreach (Team team in Match.teams) {
			foreach (Fleet fleet in team.fleets) {
				foreach (Ship ship in fleet.ships) {
					if(ship == null) continue;
					ship.FixedUpdate2();
				}
			}
		}
		foreach (Team team in Match.teams) {
			foreach (Fleet fleet in team.fleets) {
				foreach (Ship ship in fleet.ships) {
					if(ship == null) continue;
					ship.ComputePushing();
				}
			}
		}
		foreach (Team team in Match.teams) {
			foreach (Fleet fleet in team.fleets) {
				foreach (Ship ship in fleet.ships) {
					if(ship == null) continue;
					ship.ApplyPushing();
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
                Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.REQUESTTICK, (NetworkValues.requestedTick).ToString()));
            }
        }
	}
}
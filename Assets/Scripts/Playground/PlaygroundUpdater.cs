
using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class PlaygroundUpdater : MonoBehaviour {
	public TimeCounter timeCounter;
	public Console console;

    bool playing = false;
    public void Go(){
        Bullet.readyQueue.Clear();
        Bullet.list.Clear();
        playing = true;
    }

	void FixedUpdate(){
        if (!NetworkValues.isNetwork && GameValueSetter.paused && !playing) return;
		//if(NetworkValues.currentTick >= NetworkValues.acceptedTick && NetworkValues.isNetwork) return;

        Bullet.GoBullets();
        
        Team team;
        int i,j,k;

		for (i=0;i<Match.teams.Count;++i) {
            team = Match.teams[i];
            for (j = 0; j < team.fleets.Count; ++j )
            {
                var fleet = team.fleets[j];
                if (fleet == null) continue;
                fleet.aiLoader.FixedUpdate2();
            }
		}

		console.FixedUpdate2();
        
		for (i=0;i<Match.teams.Count;++i) {
            team = Match.teams[i];
            for (j = 0; j < team.fleets.Count; ++j) {
                var fleet = team.fleets[j];
				for (k = 0; k < fleet.ships.Count; ++k) {
                    var ship = fleet.ships[k];
					if(ship == null) continue;
					ship.FixedUpdate2();
				}
			}
		}
        /*
        for (i = 0; i < Match.teams.Count; ++i)
        {
            team = Match.teams[i];
            for (j = 0; j < team.fleets.Count; ++j)
            {
                var fleet = team.fleets[j];
                for (k = 0; k < fleet.ships.Count; ++k)
                {
                    var ship = fleet.ships[k];
					if(ship == null) continue;
					ship.ComputePushing();
				}
			}
        }
        for (i = 0; i < Match.teams.Count; ++i)
        {
            team = Match.teams[i];
            for (j = 0; j < team.fleets.Count; ++j)
            {
                var fleet = team.fleets[j];
                for (k = 0; k < fleet.ships.Count; ++k)
                {
                    var ship = fleet.ships[k];
					if(ship == null) continue;
					ship.ApplyPushing();
				}
			}
		}*/
        
		

		timeCounter.FixedUpdate2();
        ++NetworkValues.currentTick;
		/*
        if (NetworkValues.isNetwork)
        {
            if (NetworkValues.currentTick + NetworkValues.networkTickTerm >= NetworkValues.acceptedTick && NetworkValues.requestedTick <= NetworkValues.acceptedTick)
            {
                NetworkValues.requestedTick = NetworkValues.acceptedTick + NetworkValues.networkTickTerm;
                Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.REQUESTTICK, (NetworkValues.requestedTick).ToString()));
            }
        }*/
	}
}
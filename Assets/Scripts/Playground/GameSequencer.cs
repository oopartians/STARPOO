using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameSequencer : MonoBehaviour {
	public GameUI ui;
    public PlaygroundUpdater updator;
	// Use this for initialization
    bool started = false;

	void FixedStart () {
		MakeFleets ();
		Record.Init (ui);
	}

    void FixedUpdate()
    {
        if (!started)
        {
            FixedStart();
            updator.Go();
            started = true;
        }
    }

	void MakeFleets(){
        foreach (Team team in Match.teams)
        {
            team.MakeFleets();
            team.InitFleetsAngle();
        }
        foreach (Team team in Match.teams)
        {
            foreach (var fleet in team.fleets)
            {
                fleet.FixedStart();
            }
        }
	}
}

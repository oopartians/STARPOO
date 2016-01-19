using UnityEngine;
using System.Collections;

public static class ScanUtils {

    public static bool NeedScanning(Team team, Team otherTeam = null)
    {
        var cond1 = (Match.myTeam.Contains(team) || Match.myTeam.Count == 0 || Match.myTeam.Count == Match.teams.Count);
        if (otherTeam != null)
        {
            return cond1 && !Match.myTeam.Contains(otherTeam);
        }
        else
        {
            return cond1;
        }
    }

    public static void ChangeLayersRecursively(this Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }
}

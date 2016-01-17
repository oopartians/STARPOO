using UnityEngine;
using System.Collections;

public static class ScanUtils {

    public static bool IsVisible(Team team)
    {
        return Match.myTeam.Contains(team) || Match.myTeam.Count == 0;
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

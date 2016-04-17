using UnityEngine;
using System.Collections;

public class INumberable : MonoBehaviour {
    static int numbering = 0;
    [HideInInspector]
    public int number = -1;
    public void DoNumbering()
    {
        number = numbering++;
    }
}

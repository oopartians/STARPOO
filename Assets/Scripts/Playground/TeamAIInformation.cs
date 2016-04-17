using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TeamAIInformation : MonoBehaviour {

    public Dictionary<IJSONExportable, int> scannedBulletsCounter = new Dictionary<IJSONExportable, int>();
    public List<IJSONExportable> scannedBullets = new List<IJSONExportable>();
    public List<IJSONExportable> allyShips = new List<IJSONExportable>();
    public Dictionary<IJSONExportable, int> scannedEnemyShipsCounter = new Dictionary<IJSONExportable, int>();
    public List<IJSONExportable> scannedEnemyShips = new List<IJSONExportable>();
}

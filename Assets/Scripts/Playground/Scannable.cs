using UnityEngine;
using System.Collections;

public class Scannable : MonoBehaviour {
    
    int scanCount = 0;


    void Start()
    {
        ChangeScanCount(0);
    }


    public void ChangeScanCount(int v)
    {
        if(!GameValueSetter.fogmode){
            ScanUtils.ChangeLayersRecursively(transform, "Scanned");
        }
        scanCount += v;

        if (scanCount > 0)
        {
            ScanUtils.ChangeLayersRecursively(transform, "Scanned");
        }
        else
        {
            ScanUtils.ChangeLayersRecursively(transform, "Unscanned");
        }
    }
}

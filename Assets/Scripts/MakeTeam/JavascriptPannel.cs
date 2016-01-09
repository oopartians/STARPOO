using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JavascriptPannel : MonoBehaviour {
    public JSInfo jsInfo;

    public void UpdateInfo(){

        GetComponentInChildren<Text>().text = jsInfo.name;
        GetComponent<Image>().color = new Color(jsInfo.color.r, jsInfo.color.g, jsInfo.color.b, 0.5f);
        name = jsInfo.name;

    }
}

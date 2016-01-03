using UnityEngine;
using System.Collections;
using System.Text;

public class NetworkTeamPannel : MonoBehaviour {


    public string MakeNetworkMessage(GameObject pannel, bool code = false)
    {
        StringBuilder b = new StringBuilder(transform.GetSiblingIndex().ToString());
        b.Append(":");
        b.Append(pannel.name);
        if (code)
        {
            b.Append(":콛");
            b.Append(pannel.GetComponent<JavascriptPannel>().jsInfo.code);
        }

        return NetworkDecorator.AttachHeader(NetworkHeader.ADDJS, b.ToString());
    }

    DragDropSlot slot;
	// Use this for initialization
	void Start () {
        if (!NetworkValues.isNetwork)
        {
            return;
        }
        slot = GetComponent<DragDropSlot>();
        slot.onHi.Add(OnHi);
        slot.onBye.Add(OnBye);
	}

    void OnHi(GameObject jsPannel)
    {
        Client.instance.Send(MakeNetworkMessage(jsPannel,true));
    }

    void OnBye(GameObject jsPannel)
    {
        Client.instance.Send(MakeNetworkMessage(jsPannel));
    }
}

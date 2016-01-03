using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragDropSlot : MonoBehaviour,IDropHandler {
    public delegate void onDragDropDelegate(GameObject obj);
    public List<onDragDropDelegate> onHi = new List<onDragDropDelegate>();
    public List<onDragDropDelegate> onBye = new List<onDragDropDelegate>();
	public void OnDrop (PointerEventData eventData)
	{
		DragHandler.itemBeingDragged.transform.SetParent(transform);
	}
    public void OnHi(GameObject obj)
    {
        foreach (onDragDropDelegate fn in onHi)
        {
            fn(obj);
        }
    }
    public void OnBye(GameObject obj)
    {
        foreach (onDragDropDelegate fn in onBye)
        {
            fn(obj);
        }
    }

}

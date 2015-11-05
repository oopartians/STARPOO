using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragDropSlot : MonoBehaviour,IDropHandler {

	public void OnDrop (PointerEventData eventData)
	{
		DragHandler.itemBeingDragged.transform.SetParent(transform);
	}
}

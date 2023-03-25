using UnityEngine;
using UnityEngine.EventSystems;

public class DropInSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
    }
}

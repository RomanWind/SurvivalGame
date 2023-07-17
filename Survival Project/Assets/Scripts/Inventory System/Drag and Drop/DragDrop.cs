using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _itemRectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    private Transform _originalParent;
    private string _parentName;

    public string ParentName => _parentName;

    private void OnValidate()
    {
        _parentName = transform.parent.name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentName = transform.parent.name;
        _originalParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        _canvasGroup.alpha = 0.8f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _itemRectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_originalParent);
        transform.position = _originalParent.position;
        transform.localScale = Vector3.one;
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }
}

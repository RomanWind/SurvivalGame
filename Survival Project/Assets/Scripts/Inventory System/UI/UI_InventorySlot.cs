using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace InventorySystem.UI
{
    public class UI_InventorySlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private int _inventorySlotIndex;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _numberOfItems;
        private InventorySlot _slot;

        private void Start()
        {
            AssignSlot(_inventorySlotIndex);
        }

        public void AssignSlot(int slotIndex)
        {
            if(_slot != null) _slot.StateChanged -= OnStateChanged;

            _inventorySlotIndex = slotIndex;
            _inventory = GetComponentInParent<UI_Inventory>().Inventory;
            _slot = _inventory.Slots[_inventorySlotIndex];
            _slot.StateChanged += OnStateChanged;
            UpdateViewState(_slot.State, _slot.Active);
        }

        private void UpdateViewState(ItemStack state, bool active)
        {
            ItemDefinition item = state?.Item;
            bool hasItem = item != null;
            bool isStackable = hasItem && item.IsStackable;
            _itemIcon.enabled = hasItem;
            _numberOfItems.enabled = isStackable;
            if (!hasItem) return;

            _itemIcon.sprite = item.UiSprite;
            if(isStackable) _numberOfItems.SetText(state.NumberOfItems.ToString());
        }

        private void OnStateChanged(object sender, InventorySlotStateChangedArgs args)
        {
            UpdateViewState(args.NewState, args.Active);
        }

        //Drag and drop system
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"New Slot Name: {gameObject.name}");
            int oldSlotIndex = Convert.ToInt32(eventData.pointerDrag.GetComponent<DragDrop>().ParentName);
            int newSlotIndex = Convert.ToInt32(gameObject.name);
            _inventory.ReplaceItemSlot(oldSlotIndex, newSlotIndex);
            AssignSlot(oldSlotIndex);
            AssignSlot(newSlotIndex);
        }

        /*public void OnPointerDown(PointerEventData eventData)
        {
            itemPosition = gameObject.GetComponentInChildren<Image>().GetComponent<RectTransform>();
            int slotIndex = Convert.ToInt32(gameObject.name);
            Debug.Log($"Slot index: {itemPosition}");

            //Remove item from slot on click
            //_slot.State = _inventory.RemoveItem(slotIndex, 1);
        }*/
    }
}

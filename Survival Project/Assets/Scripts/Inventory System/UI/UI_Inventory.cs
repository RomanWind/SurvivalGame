using UnityEngine;
using System.Collections.Generic;

namespace InventorySystem.UI
{
    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField] private GameObject _inventorySlotPrefab;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private List<UI_InventorySlot> _slots;

        public Inventory Inventory => _inventory;

        [ContextMenu("Initialize Inventory")]
        private void InitializeInventoryUi()
        {
            if (_inventory == null || _inventorySlotPrefab == null) return;

            _slots = new List<UI_InventorySlot>(_inventory.Size);
            for(int i = 0; i < _inventory.Size; i++)
            {
                GameObject uiSlot = Instantiate(_inventorySlotPrefab);
                uiSlot.name = i.ToString();
                uiSlot.transform.SetParent(transform, false);
                UI_InventorySlot uiSlotScript = uiSlot.GetComponent<UI_InventorySlot>();
                uiSlotScript.AssignSlot(i);
                _slots.Add(uiSlotScript);
            }
        }
    }
}

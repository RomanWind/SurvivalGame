using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int _size = 8;
        [SerializeField] private List<InventorySlot> _slots;
        private int _activeSlotIndex;
        private ItemStack _temporalItem;

        public int Size => _size;
        public List<InventorySlot> Slots => _slots;
        public int ActiveSlotIndex 
        {
            get => _activeSlotIndex;
            private set
            {
                _slots[_activeSlotIndex].Active = false;
                _activeSlotIndex = value < 0 ? _size - 1 : value % Size;
                _slots[_activeSlotIndex].Active = true;
            }
        }
        private void OnValidate()
        {
            AdjustSize();
        }

        private void AdjustSize()
        {
            _slots ??= new List<InventorySlot>();
            if (_slots.Count > _size) _slots.RemoveRange(_size, _slots.Count - _size);
            if (_slots.Count < _size) _slots.AddRange(new InventorySlot[_size - _slots.Count]);
        }

        public bool IsFull()
        {
            return _slots.Count(slot => slot.HasItem) >= _size;
        }

        public bool CanAcceptItem(ItemStack itemStack)
        {
            var slotWithStackableItem = FindSlot(itemStack.Item, true);
            return !IsFull() || slotWithStackableItem != null;
        }

        private InventorySlot FindSlot(ItemDefinition item, bool onlyStackable = false)
        {
            return _slots.FirstOrDefault(slot => slot.Item == item && 
                                                 item.IsStackable || 
                                                 !onlyStackable);
        }

        public bool HasItem(ItemStack itemStack, bool checkNumberOfItems = false)
        {
            InventorySlot itemSlot = FindSlot(itemStack.Item);
            if(itemSlot == null) return false;
            if (!checkNumberOfItems) return true;
            if(itemStack.Item.IsStackable)
            {
                return itemSlot.NumberOfItems >= itemSlot.NumberOfItems;
            }

            return _slots.Count(slot => slot.Item == itemStack.Item) >= itemStack.NumberOfItems;
        }

        public ItemStack AddItem(ItemStack itemStack)
        {
            var relevantSlot = FindSlot(itemStack.Item, true);
            if(IsFull() && relevantSlot == null)
            {
                throw new InventoryException(InventoryOperation.Add, "Inventory is full");
            }

            if(relevantSlot != null)
            {
                relevantSlot.NumberOfItems += itemStack.NumberOfItems;
            }
            else
            {
                relevantSlot = _slots.First(slot => !slot.HasItem);
                relevantSlot.State = itemStack;
            }

            return relevantSlot.State;
        }

        public ItemStack RemoveItem(int atIndex, int amount)
        {
            if(!_slots[atIndex].HasItem)
            {
                throw new InventoryException(InventoryOperation.Remove, "Slot is Empty");
            }

            if(_slots[atIndex].State.NumberOfItems - amount > 0 && _slots[atIndex].State.IsStackable)
            {
                _slots[atIndex].State.NumberOfItems -= amount;
            }
            else
            {
                ClearSlot(atIndex);
            }
            
            return _slots[atIndex].State;
        }

        public void ReplaceItemSlot(int oldItemSlot, int newItemSlot)
        {
            if (!_slots[newItemSlot].HasItem)
            {
                _slots[newItemSlot].State = _slots[oldItemSlot].State;
                _slots[oldItemSlot].Clear();
            }
            else
            {
                _temporalItem = _slots[newItemSlot].State;
                _slots[newItemSlot].State = _slots[oldItemSlot].State;
                _slots[oldItemSlot].State = _temporalItem;
            }
        }

        public void ClearSlot(int atIndex)
        {
            _slots[atIndex].Clear();
        }

        public void ActivateSlot(int atIndex)
        {
            ActiveSlotIndex = atIndex;
        }
    }
}

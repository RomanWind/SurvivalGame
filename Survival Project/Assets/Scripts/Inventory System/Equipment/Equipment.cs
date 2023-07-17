using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace InventorySystem
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private InventorySlot[] _equipmentSlots;
        private InventorySlot _slot;

        private void OnValidate()
        {
            _equipmentSlots = new InventorySlot[8];
        }
    }
}
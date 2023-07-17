using UnityEngine;

namespace InventorySystem.UI
{
    public class UI_Equipment : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private GameObject[] _equipmentSlots = new GameObject[8];
    }
}

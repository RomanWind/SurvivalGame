using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/Item Definition", fileName = "New Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _isStackable;
        [SerializeField] private Sprite _inGameSprite;
        [SerializeField] private Sprite _iuSprite;

        [SerializeField] private int _attackBonus;
        [SerializeField] private int _defenceBonus;

        public string Name => _name;
        public bool IsStackable => _isStackable;
        public Sprite InGameSprite => _inGameSprite;
        public Sprite UiSprite => _iuSprite;

        public int AttackBonus => _attackBonus;
        public int DefenceBonus => _defenceBonus;
    }
}
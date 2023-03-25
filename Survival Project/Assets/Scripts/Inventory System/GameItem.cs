using UnityEngine;

namespace InventorySystem
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField] private ItemStack _stack;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public ItemStack Stack => _stack;

        private void OnValidate()
        {
            SetupGameObject();
            AdjustNumberOfItems();
            UpdateGameObjectName();
        }

        private void SetupGameObject()
        {
            if (_stack.Item == null) return;

            SetGameSprite();
        }

        private void SetGameSprite()
        {
            _spriteRenderer.sprite = _stack.Item.InGameSprite;
        }

        private void UpdateGameObjectName()
        {
            if (_stack.Item == null) return;

            string name = _stack.Item.Name;
            string number = _stack.IsStackable ? _stack.NumberOfItems.ToString() : "ns";
            gameObject.name = $"{name} ({number})";
        }

        private void AdjustNumberOfItems()
        {
            if (_stack.Item == null) return;
            
            _stack.NumberOfItems = _stack.NumberOfItems;
        }

        public ItemStack Pick()
        {
            Destroy(gameObject);
            return _stack;
        }
    }
}

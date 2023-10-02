using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ItemData itemData;
        private void SetupVisuals()
        {
            if (itemData == null)
                return;

            GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
            gameObject.name = "Item object - " + itemData.itemName;
        }


        public void SetupItem(ItemData _itemData, Vector2 _velocity)
        {
            itemData = _itemData;
            rb.velocity = _velocity;

            SetupVisuals();
        }

        public void PickupItem()
        {
            if (!Inventory.Instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
            {
                rb.velocity = new Vector2(0, 7);
                ConfigEvent.EffectPopUpTextEvent.EventTrigger("Inventory is full", PlayerManager.Instance.player.transform);
                return;
            }

            AudioManager.Instance.PlaySFX(9, transform);
            Inventory.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
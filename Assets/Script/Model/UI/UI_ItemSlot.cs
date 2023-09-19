using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
namespace RPGGame
{
    public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image itemImage;
        [SerializeField] protected Text itemText;

        protected UI ui;
        public InventoryItem item;

        protected virtual void Start()
        {
            ui = GetComponentInParent<UI>();
        }

        public void UpdateSlot(InventoryItem _newItem)
        {
            item = _newItem;

            itemImage.color = Color.white;

            if (item != null)
            {
                itemImage.sprite = item.data.itemIcon;

                if (item.stackSize > 1)
                {
                    itemText.text = item.stackSize.ToString();
                }
                else
                {
                    itemText.text = "";
                }
            }
        }

        public void CleanUpSlot()
        {
            item = null;

            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemText.text = "";
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (item == null)
                return;

            ui.itemToolTip.HideToolTip();

            if (Input.GetKey(KeyCode.LeftControl))
            {
                Inventory.Instance.RemoveItem(item.data);
                return;
            }

            if (item.data.itemType == ItemType.Equipment)
                Inventory.Instance.EquipItem(item.data);

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (item == null)
                return;

            ui.itemToolTip.ShowToolTip(item.data as ItemData_Equipment);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (item == null)
                return;

            ui.itemToolTip.HideToolTip();
        }
    }
}
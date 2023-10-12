using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    物品提示

-----------------------*/

namespace RPGGame 
{
    public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image itemImage;
        [SerializeField] protected TextMeshProUGUI itemText;

        public InventoryItem item;


        protected virtual void Start()
        {
            itemText = GetComponentInChildren<TextMeshProUGUI>();
            itemImage= GetComponent<Image>();
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

            ConfigEvent.EventItemTooltipClose.EventTrigger();

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
            ConfigEvent.EventItemTooltipShow.EventTrigger(item.data as ItemData_Equipment);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (item == null)
                return;
            ConfigEvent.EventItemTooltipClose.EventTrigger();
        }
    }
}
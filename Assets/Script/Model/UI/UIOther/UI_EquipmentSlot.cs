using Core;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    装备格子

-----------------------*/

namespace RPGGame 
{
    public class UI_EquipmentSlot : UI_ItemSlot
    {
        public EquipmentType slotType;

        private void Awake()
        {
            itemImage = transform.GetChildComponent<Image>("Background");
            itemText = transform.GetChildComponent<TextMeshProUGUI>("ItemAmount");
        }
        private void OnValidate()
        {
            gameObject.name = "装备格子 - " + slotType.ToString();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (item == null || item.data == null)
                return;

            Inventory.Instance.UnequipItem(item.data as ItemData_Equipment);
            Inventory.Instance.AddItem(item.data as ItemData_Equipment);

            ConfigEvent.EventItemTooltipClose.EventTrigger();

            CleanUpSlot();
        }
    }
}
using Core;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    װ������

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
            gameObject.name = "װ������ - " + slotType.ToString();
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
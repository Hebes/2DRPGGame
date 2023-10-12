using Core;
using UnityEngine.EventSystems;

/*--------½Å±¾ÃèÊö-----------

µç×ÓÓÊÏä£º
    1607388033@qq.com
×÷Õß:
    °µ³Á
ÃèÊö:
    ×´Ì¬¸ñ×Ó

-----------------------*/

namespace RPGGame
{
    public class UI_CraftSlot : UI_ItemSlot
    {

        protected override void Start()
        {
            base.Start();
        }

        public void SetupCraftSlot(ItemData_Equipment _data)
        {
            if (_data == null)
                return;

            item.data = _data;

            itemImage.sprite = _data.itemIcon;
            itemText.text = _data.itemName;

            if (itemText.text.Length > 12)
                itemText.fontSize = (int)(itemText.fontSize * .7f);
            else
                itemText.fontSize = 24;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            ConfigEvent.EventCraftWindow.EventTrigger(item.data as ItemData_Equipment);
        }
    }
}
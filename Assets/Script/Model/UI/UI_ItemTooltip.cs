using Core;
using TMPro;
using UnityEngine;


/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ��Ʒ��ʾ

-----------------------*/

namespace RPGGame
{
    public class UI_ItemTooltip : UI_ToolTip
    {
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI itemDescription;

        [SerializeField] private int defaultFontSize = 32;

        private void Awake()
        {
            ConfigEvent.EventItemTooltipShow.AddEventListener<ItemData_Equipment>(ShowToolTip);
            ConfigEvent.EventItemTooltipClose.AddEventListener(HideToolTip);

            gameObject.SetActive(false);
        }

        public void ShowToolTip(ItemData_Equipment item)
        {
            gameObject.SetActive(true);

            if (item == null)
                return;

            itemNameText.text = item.itemName;
            itemTypeText.text = item.equipmentType.ToString();
            itemDescription.text = item.GetDescription();

            AdjustFontSize(itemNameText);
            AdjustPosition();

        }

        public void HideToolTip()
        {
            itemNameText.fontSize = defaultFontSize;
            gameObject.SetActive(false);
        }

    }
}
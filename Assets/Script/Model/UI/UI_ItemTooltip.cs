using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPGGame
{
    public class UI_ItemTooltip : UI_ToolTip
    {
        [SerializeField] private Text itemNameText;
        [SerializeField] private Text itemTypeText;
        [SerializeField] private Text itemDescription;

        [SerializeField] private int defaultFontSize = 32;

        public void ShowToolTip(ItemData_Equipment item)
        {
            if (item == null)
                return;

            itemNameText.text = item.itemName;
            itemTypeText.text = item.equipmentType.ToString();
            itemDescription.text = item.GetDescription();

            AdjustFontSize(itemNameText);
            AdjustPosition();

            gameObject.SetActive(true);
        }

        public void HideToolTip()
        {
            itemNameText.fontSize = defaultFontSize;
            gameObject.SetActive(false);
        }

    }
}
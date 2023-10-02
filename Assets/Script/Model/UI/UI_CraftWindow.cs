using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPGGame
{
    public class UI_CraftWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Button craftButton;

        [SerializeField] private Image[] materialImage;

        public void SetupCraftWindow(ItemData_Equipment _data)
        {

            craftButton.onClick.RemoveAllListeners();

            for (int i = 0; i < materialImage.Length; i++)
            {
                materialImage[i].color = Color.clear;
                materialImage[i].GetComponentInChildren<Text>().color = Color.clear;
            }

            for (int i = 0; i < _data.craftingMaterials.Count; i++)
            {
                if (_data.craftingMaterials.Count > materialImage.Length)
                    Debug.LogWarning("You have more materials amount than you have material slots in craft window");


                materialImage[i].sprite = _data.craftingMaterials[i].data.itemIcon;
                materialImage[i].color = Color.white;

                Text materialSlotText = materialImage[i].GetComponentInChildren<Text>();

                materialSlotText.text = _data.craftingMaterials[i].stackSize.ToString();
                materialSlotText.color = Color.white;
            }


            itemIcon.sprite = _data.itemIcon;
            itemName.text = _data.itemName;
            itemDescription.text = _data.GetDescription();

            craftButton.onClick.AddListener(() => Inventory.Instance.CanCraft(_data, _data.craftingMaterials));
        }
    }
}
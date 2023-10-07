using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = Core.Debug;


/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ���ù��մ���

-----------------------*/

namespace RPGGame
{
    /// <summary>
    /// ���ù��մ���
    /// </summary>
    public class UI_CraftWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Button craftButton;

        [SerializeField] private Image[] materialImage;

        private void Awake()
        {
            ConfigEvent.EventCraftWindow.AddEventListener<ItemData_Equipment>(SetupCraftWindow);
        }

        /// <summary>
        /// ���ù��մ���
        /// </summary>
        /// <param name="_data"></param>
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
                    Debug.Warn("���и���Ĳ���������Ĳ��ϲ��ڹ��մ���");


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
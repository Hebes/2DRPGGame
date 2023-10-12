using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    状态格子

-----------------------*/

namespace RPGGame
{
    public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string statName;
        [SerializeField] private EStatType statType;
        [SerializeField] private TextMeshProUGUI statValueText;
        [SerializeField] private TextMeshProUGUI statNameText;

        [TextArea]
        [SerializeField] private string statDescription;

        private void Awake()
        {
            statValueText = GetComponent<TextMeshProUGUI>();
            statNameText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnValidate()
        {
            gameObject.name = "状态 - " + statName;


            if (statNameText != null)
                statNameText.text = statName;
        }
        void Start()
        {
            UpdateStatValueUI();
        }

        public void UpdateStatValueUI()
        {
            PlayerStats playerStats = ModelPlayerManager.Instance.player.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                statValueText.text = playerStats.GetStat(statType).GetValue().ToString();

                if (statType == EStatType.health)
                    statValueText.text = playerStats.GetMaxHealthValue().ToString();
                else if (statType == EStatType.damage)
                    statValueText.text = (playerStats.damage.GetValue() + playerStats.strength.GetValue()).ToString();
                else if (statType == EStatType.critPower)
                    statValueText.text = (playerStats.critPower.GetValue() + playerStats.strength.GetValue()).ToString();
                else if (statType == EStatType.critChance)
                    statValueText.text = (playerStats.critChance.GetValue() + playerStats.agility.GetValue()).ToString();
                else if (statType == EStatType.evasion)
                    statValueText.text = (playerStats.evasion.GetValue() + playerStats.agility.GetValue()).ToString();
                else if (statType == EStatType.magicRes)
                    statValueText.text = (playerStats.magicResistance.GetValue() + (playerStats.intelligence.GetValue() * 3)).ToString();
            }
        }



        //鼠标事件
        public void OnPointerEnter(PointerEventData eventData)
        {
            ConfigEvent.EventStatToolTipShow.EventTrigger(statDescription);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ConfigEvent.EventStatToolTipClose.EventTrigger();
        }
    }
}
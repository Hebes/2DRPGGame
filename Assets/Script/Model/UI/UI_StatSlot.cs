using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace RPGGame
{
    public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private UI ui;

        [SerializeField] private string statName;
        [SerializeField] private EStatType statType;
        [SerializeField] private Text statValueText;
        [SerializeField] private Text statNameText;

        [TextArea]
        [SerializeField] private string statDescription;

        private void OnValidate()
        {
            gameObject.name = "Stat - " + statName;


            if (statNameText != null)
                statNameText.text = statName;
        }
        void Start()
        {
            UpdateStatValueUI();

            ui = GetComponentInParent<UI>();
        }

        public void UpdateStatValueUI()
        {
            PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            ui.statToolTip.ShowStatToolTip(statDescription);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ui.statToolTip.HideStatToolTip();
        }
    }
}
using Core;
using UnityEngine;
using UnityEngine.UI;
namespace RPGGame
{
    public class UI_HealthBar : MonoBehaviour
    {
        private Entity entity;
        private RectTransform myTransform;
        private Slider slider;

        private void Awake()
        {
            ConfigEvent.EventUIPanelPlayerHealth.AddEventListener<CharacterStats>(UpdateHealthUI);
            ConfigEvent.EventPlayerFlipUI.AddEventListener(FlipUI);
        }
        private void Start()
        {
            myTransform = GetComponent<RectTransform>();
            entity = GetComponentInParent<Entity>();
            slider = GetComponentInChildren<Slider>();
            UpdateHealthUI(ModelPlayerManager.Instance.player.playerStats);
        }

        private void OnDisable()
        {
            ConfigEvent.EventPlayerFlipUI.RemoveEventListener(FlipUI);
            ConfigEvent.EventUIPanelPlayerHealth.RemoveEventListener<CharacterStats>(UpdateHealthUI);
        }



        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="myStats">���</param>
        private void UpdateHealthUI(CharacterStats myStats)
        {
            slider.maxValue = myStats.GetMaxHealthValue();
            slider.value = myStats.currentHealth;
        }

        /// <summary>
        /// ��ɫ������һ������
        /// </summary>
        private void FlipUI()
        {
            myTransform.Rotate(0, 180, 0);
        }
    }
}
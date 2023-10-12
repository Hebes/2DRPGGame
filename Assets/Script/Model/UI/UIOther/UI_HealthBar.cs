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
            ConfigEvent.EventUIPanelPlayerHealth.EventAdd<CharacterStats>(UpdateHealthUI);
            ConfigEvent.EventPlayerFlipUI.EventAdd(FlipUI);
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
            ConfigEvent.EventPlayerFlipUI.EventRemove(FlipUI);
            ConfigEvent.EventUIPanelPlayerHealth.EventRemove<CharacterStats>(UpdateHealthUI);
        }



        /// <summary>
        /// 更新玩家生命
        /// </summary>
        /// <param name="myStats">玩家</param>
        private void UpdateHealthUI(CharacterStats myStats)
        {
            slider.maxValue = myStats.GetMaxHealthValue();
            slider.value = myStats.currentHealth;
        }

        /// <summary>
        /// 角色朝向另一个方向
        /// </summary>
        private void FlipUI()
        {
            myTransform.Rotate(0, 180, 0);
        }
    }
}
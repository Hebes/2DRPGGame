using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    显示在前头的面板

-----------------------*/

namespace RPGGame
{
    public class UIPanel_InGame : UIBase
    {

        private Slider slider;

        private Image dashImage;
        private Image parryImage;
        private Image crystalImage;
        private Image swordImage;
        private Image blackholeImage;
        private Image flaskImage;



        private TextMeshProUGUI currentSouls;   //当前灵魂
        private float soulsAmount;
        private float increaseRate = 100;       //增长率


        //属性
        private ModelSkill skills => ModelSkill.Instance;


        //生命周期
        public override void UIAwake()
        {
            base.UIAwake();
            InitUIBase(EUIType.Fixed, EUIMode.HideOther, EUILucenyType.Pentrate, false);

            UIComponent UIComponent = panelGameObject.GetComponent<UIComponent>();
            GameObject T_Currency_UI = UIComponent.Get<GameObject>("T_Currency_UI");
            GameObject T_Flask_Cooldown = UIComponent.Get<GameObject>("T_Flask_Cooldown");
            GameObject T_Parry_Cooldown = UIComponent.Get<GameObject>("T_Parry_Cooldown");
            GameObject T_Blackhole_Cooldown = UIComponent.Get<GameObject>("T_Blackhole_Cooldown");
            GameObject T_Sword_Cooldown = UIComponent.Get<GameObject>("T_Sword_Cooldown");
            GameObject T_Crystal_Cooldown = UIComponent.Get<GameObject>("T_Crystal_Cooldown");
            GameObject T_Dash_Cooldown = UIComponent.Get<GameObject>("T_Dash_Cooldown");
            GameObject T_Health_UI = UIComponent.Get<GameObject>("T_Health_UI");

            slider = T_Health_UI.GetSlider();
            dashImage = T_Dash_Cooldown.GetImage();
            parryImage = T_Parry_Cooldown.GetImage();
            crystalImage = T_Crystal_Cooldown.GetImage();
            swordImage = T_Sword_Cooldown.GetImage();
            blackholeImage = T_Blackhole_Cooldown.GetImage();
            flaskImage = T_Flask_Cooldown.GetImage();
            currentSouls = T_Currency_UI.GetTextMeshPro();
            //事件监听
            ConfigEvent.EventUIPanelPlayerHealth.EventAdd<CharacterStats>(UpdateHealthUI);
        }
        public override void UIUpdate()
        {
            base.UIUpdate();
            UpdateSoulsUI();

            if (Input.GetKeyDown(KeyCode.LeftShift) && skills.GetSkill<Dash_Skill>().dashUnlocked)
                SetCooldownOf(dashImage);

            if (Input.GetKeyDown(KeyCode.Q) && skills.GetSkill<Parry_Skill>() .parryUnlocked)
                SetCooldownOf(parryImage);

            if (Input.GetKeyDown(KeyCode.F) && skills.GetSkill<Crystal_Skill>() .crystalUnlocked)
                SetCooldownOf(crystalImage);

            if (Input.GetKeyDown(KeyCode.Mouse1) && skills.GetSkill<Sword_Skill>().swordUnlocked)
                SetCooldownOf(swordImage);

            if (Input.GetKeyDown(KeyCode.R) && skills.GetSkill<Blackhole_Skill>().blackholeUnlocked)
                SetCooldownOf(blackholeImage);


            if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.Instance.GetEquipment(EquipmentType.Flask) != null)
                SetCooldownOf(flaskImage);

            //TODO 技能冷却需要修改
            CheckCooldownOf(dashImage, skills.GetSkill<Dash_Skill>().cooldown);
            CheckCooldownOf(parryImage, skills.GetSkill<Parry_Skill>().cooldown);
            CheckCooldownOf(crystalImage, skills.GetSkill<Crystal_Skill>().cooldown);
            CheckCooldownOf(swordImage, skills.GetSkill<Sword_Skill>().cooldown);
            CheckCooldownOf(blackholeImage, skills.GetSkill<Blackhole_Skill>().cooldown);
            CheckCooldownOf(flaskImage, Inventory.Instance.flaskCooldown);
        }



        //其他函数
        /// <summary>
        /// 更新血量
        /// </summary>
        private void UpdateHealthUI(CharacterStats characterStats)
        {
            slider.maxValue = characterStats.GetMaxHealthValue();
            slider.value = characterStats.currentHealth;
        }


        /// <summary>
        /// 更新灵魂数量
        /// </summary>
        private void UpdateSoulsUI()
        {
            if (soulsAmount < ModelData.Instance.currency)
                soulsAmount += Time.deltaTime * increaseRate;
            else
                soulsAmount = ModelData.Instance.currency;


            currentSouls.text = ((int)soulsAmount).ToString();
        }

        /// <summary>
        /// 设置冷却时间
        /// </summary>
        /// <param name="_image"></param>
        private void SetCooldownOf(Image _image)
        {
            if (_image.fillAmount <= 0)
                _image.fillAmount = 1;
        }

        /// <summary>
        /// 更新冷却时间
        /// </summary>
        /// <param name="_image"></param>
        /// <param name="_cooldown"></param>
        private void CheckCooldownOf(Image _image, float _cooldown)
        {
            if (_image.fillAmount > 0)
                _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }
}

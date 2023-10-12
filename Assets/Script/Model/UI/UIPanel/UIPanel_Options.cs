using Core;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    设置面板

-----------------------*/

namespace RPGGame
{
    public class UIPanel_Options:UIBase
    {
        public override void UIAwake()
        {
            base.UIAwake();
            InitUIBase(EUIType.PopUp, EUIMode.HideOther, EUILucenyType.ImPenetrable, false);

            UIComponent UIComponent = panelGameObject.GetComponent<UIComponent>();
            GameObject T_Options = UIComponent.Get<GameObject>("T_Options");
            GameObject T_Craft = UIComponent.Get<GameObject>("T_Craft");
            GameObject T_Skilltree = UIComponent.Get<GameObject>("T_Skill tree");
            GameObject T_Character = UIComponent.Get<GameObject>("T_Character");

            //按钮监听
            ButtonOnClickAddListener(T_Options.name, OptionsUIPanel);       //设置面板
            ButtonOnClickAddListener(T_Craft.name, CraftUIPanel);           //技巧面板
            ButtonOnClickAddListener(T_Skilltree.name, SkilltreeUIPanel);   //技能树面板
            ButtonOnClickAddListener(T_Character.name, CharacterUIPanel);   //角色面板
        }

        /// <summary>
        /// 技巧面板
        /// </summary>
        /// <param name="go"></param>
        public void CraftUIPanel(GameObject go)
        {
            ConfigPrefab.prefabUIPanel_InGame.CloseUIPanel();
            CloseUIForm();
            ModelUI.Instance.UISwitch<UIPanel_Craft>(ConfigPrefab.prefabUIPanel_Craft);
        }

        /// <summary>
        /// 技能树面板
        /// </summary>
        /// <param name="go"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SkilltreeUIPanel(GameObject go)
        {
            ConfigPrefab.prefabUIPanel_InGame.CloseUIPanel();
            CloseUIForm();
            ModelUI.Instance.UISwitch<UIPanel_SkillTree>(ConfigPrefab.prefabUIPanel_SkillTree);
        }

        /// <summary>
        /// 角色面板
        /// </summary>
        /// <param name="go"></param>
        public void CharacterUIPanel(GameObject go)
        {
            ConfigPrefab.prefabUIPanel_InGame.CloseUIPanel();
            CloseUIForm();
            ModelUI.Instance.UISwitch<UIPanel_Character>(ConfigPrefab.prefabUIPanel_Character);
        }

        /// <summary>
        /// 技巧面板
        /// </summary>
        /// <param name="go"></param>
        public void OptionsUIPanel(GameObject go)
        {
            ConfigPrefab.prefabUIPanel_InGame.CloseUIPanel();
            CloseUIForm();
            ModelUI.Instance.UISwitch<UIPanel_Options>(ConfigPrefab.prefabUIPanel_Options);
        }
    }
}

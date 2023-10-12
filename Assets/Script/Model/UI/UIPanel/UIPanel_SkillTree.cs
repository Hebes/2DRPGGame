using Core;
using System;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    技能树面板

-----------------------*/

namespace RPGGame
{
    public class UIPanel_SkillTree : UIBase
    {
        public override void UIAwake()
        {
            base.UIAwake();
            InitUIBase(EUIType.PopUp, EUIMode.HideOther, EUILucenyType.ImPenetrable, false);

            UIComponent UIComponent = panelGameObject.GetComponent<UIComponent>();
            GameObject T_Character = UIComponent.Get<GameObject>("T_Character");
            GameObject T_Skilltree = UIComponent.Get<GameObject>("T_Skilltree");
            GameObject T_Craft = UIComponent.Get<GameObject>("T_Craft");
            GameObject T_Options = UIComponent.Get<GameObject>("T_Options");
            GameObject T_SwordThrow = UIComponent.Get<GameObject>("T_SwordThrow");
            GameObject T_Timestop = UIComponent.Get<GameObject>("T_Timestop");
            GameObject T_Vulnerability = UIComponent.Get<GameObject>("T_Vulnerability");
            GameObject T_Bouncysword = UIComponent.Get<GameObject>("T_Bouncysword");
            GameObject T_Bulletsword = UIComponent.Get<GameObject>("T_Bulletsword");
            GameObject T_Chainsawsword = UIComponent.Get<GameObject>("T_Chainsawsword");
            GameObject T_Timemirage = UIComponent.Get<GameObject>("T_Timemirage");
            GameObject T_Aggresivemirage = UIComponent.Get<GameObject>("T_Aggresivemirage");
            GameObject T_Multiplemirage = UIComponent.Get<GameObject>("T_Multiplemirage");
            GameObject T_Crystalmirage = UIComponent.Get<GameObject>("T_Crystalmirage");
            GameObject T_Blackhole = UIComponent.Get<GameObject>("T_Blackhole");
            GameObject T_Crystal = UIComponent.Get<GameObject>("T_Crystal");
            GameObject T_Mirageblink = UIComponent.Get<GameObject>("T_Mirageblink");
            GameObject T_Explosion = UIComponent.Get<GameObject>("T_Explosion");
            GameObject T_Controlleddestruction = UIComponent.Get<GameObject>("T_Controlleddestruction");
            GameObject T_Multipledistruction = UIComponent.Get<GameObject>("T_Multipledistruction");
            GameObject T_Dodge = UIComponent.Get<GameObject>("T_Dodge");
            GameObject T_Dodgemirage = UIComponent.Get<GameObject>("T_Dodgemirage");
            GameObject T_Dash = UIComponent.Get<GameObject>("T_Dash");
            GameObject T_DashHereIam = UIComponent.Get<GameObject>("T_DashHereIam");
            GameObject T_DashActuallyhereIam = UIComponent.Get<GameObject>("T_DashActuallyhereIam");
            GameObject T_Parry = UIComponent.Get<GameObject>("T_Parry");
            GameObject T_Restorewithparry = UIComponent.Get<GameObject>("T_Restorewithparry");
            GameObject T_Parrywithamirage = UIComponent.Get<GameObject>("T_Parrywithamirage");
            GameObject T_SkillToolTip = UIComponent.Get<GameObject>("T_SkillToolTip");



            //按钮监听
            ButtonOnClickAddListener(T_Options.name, OptionsUIPanel);       //设置面板
            ButtonOnClickAddListener(T_Craft.name, CraftUIPanel);           //技巧面板
            ButtonOnClickAddListener(T_Skilltree.name, SkilltreeUIPanel);   //技能树面板
            ButtonOnClickAddListener(T_Character.name, CharacterUIPanel);   //角色面板

            //这里解锁技能

            //Blackhole_Skill
            ButtonOnClickAddListener(T_Blackhole.name, Blackhole);               //黑洞

            //Clone_Skill
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Timemirage.name, Timemirage);             //时间克隆
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Aggresivemirage.name, Aggresivemirage);   //攻击克隆
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Multiplemirage.name, Multiplemirage);   //攻击克隆
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Crystalmirage.name, Crystalmirage);   //水晶克隆

            ////Crystal_Skill
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Mirageblink.name, Mirageblink);
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Crystal.name, Crystal);
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Explosion.name, Explosion);
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Controlleddestruction.name, Controlleddestruction);
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Multipledistruction.name, Multipledistruction);

            //TODO 需要取消注释
            ////Dash_Skill
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Dash.name, SkillDash);                    //解锁冲刺
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Dash_HereIam.name, SkillCloneOnDash);     //在冲刺上解锁克隆
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Dash_HereIam.name, SkillCloneOnArrival);  //到达后解锁克隆

            ////Dodge_Skill
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Dodge.name, SkillDodge);                  //闪避
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Dodgemirage.name, SkillMirageDodge);      //闪避幻影

            ////Parry_Skill
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Parry.name, SkillParry);                  //格挡
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Parry.name, SkillParryRestore);           //格挡恢复
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Parry.name, SkillParryWithMirage);        //格挡幻影

            ////Sword_Skill
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Bouncysword.name, SkillBounceSword);                  //反弹剑
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Bulletsword.name, SkillPierceSword);                  //刺穿剑
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Chainsawsword.name, SkillSpinSword);                  //旋转的剑
            //ButtonOnClickAddListener(SkillTreeSlot_UI_SwordThrow.name, SkillSword);                         //剑
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Timestop.name, SkillTimeStop);                        //时间停止
            //ButtonOnClickAddListener(SkillTreeSlot_UI_Vulnerability.name, SkillVulnurable);                  //受伤

        }

        private void SkillVulnurable(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Sword_Skill>(ConfigSkill.SkillVulnurable);
        }

        private void SkillTimeStop(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Sword_Skill>(ConfigSkill.SkillTimeStop);
        }

        private void SkillSword(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Sword_Skill>(ConfigSkill.SkillSword);
        }

        private void SkillSpinSword(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Sword_Skill>(ConfigSkill.SkillSpinSword);
        }

        private void SkillPierceSword(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Sword_Skill>(ConfigSkill.SkillPierceSword);
        }

        private void SkillBounceSword(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Sword_Skill>(ConfigSkill.SkillBounceSword);
        }

        private void SkillParryWithMirage(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Parry_Skill>(ConfigSkill.SkillParryWithMirage);
        }

        private void SkillParryRestore(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Parry_Skill>(ConfigSkill.SkillParryRestore);
        }

        private void SkillParry(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Parry_Skill>(ConfigSkill.SkillParry);
        }

        private void SkillMirageDodge(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Dodge_Skill>(ConfigSkill.SkillMirageDodge);
        }

        private void SkillDodge(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Dodge_Skill>(ConfigSkill.SkillDodge);
        }

        private void SkillCloneOnArrival(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Dash_Skill>(ConfigSkill.SkillCloneOnArrival);
        }

        private void SkillCloneOnDash(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Dash_Skill>(ConfigSkill.SkillCloneOnDash);
        }

        private void SkillDash(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Dash_Skill>(ConfigSkill.SkillDash);
        }

        private void Crystalmirage(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Clone_Skill>(ConfigSkill.SkillCrystalInstead);
        }

        private void Multiplemirage(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Clone_Skill>(ConfigSkill.SkillMultiClone);
        }

        private void Aggresivemirage(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Clone_Skill>(ConfigSkill.SkillAggresiveClone);
        }

        private void Timemirage(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Clone_Skill>(ConfigSkill.SkillCloneAttack);
        }

        private void Blackhole(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Blackhole_Skill>(string.Empty);
        }

        private void Multipledistruction(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Crystal_Skill>(ConfigSkill.SkillMultiStack);
        }

        private void Controlleddestruction(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Crystal_Skill>(ConfigSkill.SkillMovingCrystal);
        }

        private void Explosion(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Crystal_Skill>(ConfigSkill.SkillExplosiveCrystal);
        }

        private void Crystal(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Crystal_Skill>(ConfigSkill.SkillCrystal);
        }

        private void Mirageblink(GameObject go)
        {
            ModelSkill.Instance.UnLockSkill<Crystal_Skill>(ConfigSkill.SkillCrystalMirage);
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

using Codice.CM.SEIDInfo;
using Core;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
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
            ButtonOnClickAddListener(T_Timemirage.name, Timemirage);             //时间克隆
            ButtonOnClickAddListener(T_Aggresivemirage.name, Aggresivemirage);   //攻击克隆
            ButtonOnClickAddListener(T_Multiplemirage.name, Multiplemirage);   //攻击克隆
            ButtonOnClickAddListener(T_Crystalmirage.name, Crystalmirage);   //水晶克隆

            //Crystal_Skill
            ButtonOnClickAddListener(T_Mirageblink.name, Mirageblink);
            ButtonOnClickAddListener(T_Crystal.name, Crystal);
            ButtonOnClickAddListener(T_Explosion.name, Explosion);
            ButtonOnClickAddListener(T_Controlleddestruction.name, Controlleddestruction);
            ButtonOnClickAddListener(T_Multipledistruction.name, Multipledistruction);

            //TODO 需要取消注释

            //Dash_Skill
            ButtonOnClickAddListener(T_Dash.name, SkillDash);                    //解锁冲刺
            ButtonOnClickAddListener(T_DashHereIam.name, SkillCloneOnDash);     //在冲刺上解锁克隆
            ButtonOnClickAddListener(T_DashHereIam.name, SkillCloneOnArrival);  //到达后解锁克隆

            //Dodge_Skill
            ButtonOnClickAddListener(T_Dodge.name, SkillDodge);                  //闪避
            ButtonOnClickAddListener(T_Dodgemirage.name, SkillMirageDodge);      //闪避幻影

            //Parry_Skill
            ButtonOnClickAddListener(T_Parry.name, SkillParry);                  //格挡
            ButtonOnClickAddListener(T_Parry.name, SkillParryRestore);           //格挡恢复
            ButtonOnClickAddListener(T_Parry.name, SkillParryWithMirage);        //格挡幻影

            //Sword_Skill
            ButtonOnClickAddListener(T_Bouncysword.name, SkillBounceSword);                  //反弹剑
            ButtonOnClickAddListener(T_Bulletsword.name, SkillPierceSword);                  //刺穿剑
            ButtonOnClickAddListener(T_Chainsawsword.name, SkillSpinSword);                  //旋转的剑
            ButtonOnClickAddListener(T_SwordThrow.name, SkillSword);                         //剑
            ButtonOnClickAddListener(T_Timestop.name, SkillTimeStop);                        //时间停止
            ButtonOnClickAddListener(T_Vulnerability.name, SkillVulnurable);                  //受伤

            //技能介绍界面

            
        }

        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="go"></param>
        private void SkillVulnurable(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Sword_Skill() as Skill, ConfigSkill.SkillVulnurable);

        /// <summary>
        /// 时间停止
        /// </summary>
        /// <param name="go"></param>
        private void SkillTimeStop(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Sword_Skill() as Skill, ConfigSkill.SkillTimeStop);

        /// <summary>
        /// 剑
        /// </summary>
        /// <param name="go"></param>
        private void SkillSword(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Sword_Skill() as Skill, ConfigSkill.SkillSword);

        /// <summary>
        /// 旋转的剑
        /// </summary>
        /// <param name="go"></param>
        private void SkillSpinSword(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Sword_Skill() as Skill, ConfigSkill.SkillSpinSword);

        /// <summary>
        /// 刺穿剑
        /// </summary>
        /// <param name="go"></param>
        private void SkillPierceSword(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Sword_Skill() as Skill, ConfigSkill.SkillPierceSword);

        /// <summary>
        /// 反弹剑
        /// </summary>
        /// <param name="go"></param>
        private void SkillBounceSword(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Sword_Skill() as Skill, ConfigSkill.SkillBounceSword);

        /// <summary>
        /// 格挡幻影
        /// </summary>
        /// <param name="go"></param>
        private void SkillParryWithMirage(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Parry_Skill() as Skill, ConfigSkill.SkillParryWithMirage);

        /// <summary>
        /// 格挡恢复
        /// </summary>
        /// <param name="go"></param>
        private void SkillParryRestore(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Parry_Skill() as Skill, ConfigSkill.SkillParryRestore);

        /// <summary>
        /// 格挡
        /// </summary>
        /// <param name="go"></param>
        private void SkillParry(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Parry_Skill() as Skill, ConfigSkill.SkillParry);

        private void SkillMirageDodge(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Dodge_Skill() as Skill, ConfigSkill.SkillMirageDodge);

        private void SkillDodge(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Dodge_Skill() as Skill, ConfigSkill.SkillDodge);

        private void SkillCloneOnArrival(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Dash_Skill() as Skill, ConfigSkill.SkillCloneOnArrival);

        private void SkillCloneOnDash(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Dash_Skill() as Skill, ConfigSkill.SkillCloneOnDash);

        private void SkillDash(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Dash_Skill() as Skill, ConfigSkill.SkillDash);

        private void Crystalmirage(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Clone_Skill() as Skill, ConfigSkill.SkillCrystalInstead);

        private void Multiplemirage(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Clone_Skill() as Skill, ConfigSkill.SkillMultiClone);

        private void Aggresivemirage(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Clone_Skill() as Skill, ConfigSkill.SkillAggresiveClone);

        private void Timemirage(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Clone_Skill() as Skill, ConfigSkill.SkillCloneAttack);

        private void Blackhole(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Blackhole_Skill() as Skill, ConfigSkill.SkillBlackholeSkill);

        private void Multipledistruction(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Crystal_Skill() as Skill, ConfigSkill.SkillMultiStack);

        private void Controlleddestruction(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Crystal_Skill() as Skill, ConfigSkill.SkillMovingCrystal);

        private void Explosion(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Crystal_Skill() as Skill, ConfigSkill.SkillExplosiveCrystal);

        private void Crystal(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Crystal_Skill() as Skill, ConfigSkill.SkillCrystal);

        private void Mirageblink(GameObject go) => ConfigEvent.SkillUnLock.EventTrigger(new Crystal_Skill() as Skill, ConfigSkill.SkillCrystalMirage);

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

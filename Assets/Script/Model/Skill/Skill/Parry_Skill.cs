using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    格挡

-----------------------*/

namespace RPGGame
{
    public class Parry_Skill : Skill
    {

        //格挡
        public bool parryUnlocked;

        //格挡恢复
        private float restoreHealthPerentage;
        public bool restoreUnlocked;

        //格挡幻影
        public bool parryWithMirageUnlocked;

        public override void Awake()
        {
            base.Awake();
            cooldown = 2;
            restoreHealthPerentage = 0.2f;
        }

        public override void UnLockSkill(string skillName)
        {
            base.UnLockSkill(skillName);
            switch (skillName)
            {
                case ConfigSkill.SkillParry:
                    parryUnlocked = true;
                    break;
                case ConfigSkill.SkillParryRestore:
                    restoreUnlocked = true;
                    break;
                case ConfigSkill.SkillParryWithMirage:
                    parryWithMirageUnlocked = true;
                    break;
            }
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        public override void UseSkill()
        {
            base.UseSkill();
            if (restoreUnlocked)
            {
                int restoreAmount = Mathf.RoundToInt(Player.Instance.stats.GetMaxHealthValue() * restoreHealthPerentage);
                Player.Instance.stats.IncreaseHealthBy(restoreAmount);
            }
        }


        public override void CheckUnlock()
        {
            base.CheckUnlock();
            //UnlockParry();
            //UnlockParryRestore();
            //UnlockParryWithMirage();
        }

        /// <summary>
        /// 制造幻影
        /// </summary>
        /// <param name="_respawnTransform"></param>
        public void MakeMirageOnParry(Transform _respawnTransform)
        {
            if (parryWithMirageUnlocked)
                ModelSkill.Instance.GetSkill<Clone_Skill>().CreateCloneWithDelay(_respawnTransform);
        }
    }
}
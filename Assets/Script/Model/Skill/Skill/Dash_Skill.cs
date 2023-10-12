using UnityEngine;
using UnityEngine.UI;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    冲刺技能

-----------------------*/

namespace RPGGame
{
    public class Dash_Skill : Skill
    {
        //冲刺
        public bool dashUnlocked;

        //冲刺的预制体
        public bool cloneOnDashUnlocked;

        //到达的预制体
        public bool cloneOnArrivalUnlocked;

        public override void Awake()
        {
            base.Awake();
            cooldown = 60;
        }

        public override void UnLockSkill(string skillName)
        {
            base.UnLockSkill(skillName);
            switch (skillName)
            {
                case ConfigSkill.SkillDash:
                    dashUnlocked = true;
                    break;
                case ConfigSkill.SkillCloneOnDash:
                    cloneOnDashUnlocked = true;
                    break;
                case ConfigSkill.SkillCloneOnArrival:
                    cloneOnArrivalUnlocked = true;
                    break;
            }
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        public override void UseSkill()
        {
            base.UseSkill();
        }

        /// <summary>
        /// 检查解锁
        /// </summary>
        public override void CheckUnlock()
        {
            base.CheckUnlock();
            //UnlockDash();
            //UnlockCloneOnDash();
            //UnlockCloneOnArrival();
        }

        /// <summary>
        /// 冲刺克隆
        /// </summary>
        public void CloneOnDash()
        {
            if (cloneOnDashUnlocked)
                ModelSkill.Instance.GetSkill<Clone_Skill>().CreateClone(Player.Instance.transform, Vector3.zero);
        }

        /// <summary>
        /// 到达后克隆
        /// </summary>
        public void CloneOnArrival()
        {
            if (cloneOnArrivalUnlocked)
                ModelSkill.Instance.GetSkill<Clone_Skill>().CreateClone(Player.Instance.transform, Vector3.zero);
        }
    }
}
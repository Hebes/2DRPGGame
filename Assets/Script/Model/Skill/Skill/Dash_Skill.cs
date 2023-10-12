using UnityEngine;
using UnityEngine.UI;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ��̼���

-----------------------*/

namespace RPGGame
{
    public class Dash_Skill : Skill
    {
        //���
        public bool dashUnlocked;

        //��̵�Ԥ����
        public bool cloneOnDashUnlocked;

        //�����Ԥ����
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
        /// ʹ�ü���
        /// </summary>
        public override void UseSkill()
        {
            base.UseSkill();
        }

        /// <summary>
        /// ������
        /// </summary>
        public override void CheckUnlock()
        {
            base.CheckUnlock();
            //UnlockDash();
            //UnlockCloneOnDash();
            //UnlockCloneOnArrival();
        }

        /// <summary>
        /// ��̿�¡
        /// </summary>
        public void CloneOnDash()
        {
            if (cloneOnDashUnlocked)
                ModelSkill.Instance.GetSkill<Clone_Skill>().CreateClone(Player.Instance.transform, Vector3.zero);
        }

        /// <summary>
        /// ������¡
        /// </summary>
        public void CloneOnArrival()
        {
            if (cloneOnArrivalUnlocked)
                ModelSkill.Instance.GetSkill<Clone_Skill>().CreateClone(Player.Instance.transform, Vector3.zero);
        }
    }
}
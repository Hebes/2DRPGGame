using UnityEngine;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ��

-----------------------*/

namespace RPGGame
{
    public class Parry_Skill : Skill
    {

        //��
        public bool parryUnlocked;

        //�񵲻ָ�
        private float restoreHealthPerentage;
        public bool restoreUnlocked;

        //�񵲻�Ӱ
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
        /// ʹ�ü���
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
        /// �����Ӱ
        /// </summary>
        /// <param name="_respawnTransform"></param>
        public void MakeMirageOnParry(Transform _respawnTransform)
        {
            if (parryWithMirageUnlocked)
                ModelSkill.Instance.GetSkill<Clone_Skill>().CreateCloneWithDelay(_respawnTransform);
        }
    }
}
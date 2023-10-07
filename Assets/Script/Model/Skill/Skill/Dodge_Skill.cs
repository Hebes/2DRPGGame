using UnityEngine;

/*--------Ω≈±æ√Ë ˆ-----------

µÁ◊”” œ‰£∫
    1607388033@qq.com
◊˜’ﬂ:
    ∞µ≥¡
√Ë ˆ:
    …¡±‹

-----------------------*/

namespace RPGGame
{
    public class Dodge_Skill : Skill
    {
        //…¡±‹
        private int evasionAmount;
        public bool dodgeUnlocked;

        //…¡±‹ª√”∞
        public bool dodgeMirageUnlocked;

        public override void Awake()
        {
            base.Awake();
            cooldown = 0;
            evasionAmount = 10;
        }
        public override void UnLockSkill(string skillName)
        {
            base.UnLockSkill(skillName);
            switch (skillName)
            {
                case ConfigSkill.SkillDodge:
                    if (!dodgeUnlocked)
                    {
                        Player.Instance.stats.evasion.AddModifier(evasionAmount);
                        Inventory.Instance.UpdateStatsUI();
                        dodgeUnlocked = true;
                    }
                    break;
                case ConfigSkill.SkillMirageDodge:
                    dodgeMirageUnlocked = true;
                    break;
            }
        }

        public override void CheckUnlock()
        {
            base.CheckUnlock();
            //UnlockDodge();
            //UnlockMirageDodge();
        }


        public void CreateMirageOnDodge()
        {
            if (dodgeMirageUnlocked)
                ModelSkillManager.Instance.GetSkill<Clone_Skill>().CreateClone(Player.Instance.transform, new Vector3(2 * Player.Instance.facingDir, 0));
        }
    }
}
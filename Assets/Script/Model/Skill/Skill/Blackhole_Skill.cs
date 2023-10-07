using Core;
using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    黑洞

-----------------------*/

namespace RPGGame
{
    public class Blackhole_Skill : Skill
    {
        public bool blackholeUnlocked;          //黑洞是否解锁
        private int amountOfAttacks;            //攻击数量
        private float cloneCooldown;            //克隆的冷却时间
        private float blackholeDuration;        //黑洞持续时间

        private GameObject blackHolePrefab;     //黑洞预制体
        private float maxSize;                  //最大尺寸
        private float growSpeed;                //增长速度
        private float shrinkSpeed;              //减少的速度


        private Blackhole_Skill_Controller currentBlackhole;    //当前的黑洞

        /// <summary>
        /// 初始化技能
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            cooldown = 60f;
            amountOfAttacks = 15;
            cloneCooldown = 0.35f;
            blackholeDuration = 3f;
            blackHolePrefab = LoadResExtension.Load<GameObject>(ConfigPrefab.prefabBlackhole);
            maxSize = 15f;
            growSpeed = 3f;
            shrinkSpeed = 6f;
        }

        public override void CheckUnlock()
        {
            base.CheckUnlock();
            //TODO 检查技能解锁 比如图标变亮
            //UnLockSkill(string.Empty);
        }

        public override void UnLockSkill(string skillName)
        {
            base.UnLockSkill(skillName);
            blackholeUnlocked = true;
        }

        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }

        public override void UseSkill()
        {
            base.UseSkill();

            GameObject newBlackHole = GameObject.Instantiate(blackHolePrefab, Player.Instance.transform.position, Quaternion.identity);
            currentBlackhole = newBlackHole.GetComponent<Blackhole_Skill_Controller>();
            currentBlackhole.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneCooldown, blackholeDuration);

            //设置音效
            ModelAudioManager.Instance.PlaySFX(18, Player.Instance.transform);
            ModelAudioManager.Instance.PlaySFX(19, Player.Instance.transform);
        }




        /// <summary>
        /// 技能完成
        /// </summary>
        /// <returns></returns>
        public bool SkillCompleted()
        {
            if (!currentBlackhole)
                return false;

            if (currentBlackhole.playerCanExitState)
            {
                currentBlackhole = null;
                return true;
            }


            return false;
        }

        /// <summary>
        /// 获取黑洞尺寸
        /// </summary>
        /// <returns></returns>
        public float GetBlackholeRadius()
        {
            return maxSize / 2;
        }
    }
}
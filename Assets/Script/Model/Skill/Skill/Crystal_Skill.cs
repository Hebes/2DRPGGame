using Core;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    水晶

-----------------------*/

namespace RPGGame
{
    public class Crystal_Skill : Skill
    {
        private float crystalDuration;
        private GameObject crystalPrefab;
        private GameObject currentCrystal;

        //水晶海市蜃楼
        private bool cloneInsteadOfCrystal;

        //简单水晶
        public bool crystalUnlocked;

        //炸药晶体
        private float explisoveCooldown;
        private bool canExplode;


        //移动水晶
        private bool canMoveToEnemy;
        private float moveSpeed;


        //多个晶体
        private bool canUseMultiStacks;
        private int amountOfStacks;
        private float multiStackCooldown;
        private float useTimeWondow;        //使用时间窗口
        private List<GameObject> crystalLeft;


        public override void Awake()
        {
            base.Awake();

            cooldown = 0f;
            crystalDuration = 1.5f;

            crystalPrefab = LoadResExtension.Load<GameObject>(ConfigPrefab.prefabCrystal);
            explisoveCooldown = 3.5f;

            moveSpeed = 4f;

            amountOfStacks = 3;
            multiStackCooldown = 4f;
            useTimeWondow = 2.5f;
            crystalLeft = new List<GameObject>();
            crystalLeft.Add(crystalPrefab);
            crystalLeft.Add(crystalPrefab);
            crystalLeft.Add(crystalPrefab);
        }



        public override void CheckUnlock()
        {

        }

        public override void UnLockSkill(string skillName)
        {
            base.UnLockSkill(skillName);

            switch (skillName)
            {
                case ConfigSkill.SkillCrystal:
                    crystalUnlocked = true;
                    break;
                case ConfigSkill.SkillCrystalMirage:
                    cloneInsteadOfCrystal = true;
                    break;
                case ConfigSkill.SkillExplosiveCrystal:
                    canExplode = true;
                    cooldown = explisoveCooldown;
                    break;
                case ConfigSkill.SkillMovingCrystal:
                    canMoveToEnemy = true;
                    break;
                case ConfigSkill.SkillMultiStack:
                    canUseMultiStacks = true;
                    break;
            }
        }

        public override void UseSkill()
        {
            base.UseSkill();

            if (CanUseMultiCrystal())
                return;

            if (currentCrystal == null)
            {
                CreateCrystal();
            }
            else
            {
                if (canMoveToEnemy)
                    return;

                Vector2 playerPos = Player.Instance.transform.position;
                Player.Instance.transform.position = currentCrystal.transform.position;
                currentCrystal.transform.position = playerPos;

                if (cloneInsteadOfCrystal)
                {
                    ModelSkillManager.Instance.GetSkill<Clone_Skill>().CreateClone(currentCrystal.transform, Vector3.zero);
                    GameObject.Destroy(currentCrystal);
                }
                else
                {
                    currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
                }
            }
        }

        /// <summary>
        /// 创建水晶
        /// </summary>
        public void CreateCrystal()
        {
            currentCrystal = GameObject.Instantiate(crystalPrefab, Player.Instance.transform.position, Quaternion.identity);
            Crystal_Skill_Controller currentCystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

            currentCystalScript.SetupCrystal(
                crystalDuration, 
                canExplode, 
                canMoveToEnemy, 
                moveSpeed, 
                FindClosestEnemy(currentCrystal.transform), 
                Player.Instance);
        }

        /// <summary>
        /// 当前水晶选择随机目标
        /// </summary>
        public void CurrentCrystalChooseRandomTarget()
        {
            currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();
        }

        /// <summary>
        /// 能否使用多个水晶
        /// </summary>
        /// <returns></returns>
        private bool CanUseMultiCrystal()
        {
            if (canUseMultiStacks)
            {
                if (crystalLeft.Count > 0)
                {
                    if (crystalLeft.Count == amountOfStacks)
                        ResetAbility().Forget();

                    cooldown = 0;
                    GameObject crystalToSpawn = crystalLeft[crystalLeft.Count - 1];
                    GameObject newCrystal = GameObject.Instantiate(crystalToSpawn, Player.Instance.transform.position, Quaternion.identity);

                    crystalLeft.Remove(crystalToSpawn);

                    newCrystal.GetComponent<Crystal_Skill_Controller>().
                        SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(newCrystal.transform), Player.Instance);

                    if (crystalLeft.Count <= 0)
                    {
                        cooldown = multiStackCooldown;
                        RefilCrystal();
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 再填充水晶
        /// </summary>
        private void RefilCrystal()
        {
            int amountToAdd = amountOfStacks - crystalLeft.Count;
            for (int i = 0; i < amountToAdd; i++)
                crystalLeft.Add(crystalPrefab);
        }

        /// <summary>
        /// 复位
        /// </summary>
        /// <returns></returns>
        private async UniTask ResetAbility()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(useTimeWondow), false);
            if (cooldownTimer > 0)
                return;
            cooldownTimer = multiStackCooldown;
            RefilCrystal();
        }
    }
}
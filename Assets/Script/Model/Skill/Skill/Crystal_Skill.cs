using Core;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ˮ��

-----------------------*/

namespace RPGGame
{
    public class Crystal_Skill : Skill
    {
        private float crystalDuration;
        private GameObject crystalPrefab;
        private GameObject currentCrystal;

        //ˮ��������¥
        private bool cloneInsteadOfCrystal;

        //��ˮ��
        public bool crystalUnlocked;

        //ըҩ����
        private float explisoveCooldown;
        private bool canExplode;


        //�ƶ�ˮ��
        private bool canMoveToEnemy;
        private float moveSpeed;


        //�������
        private bool canUseMultiStacks;
        private int amountOfStacks;
        private float multiStackCooldown;
        private float useTimeWondow;        //ʹ��ʱ�䴰��
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
        /// ����ˮ��
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
        /// ��ǰˮ��ѡ�����Ŀ��
        /// </summary>
        public void CurrentCrystalChooseRandomTarget()
        {
            currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();
        }

        /// <summary>
        /// �ܷ�ʹ�ö��ˮ��
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
        /// �����ˮ��
        /// </summary>
        private void RefilCrystal()
        {
            int amountToAdd = amountOfStacks - crystalLeft.Count;
            for (int i = 0; i < amountToAdd; i++)
                crystalLeft.Add(crystalPrefab);
        }

        /// <summary>
        /// ��λ
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
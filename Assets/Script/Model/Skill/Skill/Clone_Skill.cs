using Core;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    克隆技能

-----------------------*/

namespace RPGGame
{
    public class Clone_Skill : Skill
    {
        [Header("克隆信息")]
        [SerializeField] private float attackMultiplier;                    //攻击速度
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration;                       //克隆持续时间

        [Header("克隆攻击")]
        [SerializeField] private float cloneAttackMultiplier;               //克隆攻击倍增器
        [SerializeField] private bool canAttack;                            //克隆能否攻击

        [Header("典型的克隆")]
        [SerializeField] private float aggresiveCloneAttackMultiplier;      //攻击克隆的攻击速度
        [HideInInspector] public bool canApplyOnHitEffect;                  //是否应用命中效果

        [Header("多个克隆")]
        [SerializeField] private float multiCloneAttackMultiplier;
        [SerializeField] private bool canDuplicateClone;
        [SerializeField] private float chanceToDuplicate;

        [Header("水晶代替克隆")]
        public bool crystalInseadOfClone;

        public override void Awake()
        {
            base.Awake();
            cooldown = 0;
            attackMultiplier = 0;
            clonePrefab = LoadResExtension.Load<GameObject>(ConfigPrefab.prefabAttackCheckClone);
            cloneDuration = 1.5f;

            cloneAttackMultiplier = 0.3f;

            aggresiveCloneAttackMultiplier = 0.8f;

            multiCloneAttackMultiplier = 0.3f;
            chanceToDuplicate = 99f;
        }

        //解锁
        public override void CheckUnlock()
        {
            base.CheckUnlock();
        }

        public override void UnLockSkill(string skillName)
        {
            base.UnLockSkill(skillName);

            switch (skillName)
            {
                case ConfigSkill.SkillCloneAttack:
                    canAttack = true;
                    attackMultiplier = cloneAttackMultiplier;
                    break;
                case ConfigSkill.SkillAggresiveClone:
                    canApplyOnHitEffect = true;
                    attackMultiplier = aggresiveCloneAttackMultiplier;
                    break;
                case ConfigSkill.SkillMultiClone:
                    canDuplicateClone = true;
                    attackMultiplier = multiCloneAttackMultiplier;
                    break;
                case ConfigSkill.SkillCrystalInstead:
                    crystalInseadOfClone = true;
                    break;
            }
        }


        //其他
        /// <summary>
        /// 创建克隆
        /// </summary>
        /// <param name="_clonePosition"></param>
        /// <param name="_offset"></param>
        public void CreateClone(Transform _clonePosition, Vector3 _offset)
        {
            if (crystalInseadOfClone)
            {
                ModelSkill.Instance.GetSkill<Crystal_Skill>().CreateCrystal();
                return;
            }

            GameObject newClone = GameObject.Instantiate(clonePrefab);

            newClone.GetComponent<Clone_Skill_Controller>().
                SetupClone(_clonePosition,
                cloneDuration,
                canAttack,
                _offset,
                FindClosestEnemy(newClone.transform),
                canDuplicateClone,
                chanceToDuplicate,
                Player.Instance,
                attackMultiplier);
        }

        /// <summary>
        /// 创建延迟克隆
        /// </summary>
        /// <param name="_enemyTransform"></param>
        public void CreateCloneWithDelay(Transform _enemyTransform)
        {
            CloneDelayCorotine(_enemyTransform, new Vector3(2 * Player.Instance.facingDir, 0)).Forget();
        }

        /// <summary>
        /// 克隆延迟
        /// </summary>
        /// <param name="_trasnform"></param>
        /// <param name="_offset"></param>
        /// <returns></returns>
        private async UniTask CloneDelayCorotine(Transform _trasnform, Vector3 _offset)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f),  false);
            CreateClone(_trasnform, _offset);
        }
    }
}
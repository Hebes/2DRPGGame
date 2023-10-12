using Core;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;


/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ��¡����

-----------------------*/

namespace RPGGame
{
    public class Clone_Skill : Skill
    {
        [Header("��¡��Ϣ")]
        [SerializeField] private float attackMultiplier;                    //�����ٶ�
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration;                       //��¡����ʱ��

        [Header("��¡����")]
        [SerializeField] private float cloneAttackMultiplier;               //��¡����������
        [SerializeField] private bool canAttack;                            //��¡�ܷ񹥻�

        [Header("���͵Ŀ�¡")]
        [SerializeField] private float aggresiveCloneAttackMultiplier;      //������¡�Ĺ����ٶ�
        [HideInInspector] public bool canApplyOnHitEffect;                  //�Ƿ�Ӧ������Ч��

        [Header("�����¡")]
        [SerializeField] private float multiCloneAttackMultiplier;
        [SerializeField] private bool canDuplicateClone;
        [SerializeField] private float chanceToDuplicate;

        [Header("ˮ�������¡")]
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

        //����
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


        //����
        /// <summary>
        /// ������¡
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
        /// �����ӳٿ�¡
        /// </summary>
        /// <param name="_enemyTransform"></param>
        public void CreateCloneWithDelay(Transform _enemyTransform)
        {
            CloneDelayCorotine(_enemyTransform, new Vector3(2 * Player.Instance.facingDir, 0)).Forget();
        }

        /// <summary>
        /// ��¡�ӳ�
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
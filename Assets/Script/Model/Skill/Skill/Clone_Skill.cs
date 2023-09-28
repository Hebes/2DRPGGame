using System.Collections;
using UnityEngine;
using UnityEngine.UI;


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
        [SerializeField] private float attackMultiplier;
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration;

        [Header("��¡����")]
        [SerializeField] private UI_SkillTreeSlot cloneAttackUnlockButton;  //��¡����������ť
        [SerializeField] private float cloneAttackMultiplier;               //��¡����������
        [SerializeField] private bool canAttack;                            //��¡�ܷ񹥻�

        [Header("���͵Ŀ�¡")]
        [SerializeField] private UI_SkillTreeSlot aggresiveCloneUnlockButton;
        [SerializeField] private float aggresiveCloneAttackMultiplier;
        [HideInInspector] public bool canApplyOnHitEffect;

        [Header("�����¡")]
        [SerializeField] private UI_SkillTreeSlot multipleUnlockButton;
        [SerializeField] private float multiCloneAttackMultiplier;
        [SerializeField] private bool canDuplicateClone;
        [SerializeField] private float chanceToDuplicate;

        [Header("ˮ�������¡")]
        [SerializeField] private UI_SkillTreeSlot crystalInseadUnlockButton;
        public bool crystalInseadOfClone;



        //��������
        protected override void Start()
        {
            base.Start();

            cloneAttackUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneAttack);
            aggresiveCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockAggresiveClone);
            multipleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMultiClone);
            crystalInseadUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalInstead);
        }



        //��������
        protected override void CheckUnlock()
        {
            UnlockCloneAttack();
            UnlockAggresiveClone();
            UnlockMultiClone();
            UnlockCrystalInstead();
        }
        private void UnlockCloneAttack()
        {
            if (cloneAttackUnlockButton.unlocked)
            {
                canAttack = true;
                attackMultiplier = cloneAttackMultiplier;
            }
        }
        private void UnlockAggresiveClone()
        {
            if (aggresiveCloneUnlockButton.unlocked)
            {
                canApplyOnHitEffect = true;
                attackMultiplier = aggresiveCloneAttackMultiplier;
            }
        }
        private void UnlockMultiClone()
        {
            if (multipleUnlockButton.unlocked)
            {
                canDuplicateClone = true;
                attackMultiplier = multiCloneAttackMultiplier;
            }
        }
        private void UnlockCrystalInstead()
        {
            if (crystalInseadUnlockButton.unlocked)
            {
                crystalInseadOfClone = true;
            }
        }



        //����
        public void CreateClone(Transform _clonePosition, Vector3 _offset)
        {
            if (crystalInseadOfClone)
            {
                SkillManager.Instance.crystal.CreateCrystal();
                return;
            }

            GameObject newClone = Instantiate(clonePrefab);

            newClone.GetComponent<Clone_Skill_Controller>().
                SetupClone(_clonePosition,
                cloneDuration,
                canAttack,
                _offset,
                FindClosestEnemy(newClone.transform),
                canDuplicateClone,
                chanceToDuplicate,
                player,
                attackMultiplier);
        }
        public void CreateCloneWithDelay(Transform _enemyTransform)
        {
            StartCoroutine(CloneDelayCorotine(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
        }
        private IEnumerator CloneDelayCorotine(Transform _trasnform, Vector3 _offset)
        {
            yield return new WaitForSeconds(.4f);
            CreateClone(_trasnform, _offset);
        }
    }
}
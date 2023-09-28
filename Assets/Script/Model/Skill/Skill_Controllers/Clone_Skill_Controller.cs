using UnityEngine;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ��¡���ܵĿ�����

-----------------------*/

namespace RPGGame
{
    public class Clone_Skill_Controller : MonoBehaviour
    {
        private Player player;
        private SpriteRenderer sr;
        private Animator anim;
        [SerializeField] private float colorLoosingSpeed;   //ɫɢ�ٶ�

        private float cloneTimer;
        private float attackMultiplier;
        [SerializeField] private Transform attackCheck;
        [SerializeField] private float attackCheckRadius = .8f; //�������뾶
        private Transform closestEnemy;
        private int facingDir = 1;                              //���Եķ���


        private bool canDuplicateClone;
        private float chanceToDuplicate;

        //��������
        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            cloneTimer -= Time.deltaTime;
            if (cloneTimer < 0)
            {
                sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
                if (sr.color.a <= 0)
                    Destroy(gameObject);
            }
        }



        //����
        /// <summary>
        /// ���ÿ�¡��Ϣ
        /// </summary>
        /// <param name="_newTransform"></param>
        /// <param name="_cloneDuration"></param>
        /// <param name="_canAttack"></param>
        /// <param name="_offset"></param>
        /// <param name="_closestEnemy"></param>
        /// <param name="_canDuplicate"></param>
        /// <param name="_chanceToDuplicate"></param>
        /// <param name="_player"></param>
        /// <param name="_attackMultiplier"></param>
        public void SetupClone(Transform _newTransform,
            float _cloneDuration,
            bool _canAttack,
            Vector3 _offset,
            Transform _closestEnemy,
            bool _canDuplicate,
            float _chanceToDuplicate,
            Player _player,
            float _attackMultiplier)
        {
            if (_canAttack)
                anim.SetInteger("AttackNumber", Random.Range(1, 3));

            attackMultiplier = _attackMultiplier;
            player = _player;
            transform.position = _newTransform.position + _offset;
            cloneTimer = _cloneDuration;

            canDuplicateClone = _canDuplicate;
            chanceToDuplicate = _chanceToDuplicate;
            closestEnemy = _closestEnemy;
            FaceClosestTarget();
        }

        private void AnimationTrigger()
        {
            cloneTimer = -.1f;
        }

        private void AttackTrigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() == null) continue;
                //player.stats.DoDamage(hit.GetComponent<CharacterStats>()); // make a new function for clone damage to regulate damage;

                hit.GetComponent<Entity>().SetupKnockbackDir(transform);

                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                EnemyStats enemyStats = hit.GetComponent<EnemyStats>();

                playerStats.CloneDoDamage(enemyStats, attackMultiplier);

                if (player.skill.clone.canApplyOnHitEffect)
                {
                    ItemData_Equipment weaponData = Inventory.Instance.GetEquipment(EquipmentType.Weapon);

                    if (weaponData != null)
                        weaponData.Effect(hit.transform);
                }

                if (canDuplicateClone)
                {
                    if (Random.Range(0, 100) < chanceToDuplicate)
                    {
                        SkillManager.Instance.clone.CreateClone(hit.transform, new Vector3(.5f * facingDir, 0));
                    }
                }
            }
        }

        /// <summary>
        /// ������Ŀ��
        /// </summary>
        private void FaceClosestTarget()
        {
            if (closestEnemy != null)
            {
                if (transform.position.x > closestEnemy.position.x)
                {
                    facingDir = -1;
                    transform.Rotate(0, 180, 0);
                }
            }
        }
    }
}
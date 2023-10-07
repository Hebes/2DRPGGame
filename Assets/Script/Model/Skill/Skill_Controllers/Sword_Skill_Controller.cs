using System.Collections.Generic;
using UnityEngine;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ���Ĺ���

-----------------------*/

namespace RPGGame
{
    public class Sword_Skill_Controller : MonoBehaviour
    {
        private Animator anim;
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        private Player player;

        private bool canRotate = true;
        private bool isReturning;           //��Ʒ�ܷ����


        private float freezeTimeDuration;
        private float returnSpeed = 12;

        [Header("Pierce info")]
        private float pierceAmount;

        [Header("��������Ϣ")]
        private float bounceSpeed;
        private bool isBouncing;
        private int bounceAmount;
        private List<Transform> enemyTarget;
        private int targetIndex;

        [Header("��ת��Ϣ")]
        private float maxTravelDistance;
        private float spinDuration;
        private float spinTimer;
        private bool wasStopped;
        private bool isSpinning;

        private float hitTimer;
        private float hitCooldown;

        private float spinDirection;

        //��������
        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();
        }
        private void Update()
        {
            if (canRotate)
                transform.right = rb.velocity;//���ĳ������


            if (isReturning)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, player.transform.position) < 1)
                    player.CatchTheSword();
            }

            BounceLogic();
            SpinLogic();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isReturning)
                return;

            Enemy enemyTemp = collision.GetComponent<Enemy>();
            if (enemyTemp != null)
                SwordSkillDamage(enemyTemp);//�����ܵ��˺�
            SetupTargetsForBounce(collision);//���õ���Ŀ��
            StuckInto(collision);//����ǽ����߹���
        }




        //����
        private void DestroyMe()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="_dir"></param>
        /// <param name="_gravityScale"></param>
        /// <param name="_player"></param>
        /// <param name="_freezeTimeDuration"></param>
        /// <param name="_returnSpeed"></param>
        public void SetupSword(Vector2 _dir, float _gravityScale, Player _player, float _freezeTimeDuration, float _returnSpeed)
        {
            player = _player;
            freezeTimeDuration = _freezeTimeDuration;
            returnSpeed = _returnSpeed;
            rb.velocity = _dir;
            rb.gravityScale = _gravityScale;
            if (pierceAmount <= 0)
                anim.SetBool("Rotation", true);
            spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);
            Invoke("DestroyMe", 7);
        }

        public void SetupBounce(bool _isBouncing, int _amountOfBounces, float _bounceSpeed)
        {
            isBouncing = _isBouncing;
            bounceAmount = _amountOfBounces;
            bounceSpeed = _bounceSpeed;


            enemyTarget = new List<Transform>();
        }

        /// <summary>
        /// ���ô���
        /// </summary>
        /// <param name="_pierceAmount"></param>
        public void SetupPierce(int _pierceAmount)
        {
            pierceAmount = _pierceAmount;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="_isSpinning"></param>
        /// <param name="_maxTravelDistance"></param>
        /// <param name="_spinDuration"></param>
        /// <param name="_hitCooldown"></param>
        public void SetupSpin(bool _isSpinning, float _maxTravelDistance, float _spinDuration, float _hitCooldown)
        {
            isSpinning = _isSpinning;
            maxTravelDistance = _maxTravelDistance;
            spinDuration = _spinDuration;
            hitCooldown = _hitCooldown;
        }

        /// <summary>
        /// ���صĽ�
        /// </summary>
        public void ReturnSword()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //rb.isKinematic = false;
            transform.parent = null;
            isReturning = true;
            // sword.skill.setcooldown;
        }

        /// <summary>
        /// �������߼�
        /// </summary>
        private void SpinLogic()
        {
            if (isSpinning)
            {
                if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
                {
                    StopWhenSpinning();
                }

                if (wasStopped)
                {
                    spinTimer -= Time.deltaTime;

                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDirection, transform.position.y), 1.5f * Time.deltaTime);

                    if (spinTimer < 0)
                    {
                        isReturning = true;
                        isSpinning = false;
                    }


                    hitTimer -= Time.deltaTime;

                    if (hitTimer < 0)
                    {
                        hitTimer = hitCooldown;

                        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                        foreach (var hit in colliders)
                        {
                            if (hit.GetComponent<Enemy>() != null)
                                SwordSkillDamage(hit.GetComponent<Enemy>());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��תʱֹͣ
        /// </summary>
        private void StopWhenSpinning()
        {
            wasStopped = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            spinTimer = spinDuration;
        }

        /// <summary>
        /// �������߼�
        /// </summary>
        private void BounceLogic()
        {
            if (isBouncing && enemyTarget.Count > 0)
            {


                transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
                {

                    SwordSkillDamage(enemyTarget[targetIndex].GetComponent<Enemy>());

                    targetIndex++;
                    bounceAmount--;

                    if (bounceAmount <= 0)
                    {
                        isBouncing = false;
                        isReturning = true;
                    }

                    if (targetIndex >= enemyTarget.Count)
                        targetIndex = 0;
                }
            }
        }

        /// <summary>
        /// �����ܵ��˺�
        /// </summary>
        /// <param name="enemy"></param>
        private void SwordSkillDamage(Enemy enemy)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

            player.stats.DoDamage(enemyStats);

            if (player.skill.GetSkill<Sword_Skill>().timeStopUnlocked)
                enemy.FreezeTimeFor(freezeTimeDuration);

            if (player.skill.GetSkill<Sword_Skill>().vulnerableUnlocked)
                enemyStats.MakeVulnerableFor(freezeTimeDuration);



            ItemData_Equipment equipedAmulet = Inventory.Instance.GetEquipment(EquipmentType.Amulet);

            if (equipedAmulet != null)
                equipedAmulet.Effect(enemy.transform);
        }

        /// <summary>
        /// ���õ���Ŀ��
        /// </summary>
        /// <param name="collision"></param>
        private void SetupTargetsForBounce(Collider2D collision)
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                if (isBouncing && enemyTarget.Count <= 0)
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                            enemyTarget.Add(hit.transform);
                    }
                }
            }
        }

        /// <summary>
        /// ����ǽ����߹���
        /// </summary>
        /// <param name="collision"></param>
        private void StuckInto(Collider2D collision)
        {
            if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
            {
                pierceAmount--;
                return;
            }

            if (isSpinning)
            {
                StopWhenSpinning();
                return;
            }


            canRotate = false;
            cd.enabled = false;

            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponentInChildren<ParticleSystem>().Play();

            if (isBouncing && enemyTarget.Count > 0)
                return;


            anim.SetBool("Rotation", false);
            transform.parent = collision.transform;
        }
    }
}
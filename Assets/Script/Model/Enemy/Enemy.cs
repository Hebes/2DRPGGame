using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	怪物基类

-----------------------*/

namespace RPGGame
{
    public class Enemy : Entity
    {
        [SerializeField] protected LayerMask whatIsPlayer;  //玩家所在的层级

        //眩晕信息
        [Header("眩晕信息")]
        public float stunDuration;          //眩晕时间
        public Vector2 stunDirection;       //眩晕的方向
        protected bool canBeStunned;        //是否可以眩晕
        [SerializeField] protected GameObject counterImage; //反向图片

        //移动信息
        [Header("移动信息")]
        public float moveSpeed = 1.5f;         //移动速度
        public float idleTime = 2;          //停止时间
        public float battleTime = 7;        //战斗时间
        private float defaultMoveSpeed;     //默认的移动速度

        //攻击信息
        [Header("攻击信息")]
        public float attackDistance;    //攻击距离
        public float attackCooldown;    //攻击间隔
        public float minAttackCooldown; //最小攻击间隔
        public float maxAttackCooldown; //最大攻击间隔
        [HideInInspector] public float lastTimeAttacked;    //最后的攻击时间

        [HideInInspector] public EnemyStateMachine stateMachine; //怪物的状态机
        [HideInInspector] public EntityFX fx;
        [HideInInspector] public string lastAnimBoolName;

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new EnemyStateMachine();
            fx = GetComponent<EntityFX>();
            defaultMoveSpeed = moveSpeed;
        }

        protected override void Update()
        {
            base.Update();


            stateMachine.currentState.Update();

        }

        public virtual void AssignLastAnimName(string _animBoolName) => lastAnimBoolName = _animBoolName;


        public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
        {
            moveSpeed = moveSpeed * (1 - _slowPercentage);
            anim.speed = anim.speed * (1 - _slowPercentage);

            Invoke("ReturnDefaultSpeed", _slowDuration);
        }

        protected override void ReturnDefaultSpeed()
        {
            base.ReturnDefaultSpeed();

            moveSpeed = defaultMoveSpeed;
        }

        public virtual void FreezeTime(bool _timeFrozen)
        {
            if (_timeFrozen)
            {
                moveSpeed = 0;
                anim.speed = 0;
            }
            else
            {
                moveSpeed = defaultMoveSpeed;
                anim.speed = 1;
            }
        }

        public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimerCoroutine(_duration));

        protected virtual IEnumerator FreezeTimerCoroutine(float _seconds)
        {
            FreezeTime(true);

            yield return new WaitForSeconds(_seconds);

            FreezeTime(false);
        }

        #region Counter Attack Window
        public virtual void OpenCounterAttackWindow()
        {
            canBeStunned = true;
            counterImage.SetActive(true);
        }

        public virtual void CloseCounterAttackWindow()
        {
            canBeStunned = false;
            counterImage.SetActive(false);
        }
        #endregion

        public virtual bool CanBeStunned()
        {
            if (canBeStunned)
            {
                CloseCounterAttackWindow();
                return true;
            }

            return false;
        }

        public virtual void AnimationFinishTrigger()
        {
            stateMachine.currentState.AnimationFinishTrigger();
        }

        //玩家是否检测到
        public virtual RaycastHit2D IsPlayerDetected()
        {
            return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
        }
    }
}

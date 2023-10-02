using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	骷髅怪总控脚本

-----------------------*/

namespace RPGGame
{
    public class Enemy_Skeleton : Enemy
    {
        //状态
        public SkeletonIdleState idleState;
        public SkeletonMoveState moveState ;
        public SkeletonBattleState battleState ;
        public SkeletonAttackState attackState ;

        public SkeletonStunnedState stunnedState ;
        public SkeletonDeadState deadState ;

        protected override void Awake()
        {
            base.Awake();

            idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
            moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
            battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
            attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
            stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
            deadState = new SkeletonDeadState(this, stateMachine, "Idle", this);
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.U))
            {
                stateMachine.ChangeState(stunnedState);
            }
        }

        public override bool CanBeStunned()
        {
            if (base.CanBeStunned())
            {
                stateMachine.ChangeState(stunnedState);
                return true;
            }

            return false;
        }

        public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(deadState);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家控制器

-----------------------*/

namespace RPGGame
{
    public class Player : Entity
    {
        //攻击配置
        [Header("攻击配置")]
        public Vector2[] attackMovement;            //攻击的细节
        public bool isBusy;                         //攻击是否在使用中
        public float counterAttackDuration = .2f;

        //移动信息
        [Header("移动信息")]
        public float moveSpeed = 12f;           //移动速度
        public float jumpForce = 7;             //跳跃的力
        public float swordReturnImpact;         //回剑冲击
        private float defaultMoveSpeed;         //默认移动速度
        private float defaultJumpForce;         //默认跳跃的力

        //冲刺信息
        [Header("冲刺信息")]
        public float dashSpeed = 25f;                 //冲刺速度
        public float dashDuration = .2f;              //冲刺持续时间
        private float defaultDashSpeed;         //默认冲刺速度
        public float dashDir;                   //冲刺方向

        public SkillManager skill;
        public GameObject sword;
        public PlayerFX fx;

        //玩家状态
        public PlayerStateMachine stateMachine;     //状态管理类

        public PlayerIdleState idleState;           //停止状态
        public PlayerMoveState moveState;           //移动状态
        public PlayerJumpState jumpState;           //跳跃状态
        public PlayerAirState airState;             //空中状态
        public PlayerWallSlideState wallSlide;      //墙壁滑动
        public PlayerWallJumpState wallJump;        //墙壁跳跃
        public PlayerDashState dashState;           //撞击状态

        public PlayerPrimaryAttackState primaryAttack;  //初级攻击
        public PlayerCounterAttackState counterAttack;  //计数攻击

        public PlayerAimSwordState aimSowrd;            //玩家瞄准剑状态
        public PlayerCatchSwordState catchSword;        //握剑的状态
        public PlayerBlackholeState blackHole;          //黑洞状态
        public PlayerDeadState deadState;               //死亡状态


        protected override void Awake()
        {
            base.Awake();
            stateMachine = new PlayerStateMachine();

            idleState = new PlayerIdleState(this, stateMachine, "Idle");
            moveState = new PlayerMoveState(this, stateMachine, "Move");
            jumpState = new PlayerJumpState(this, stateMachine, "Jump");
            airState = new PlayerAirState(this, stateMachine, "Jump");
            dashState = new PlayerDashState(this, stateMachine, "Dash");
            wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
            wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");

            primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
            counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

            aimSowrd = new PlayerAimSwordState(this, stateMachine, "AimSword");
            catchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
            blackHole = new PlayerBlackholeState(this, stateMachine, "Jump");

            deadState = new PlayerDeadState(this, stateMachine, "Die");

            fx = GetComponent<PlayerFX>();

            skill = SkillManager.Instance;

            stateMachine.Initialize(idleState);//状态机初始化

            defaultMoveSpeed = moveSpeed;
            defaultJumpForce = jumpForce;
            defaultDashSpeed = dashSpeed;
        }


        protected override void Update()
        {
            if (Time.timeScale == 0) return;

            stateMachine.currentState.Update();//状态机更新

            CheckForDashInput();

            if (Input.GetKeyDown(KeyCode.F) && skill.crystal.crystalUnlocked)
                skill.crystal.CanUseSkill();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                Inventory.Instance.UseFlask();
        }

        public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
        {
            moveSpeed = moveSpeed * (1 - _slowPercentage);
            jumpForce = jumpForce * (1 - _slowPercentage);
            dashSpeed = dashSpeed * (1 - _slowPercentage);
            anim.speed = anim.speed * (1 - _slowPercentage);

            Invoke("ReturnDefaultSpeed", _slowDuration);
        }

        protected override void ReturnDefaultSpeed()
        {
            base.ReturnDefaultSpeed();

            moveSpeed = defaultMoveSpeed;
            jumpForce = defaultJumpForce;
            dashSpeed = defaultDashSpeed;
        }

        public void AssignNewSword(GameObject _newSword)
        {
            sword = _newSword;
        }

        public void CatchTheSword()
        {
            stateMachine.ChangeState(catchSword);
            Destroy(sword);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="_seconds"></param>
        /// <returns></returns>
        public IEnumerator BusyFor(float _seconds)
        {
            isBusy = true;

            yield return new WaitForSeconds(_seconds);
            isBusy = false;
        }

        public void AnimationTrigger()
        {
            stateMachine.currentState.AnimationFinishTrigger();
        }

        /// <summary>
        /// 冲刺
        /// </summary>
        private void CheckForDashInput()
        {
            if (IsWallDetected()) return;
            if (skill.dash.dashUnlocked == false) return;
            if (SkillManager.Instance.dash.CanUseSkill()==false)
                Debug.Log("技能没有冷却,无法使用技能");
            if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.Instance.dash.CanUseSkill())
            {
                dashDir = Input.GetAxisRaw("Horizontal");//冲刺的方向
                if (dashDir == 0)
                    dashDir = facingDir;
                stateMachine.ChangeState(dashState);
                //只有输入才能触发冲刺
                //if (dashDir != 0)
                //    stateMachine.ChangeState(dashState);
            }
        }

        public override void Die()
        {
            base.Die();

            stateMachine.ChangeState(deadState);
        }

        protected override void SetupZeroKnockbackPower()
        {
            knockbackPower = new Vector2(0, 0);
        }
    }
}

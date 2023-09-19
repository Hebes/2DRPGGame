using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ��ҿ�����

-----------------------*/

namespace RPGGame
{
    public class Player : Entity
    {
        //��������
        [Header("��������")]
        public Vector2[] attackMovement;            //������ϸ��
        public bool isBusy;                         //�����Ƿ���ʹ����
        public float counterAttackDuration = .2f;

        //�ƶ���Ϣ
        [Header("�ƶ���Ϣ")]
        public float moveSpeed = 12f;           //�ƶ��ٶ�
        public float jumpForce = 7;             //��Ծ����
        public float swordReturnImpact;         //�ؽ����
        private float defaultMoveSpeed;         //Ĭ���ƶ��ٶ�
        private float defaultJumpForce;         //Ĭ����Ծ����

        //�����Ϣ
        [Header("�����Ϣ")]
        public float dashSpeed = 25f;                 //����ٶ�
        public float dashDuration = .2f;              //��̳���ʱ��
        private float defaultDashSpeed;         //Ĭ�ϳ���ٶ�
        public float dashDir;                   //��̷���

        public SkillManager skill;
        public GameObject sword;
        public PlayerFX fx;

        //���״̬
        public PlayerStateMachine stateMachine;     //״̬������

        public PlayerIdleState idleState;           //ֹͣ״̬
        public PlayerMoveState moveState;           //�ƶ�״̬
        public PlayerJumpState jumpState;           //��Ծ״̬
        public PlayerAirState airState;             //����״̬
        public PlayerWallSlideState wallSlide;      //ǽ�ڻ���
        public PlayerWallJumpState wallJump;        //ǽ����Ծ
        public PlayerDashState dashState;           //ײ��״̬

        public PlayerPrimaryAttackState primaryAttack;  //��������
        public PlayerCounterAttackState counterAttack;  //��������

        public PlayerAimSwordState aimSowrd;            //�����׼��״̬
        public PlayerCatchSwordState catchSword;        //�ս���״̬
        public PlayerBlackholeState blackHole;          //�ڶ�״̬
        public PlayerDeadState deadState;               //����״̬


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

            stateMachine.Initialize(idleState);//״̬����ʼ��

            defaultMoveSpeed = moveSpeed;
            defaultJumpForce = jumpForce;
            defaultDashSpeed = dashSpeed;
        }


        protected override void Update()
        {
            if (Time.timeScale == 0) return;

            stateMachine.currentState.Update();//״̬������

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
        /// ��ͣ
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
        /// ���
        /// </summary>
        private void CheckForDashInput()
        {
            if (IsWallDetected()) return;
            if (skill.dash.dashUnlocked == false) return;
            if (SkillManager.Instance.dash.CanUseSkill()==false)
                Debug.Log("����û����ȴ,�޷�ʹ�ü���");
            if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.Instance.dash.CanUseSkill())
            {
                dashDir = Input.GetAxisRaw("Horizontal");//��̵ķ���
                if (dashDir == 0)
                    dashDir = facingDir;
                stateMachine.ChangeState(dashState);
                //ֻ��������ܴ������
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

using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家主要攻击状态

-----------------------*/

namespace RPGGame
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerPrimaryAttackState : PlayerState
    {

        public int comboCounter;                //攻击计数器
        private float lastTimeAttacked;         //最后攻击时间
        private float comboWindow = 2;          //连接开始前应经过多少时间重置

        public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //AudioManager.Instance.PlaySFX(2); // 攻击音效
            xInput = 0;  // 修复攻击方向上的bug
            if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
                comboCounter = 0;
            player.anim.SetInteger("ComboCounter", comboCounter);
            //player.anim.speed = 0.9f;//重型武器可以播放的速度

            //选择攻击方向
            float attackDir = player.facingDir;
            if (xInput != 0)
                attackDir = xInput;


            player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);


            stateTimer = .1f;
        }

        public override void Exit()
        {
            base.Exit();

            player.StartCoroutine("BusyFor", .15f);

            comboCounter++;
            lastTimeAttacked = Time.time;
            //player.anim.speed = 1f;//重型武器可以播放的速度
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0)
                player.SetZeroVelocity();

            if (triggerCalled)
                stateMachine.ChangeState(player.idleState);
        }
    }
}

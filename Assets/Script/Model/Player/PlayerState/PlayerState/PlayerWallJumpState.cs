using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    /// <summary>
    /// 墙面跳跃
    /// </summary>
    public class PlayerWallJumpState : PlayerState
    {
        public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = 1f;//当前墙面跳跃状态定时器
            player.SetVelocity(5 * -player.facingDir, player.jumpForce);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0)
                stateMachine.ChangeState(player.airState);

            if (player.IsGroundDetected())
                stateMachine.ChangeState(player.idleState);
        }
    }

}

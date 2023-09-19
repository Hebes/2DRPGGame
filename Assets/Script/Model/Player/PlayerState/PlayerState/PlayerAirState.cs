using RPGGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;

namespace RPGGame
{
    /// <summary>
    /// 玩家空中状态
    /// </summary>
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (player.IsWallDetected())
                stateMachine.ChangeState(player.wallSlide);
            if (player.IsGroundDetected())
                stateMachine.ChangeState(player.idleState);
            if (xInput != 0)
                player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }
    }

}

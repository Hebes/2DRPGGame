using Core;
using RPGGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;

namespace RPGGame
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            //AudioManager.Instance.PlaySFX(8, null);
        }

        public override void Exit()
        {
            base.Exit();

            //AudioManager.Instance.StopSFX(8);
        }

        public override void Update()
        {
            base.Update();
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

            if (xInput == 0 || player.IsWallDetected())
                stateMachine.ChangeState(player.idleState);
        }
    }

}

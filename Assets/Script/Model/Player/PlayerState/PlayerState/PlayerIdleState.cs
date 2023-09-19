using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;

namespace RPGGame
{
    /// <summary>
    /// 战力状态
    /// </summary>
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.SetZeroVelocity();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (xInput == player.facingDir && player.IsWallDetected())
                return;

            if (xInput != 0 && !player.isBusy)
                stateMachine.ChangeState(player.moveState);
        }
    }
}

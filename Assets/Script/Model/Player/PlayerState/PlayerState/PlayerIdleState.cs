using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家等待状态

-----------------------*/

namespace RPGGame
{
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

using Core;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    墙面跳跃

-----------------------*/

namespace RPGGame
{
    public class PlayerWallJumpState : PlayerState
    {
        public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("墙面跳跃");

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
            {
                bool isGroundDetected = player.IsGroundDetected();
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

}

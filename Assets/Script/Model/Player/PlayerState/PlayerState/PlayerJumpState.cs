using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家跳跃状态

-----------------------*/

namespace RPGGame
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (rb.velocity.y < 0)
                stateMachine.ChangeState(player.airState);
        }
    }

}

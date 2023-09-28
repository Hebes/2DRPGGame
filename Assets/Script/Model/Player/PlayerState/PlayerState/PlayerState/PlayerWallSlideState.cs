using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    墙壁滑动

-----------------------*/

namespace RPGGame
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

            if (player.IsWallDetected() == false)
            {
                Debug.Log("没有进入墙壁滑动状态");
                stateMachine.ChangeState(player.airState);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("玩家再次跳跃");
                stateMachine.ChangeState(player.wallJump);
                return;
            }

            if (xInput != 0 && player.facingDir != xInput)//没有任何输入并且输入的不是面向的方向
                stateMachine.ChangeState(player.idleState);

            if (yInput < 0) //解决下滑过快(因为自身重力)
                rb.velocity = new Vector2(0, rb.velocity.y);
            else
                rb.velocity = new Vector2(0, rb.velocity.y * .7f);

            if (player.IsGroundDetected())
                stateMachine.ChangeState(player.idleState);
        }
    }

}

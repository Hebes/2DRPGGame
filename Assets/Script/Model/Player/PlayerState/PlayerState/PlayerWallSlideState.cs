using UnityEngine;

namespace RPGGame
{
    /// <summary>
    /// 墙壁滑动
    /// </summary>
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
                stateMachine.ChangeState(player.airState);

            if (Input.GetKeyDown(KeyCode.Space))
            {
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

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家冲刺

-----------------------*/

namespace RPGGame
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.skill.GetSkill<Dash_Skill>().CloneOnDash();
            stateTimer = player.dashDuration;

            player.stats.MakeInvincible(true);

        }

        public override void Exit()
        {
            base.Exit();

            player.skill.GetSkill<Dash_Skill>().CloneOnArrival();
            player.SetVelocity(0, rb.velocity.y);

            player.stats.MakeInvincible(false);
        }
        public override void Update()
        {
            base.Update();

            if (!player.IsGroundDetected() && player.IsWallDetected())
                stateMachine.ChangeState(player.wallSlide);
            player.SetVelocity(player.dashSpeed * player.dashDir, 0);

            if (stateTimer < 0)
                stateMachine.ChangeState(player.idleState);

            player.fx.CreateAfterImage();
        }
    }

}

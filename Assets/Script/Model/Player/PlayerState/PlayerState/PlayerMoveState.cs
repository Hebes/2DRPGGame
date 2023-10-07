using Core;
using Cysharp.Threading.Tasks;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家移动状态

-----------------------*/

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
            ConfigEvent.EventPlayAudioSource.EventTriggerUniTask(ConfigAudio.mp3sfx_footsteps, EAudioSourceType.SFX, false).Forget();
        }

        public override void Exit()
        {
            base.Exit();
            ConfigEvent.EventStopAudioSource.EventTrigger(EAudioSourceType.SFX);
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

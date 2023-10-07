using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家死亡状态

-----------------------*/

namespace RPGGame
{
    public class PlayerDeadState : PlayerState
    {
        public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }

        public override void Enter()
        {
            base.Enter();
            //玩家死亡触发
            ModelUIManager.Instance.SwitchOnEndScreen().Forget();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            player.SetZeroVelocity();
        }
    }

}

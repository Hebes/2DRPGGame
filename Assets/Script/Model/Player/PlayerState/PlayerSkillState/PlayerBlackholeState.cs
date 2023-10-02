using Codice.Client.BaseCommands;
using RPGGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家黑洞状态

-----------------------*/

namespace RPGGame
{
    public class PlayerBlackholeState : PlayerState
    {
        private float flyTime = .4f;
        private bool skillUsed;


        private float defaultGravity;
        public PlayerBlackholeState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }

        public override void Enter()
        {
            base.Enter();

            defaultGravity = player.rb.gravityScale;

            skillUsed = false;
            stateTimer = flyTime;
            rb.gravityScale = 0;
        }

        public override void Exit()
        {
            base.Exit();

            player.rb.gravityScale = defaultGravity;
            player.fx.MakeTransprent(false);
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer > 0)
                rb.velocity = new Vector2(0, 15);

            if (stateTimer < 0)
            {
                rb.velocity = new Vector2(0, -.1f);

                if (!skillUsed)
                {
                    if (player.skill.blackhole.CanUseSkill())
                        skillUsed = true;
                }
            }

            if (player.skill.blackhole.SkillCompleted())
                stateMachine.ChangeState(player.airState);
        }
    }

}

using Codice.Client.BaseCommands;
using RPGGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;

namespace RPGGame
{
    /// <summary>
    /// 玩家反击状态
    /// </summary>
    public class PlayerCounterAttackState : PlayerState
    {
        private bool canCreateClone;

        public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            canCreateClone = true;
            stateTimer = player.counterAttackDuration;
            player.anim.SetBool("SuccessfulCounterAttack", false);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            player.SetZeroVelocity();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() == null) continue;
                if (hit.GetComponent<Enemy>().CanBeStunned() == false) continue;
                stateTimer = 10; // 任何大于1的值
                player.anim.SetBool("SuccessfulCounterAttack", true);
                player.skill.parry.UseSkill(); // 用来恢复队友的生命值
                if (canCreateClone)
                {
                    canCreateClone = false;
                    player.skill.parry.MakeMirageOnParry(hit.transform);
                }
            }

            if (stateTimer < 0 || triggerCalled)
                stateMachine.ChangeState(player.idleState);
        }
    }

}

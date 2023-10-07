using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家反击状态

-----------------------*/

namespace RPGGame
{
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
                player.skill.GetSkill<Parry_Skill>().UseSkill(); // 用来恢复队友的生命值
                if (canCreateClone)
                {
                    canCreateClone = false;
                    player.skill.GetSkill<Parry_Skill>().MakeMirageOnParry(hit.transform);
                }
            }

            if (stateTimer < 0 || triggerCalled)
                stateMachine.ChangeState(player.idleState);
        }
    }

}

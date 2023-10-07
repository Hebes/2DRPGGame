using Core;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家地面状态

-----------------------*/

namespace RPGGame
{
    public class PlayerGroundedState : PlayerState
    {
        public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

            Blackhole_Skill blackhole_Skill =  player.skill.GetSkill<Blackhole_Skill>();
            if (Input.GetKeyDown(KeyCode.R) && blackhole_Skill.blackholeUnlocked)
            {
                if (blackhole_Skill.cooldownTimer > 0)
                {
                    ConfigEvent.EventEffectPopUpText.EventTrigger("Cooldown!", player.transform.position);
                    return;
                }
                stateMachine.ChangeState(player.blackHole);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && player.skill.GetSkill<Sword_Skill>().swordUnlocked)
                stateMachine.ChangeState(player.aimSowrd);

            if (Input.GetKeyDown(KeyCode.Q) && player.skill.GetSkill<Parry_Skill>().parryUnlocked)
                stateMachine.ChangeState(player.counterAttack);

            //攻击
            if (Input.GetKeyDown(KeyCode.J))
                stateMachine.ChangeState(player.primaryAttack);

            if (!player.IsGroundDetected())
                stateMachine.ChangeState(player.airState);

            //跳跃 地面检测是为了不让跳到敌人头上或者物品上
            if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
                stateMachine.ChangeState(player.jumpState);
        }

        private bool HasNoSword()
        {
            if (!player.sword)
                return true;

            player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
            return false;
        }
    }
}

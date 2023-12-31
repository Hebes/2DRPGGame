﻿using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    投掷剑的状态

-----------------------*/

namespace RPGGame
{
    public class PlayerAimSwordState : PlayerState
    {
        public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ModelSkill.Instance.GetSkill<Sword_Skill>().DotsActive(true);
        }

        public override void Exit()
        {
            base.Exit();

            player.StartCoroutine("BusyFor", .2f);
        }

        public override void Update()
        {
            base.Update();

            player.SetZeroVelocity();

            if (Input.GetKeyUp(KeyCode.Mouse1))
                stateMachine.ChangeState(player.idleState);

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
                player.Flip();
            else if (player.transform.position.x < mousePosition.x && player.facingDir == -1)
                player.Flip();
        }
    }

}

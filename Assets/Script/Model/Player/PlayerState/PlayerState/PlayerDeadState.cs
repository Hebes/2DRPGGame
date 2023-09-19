﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

            //GameObject.Find("Canvas").GetComponent<UI>().SwitchOnEndScreen();
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
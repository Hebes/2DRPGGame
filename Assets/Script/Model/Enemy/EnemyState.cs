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
    怪物状态

-----------------------*/

namespace RPGGame
{
    public class EnemyState
    {
        protected EnemyStateMachine stateMachine;
        protected Enemy enemyBase;
        protected Rigidbody2D rb;

        private string animBoolName;

        protected float stateTimer;
        protected bool triggerCalled;

        public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
        {
            this.enemyBase = _enemyBase;
            this.stateMachine = _stateMachine;
            this.animBoolName = _animBoolName;
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
        }


        public virtual void Enter()
        {
            triggerCalled = false;
            rb = enemyBase.rb;
            enemyBase.anim.SetBool(animBoolName, true);
        }

        public virtual void Exit()
        {
            enemyBase.anim.SetBool(animBoolName, false);
            enemyBase.AssignLastAnimName(animBoolName);
        }

        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;//怪物攻击碰撞触发
        }
    }
}

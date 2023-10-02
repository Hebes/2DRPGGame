using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    怪物死亡状态

-----------------------*/

namespace RPGGame
{
    public class SkeletonDeadState : EnemyState
    {
        private Enemy_Skeleton enemy;

        public SkeletonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.enemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();

            enemy.anim.SetBool(enemy.lastAnimBoolName, true);
            enemy.anim.speed = 0;
            enemy.cd.enabled = false;

            stateTimer = .15f;
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer > 0)
                rb.velocity = new Vector2(0, 10);
        }
    }
}

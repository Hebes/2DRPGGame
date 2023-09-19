
using Core;
using Debug = UnityEngine.Debug;

namespace RPGGame
{
    /// <summary>
    /// 停止移动状态
    /// </summary>
    public class SkeletonIdleState : SkeletonGroundedState
    {
        public SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = enemy.idleTime;
        }
        public override void Exit()
        {
            base.Exit();
            //AudioManager.Instance.PlaySFX(14, enemy.transform);
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0)
                stateMachine.ChangeState(enemy.moveState);
        }
    }
}

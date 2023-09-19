using UnityEngine;

namespace RPGGame
{
    /// <summary>
    /// 骷髅怪动画触发
    /// </summary>
    public class Enemy_SkeletonAnimationTriggers : MonoBehaviour
    {
        private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

        private void AnimationTrigger()
        {
            enemy.AnimationFinishTrigger();
        }

        /// <summary>
        /// 攻击触发,动画控制器中监听
        /// </summary>
        private void AttackTrigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Player>() != null)
                {
                    PlayerStats target = hit.GetComponent<PlayerStats>();
                    //enemy.stats.DoDamage(target);
                }
            }
        }

        private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
        private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
    }
}

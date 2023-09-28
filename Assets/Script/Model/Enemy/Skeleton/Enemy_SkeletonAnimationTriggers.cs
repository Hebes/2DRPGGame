using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    骷髅怪动画触发

-----------------------*/

namespace RPGGame
{
    public class Enemy_SkeletonAnimationTriggers : MonoBehaviour
    {
        private Enemy_Skeleton enemy
        {
            get
            {
                return GetComponentInParent<Enemy_Skeleton>();
            }
        }

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
                    enemy.stats.DoDamage(target);
                }
            }
        }

        private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
        private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
    }
}

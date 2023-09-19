using Core;
using RPGGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPGGame
{
    /// <summary>
    /// 玩家动画播放组件
    /// </summary>
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private Player player => GetComponentInParent<Player>();

        private void AnimationTrigger()
        {
            player.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            AudioManager.Instance.PlaySFX(2, null);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    EnemyStats _target = hit.GetComponent<EnemyStats>();

                    if (_target != null)
                        player.stats.DoDamage(_target);

                    ItemData_Equipment weaponData = Inventory.Instance.GetEquipment(EquipmentType.Weapon);

                    if (weaponData != null)
                        weaponData.Effect(_target.transform);
                }
            }
        }
        private void ThrowSword()
        {
            SkillManager.Instance.sword.CreateSword();
        }
    }

}

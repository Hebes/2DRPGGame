using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{
    [CreateAssetMenu(fileName = "Freeze enemeis effect", menuName = "Data/Item effect/Freeze enemies")]
    public class FreezeEnemies_Effect : ItemEffect
    {
        [SerializeField] private float duration;


        public override void ExecuteEffect(Transform _transform)
        {
            PlayerStats playerStats = ModelPlayerManager.Instance.player.GetComponent<PlayerStats>();

            if (playerStats.currentHealth > playerStats.GetMaxHealthValue() * .1f)
                return;

            if (!Inventory.Instance.CanUseArmor())
                return;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 2);

            foreach (var hit in colliders)
            {
                hit.GetComponent<Enemy>()?.FreezeTimeFor(duration);
            }
        }
    }
}
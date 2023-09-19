using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{
    [CreateAssetMenu(fileName = "Heal effect", menuName = "Data/Item effect/Heal effect")]
    public class Heal_Effect : ItemEffect
    {
        [Range(0f, 1f)]
        [SerializeField] private float healPercent;

        public override void ExecuteEffect(Transform _enemyPosition)
        {
            PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();

            int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healPercent);

            playerStats.IncreaseHealthBy(healAmount);
        }
    }
}
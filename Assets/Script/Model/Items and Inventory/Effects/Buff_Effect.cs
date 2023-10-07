using UnityEngine;
namespace RPGGame
{

    [CreateAssetMenu(fileName = "Buff effect", menuName = "Data/Item effect/Buff effect")]

    public class Buff_Effect : ItemEffect
    {
        private PlayerStats stats;
        [SerializeField] private EStatType buffType;
        [SerializeField] private int buffAmount;
        [SerializeField] private float buffDuration;

        public override void ExecuteEffect(Transform _enemyPosition)
        {
            stats = ModelPlayerManager.Instance.player.GetComponent<PlayerStats>();
            stats.IncreaseStatBy(buffAmount, buffDuration, stats.GetStat(buffType));
        }
    }
}
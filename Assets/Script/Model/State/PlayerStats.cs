using UnityEngine;

namespace RPGGame
{
    /// <summary>
    /// 玩家状态
    /// </summary>
    public class PlayerStats : CharacterStats
    {
        private Player player;

        protected override void Start()
        {
            base.Start();

            player = GetComponent<Player>();
        }

        public override void TakeDamage(int _damage)
        {
            base.TakeDamage(_damage);
        }

        protected override void Die()
        {
            base.Die();
            player.Die();

            GameManager.Instance.lostCurrencyAmount = ModelDataManager.Instance.currency;
            ModelDataManager.Instance.currency = 0;

            GetComponent<PlayerItemDrop>()?.GenerateDrop();
        }

        /// <summary>
        /// 减少生命值
        /// </summary>
        /// <param name="_damage"></param>
        protected override void DecreaseHealthBy(int _damage)
        {
            base.DecreaseHealthBy(_damage);

            if (isDead)
                return;

            if (_damage > GetMaxHealthValue() * .3f)
            {
                player.SetupKnockbackPower(new Vector2(10, 6));
                player.fx.ScreenShake(player.fx.shakeHighDamage);


                int randomSound = Random.Range(34, 35);
                ModelAudioManager.Instance.PlaySFX(randomSound, null);

            }

            ItemData_Equipment currentArmor = Inventory.Instance.GetEquipment(EquipmentType.Armor);

            if (currentArmor != null)
                currentArmor.Effect(player.transform);
        }

        public override void OnEvasion()
        {
            player.skill.GetSkill<Dodge_Skill>().CreateMirageOnDodge();
        }

        public void CloneDoDamage(CharacterStats _targetStats, float _multiplier)
        {
            if (TargetCanAvoidAttack(_targetStats))
                return;

            int totalDamage = damage.GetValue() + strength.GetValue();

            if (_multiplier > 0)
                totalDamage = Mathf.RoundToInt(totalDamage * _multiplier);

            if (CanCrit())
            {
                totalDamage = CalculateCriticalDamage(totalDamage);
            }

            totalDamage = CheckTargetArmor(_targetStats, totalDamage);
            _targetStats.TakeDamage(totalDamage);


            DoMagicalDamage(_targetStats); // remove if you don't want to apply magic hit on primary attack
        }
    }
}

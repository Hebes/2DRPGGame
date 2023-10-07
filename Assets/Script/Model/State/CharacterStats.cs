using Core;
using System.Collections;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	玩家状态

-----------------------*/

namespace RPGGame
{
    /// <summary>
    /// 角色属性
    /// </summary>
    public class CharacterStats : MonoBehaviour
    {
        private EntityFX fx;

        [Header("主要属性")]
        public Stat strength; // 增加1点伤害和暴击。功率降低1%
        public Stat agility;  // 增加1点闪避和暴击。几率1%
        public Stat intelligence; // 增加1点魔法伤害和31点魔法抗性。增加1点魔法伤害和3点魔法抗性
        public Stat vitality; // 1点增加5点生命值

        [Header("攻击属性")]
        public Stat damage;
        public Stat critChance;
        public Stat critPower;              // 暴击率 默认值150%

        [Header("最大生命值")]
        public Stat maxHealth;
        public Stat armor;
        public Stat evasion;
        public Stat magicResistance;

        [Header("魔法属性")]
        public Stat fireDamage;
        public Stat iceDamage;
        public Stat lightingDamage;


        public bool isIgnited;   // does damage over time
        public bool isChilled;   // reduce armor by 20%
        public bool isShocked;   // reduce accuracy by 20%


        [SerializeField] private float ailmentsDuration = 4;
        private float ignitedTimer;
        private float chilledTimer;
        private float shockedTimer;


        private float igniteDamageCoodlown = .3f;
        private float igniteDamageTimer;
        private int igniteDamage;
        [SerializeField] private GameObject shockStrikePrefab;
        private int shockDamage;
        public int currentHealth;

        public System.Action onHealthChanged;
        public bool isDead { get; private set; }
        public bool isInvincible { get; private set; }
        private bool isVulnerable;

        protected virtual void Start()
        {
            critPower.SetDefaultValue(150);
            currentHealth = GetMaxHealthValue();

            fx = GetComponent<EntityFX>();
        }

        protected virtual void Update()
        {
            ignitedTimer -= Time.deltaTime;
            chilledTimer -= Time.deltaTime;
            shockedTimer -= Time.deltaTime;

            igniteDamageTimer -= Time.deltaTime;


            if (ignitedTimer < 0)
                isIgnited = false;

            if (chilledTimer < 0)
                isChilled = false;

            if (shockedTimer < 0)
                isShocked = false;

            if (isIgnited)
                ApplyIgniteDamage();
        }


        public void MakeVulnerableFor(float _duration) => StartCoroutine(VulnerableCorutine(_duration));

        private IEnumerator VulnerableCorutine(float _duartion)
        {
            isVulnerable = true;

            yield return new WaitForSeconds(_duartion);

            isVulnerable = false;
        }

        public virtual void IncreaseStatBy(int _modifier, float _duration, Stat _statToModify)
        {
            // start corototuine for stat increase
            StartCoroutine(StatModCoroutine(_modifier, _duration, _statToModify));
        }

        private IEnumerator StatModCoroutine(int _modifier, float _duration, Stat _statToModify)
        {
            _statToModify.AddModifier(_modifier);

            yield return new WaitForSeconds(_duration);

            _statToModify.RemoveModifier(_modifier);
        }


        public virtual void DoDamage(CharacterStats _targetStats)
        {
            bool criticalStrike = false;

            if (TargetCanAvoidAttack(_targetStats))
                return;

            _targetStats.GetComponent<Entity>().SetupKnockbackDir(transform);

            int totalDamage = damage.GetValue() + strength.GetValue();

            if (CanCrit())
            {
                totalDamage = CalculateCriticalDamage(totalDamage);
                criticalStrike = true;
            }

            fx.CreateHitFx(_targetStats.transform, criticalStrike);

            totalDamage = CheckTargetArmor(_targetStats, totalDamage);
            _targetStats.TakeDamage(totalDamage);


            DoMagicalDamage(_targetStats); // remove if you don't want to apply magic hit on primary attack

        }

        #region Magical damage and ailemnts

        public virtual void DoMagicalDamage(CharacterStats _targetStats)
        {
            int _fireDamage = fireDamage.GetValue();
            int _iceDamage = iceDamage.GetValue();
            int _lightingDamage = lightingDamage.GetValue();



            int totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

            totalMagicalDamage = CheckTargetResistance(_targetStats, totalMagicalDamage);
            _targetStats.TakeDamage(totalMagicalDamage);


            if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
                return;


            AttemptyToApplyAilements(_targetStats, _fireDamage, _iceDamage, _lightingDamage);

        }

        private void AttemptyToApplyAilements(CharacterStats _targetStats, int _fireDamage, int _iceDamage, int _lightingDamage)
        {
            bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
            bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
            bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

            while (!canApplyIgnite && !canApplyChill && !canApplyShock)
            {
                if (Random.value < .3f && _fireDamage > 0)
                {
                    canApplyIgnite = true;
                    _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);

                    return;
                }

                if (Random.value < .5f && _iceDamage > 0)
                {
                    canApplyChill = true;
                    _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);

                    return;
                }

                if (Random.value < .5f && _lightingDamage > 0)
                {
                    canApplyShock = true;

                    _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                    return;

                }
            }

            if (canApplyIgnite)
                _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));

            if (canApplyShock)
                _targetStats.SetupShockStrikeDamage(Mathf.RoundToInt(_lightingDamage * .1f));

            _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
        }


        public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
        {
            bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
            bool canApplyChill = !isIgnited && !isChilled && !isShocked;
            bool canApplyShock = !isIgnited && !isChilled;


            if (_ignite && canApplyIgnite)
            {
                isIgnited = _ignite;
                ignitedTimer = ailmentsDuration;

                fx.IgniteFxFor(ailmentsDuration);
            }

            if (_chill && canApplyChill)
            {
                chilledTimer = ailmentsDuration;
                isChilled = _chill;

                float slowPercentage = .2f;

                GetComponent<Entity>().SlowEntityBy(slowPercentage, ailmentsDuration);
                fx.ChillFxFor(ailmentsDuration);
            }

            if (_shock && canApplyShock)
            {
                if (!isShocked)
                {
                    ApplyShock(_shock);
                }
                else
                {
                    if (GetComponent<Player>() != null)
                        return;

                    HitNearestTargetWithShockStrike();
                }
            }

        }

        public void ApplyShock(bool _shock)
        {
            if (isShocked)
                return;

            shockedTimer = ailmentsDuration;
            isShocked = _shock;

            fx.ShockFxFor(ailmentsDuration);
        }

        private void HitNearestTargetWithShockStrike()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

            float closestDistance = Mathf.Infinity;
            Transform closestEnemy = null;

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = hit.transform;
                    }
                }

                if (closestEnemy == null)            // delete if you don't want shocked target to be hit by shock strike
                    closestEnemy = transform;
            }


            if (closestEnemy != null)
            {
                GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);
                newShockStrike.GetComponent<ShockStrike_Controller>().Setup(shockDamage, closestEnemy.GetComponent<CharacterStats>());
            }
        }
        private void ApplyIgniteDamage()
        {
            if (igniteDamageTimer < 0)
            {
                DecreaseHealthBy(igniteDamage);

                if (currentHealth < 0 && !isDead)
                    Die();

                igniteDamageTimer = igniteDamageCoodlown;
            }
        }

        public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;
        public void SetupShockStrikeDamage(int _damage) => shockDamage = _damage;

        #endregion

        public virtual void TakeDamage(int _damage)
        {

            if (isInvincible)
                return;

            DecreaseHealthBy(_damage);



            GetComponent<Entity>().DamageImpact();
            fx.StartCoroutine("FlashFX");

            if (currentHealth < 0 && !isDead)
                Die();


        }


        /// <summary>
        /// 增加生命值
        /// </summary>
        /// <param name="_amount"></param>
        public virtual void IncreaseHealthBy(int _amount)
        {
            currentHealth += _amount;

            if (currentHealth > GetMaxHealthValue())
                currentHealth = GetMaxHealthValue();

            ConfigEvent.EventUIPanelPlayerHealth.EventTrigger(this);
        }

        /// <summary>
        /// 减少生命值
        /// </summary>
        /// <param name="damage"></param>
        protected virtual void DecreaseHealthBy(int damage)
        {
            if (isVulnerable)
                damage = Mathf.RoundToInt(damage * 1.1f);

            currentHealth -= damage;

            if (damage > 0)
                ConfigEvent.EventEffectPopUpText.EventTrigger(damage.ToString(),transform.position);

            ConfigEvent.EventUIPanelPlayerHealth.EventTrigger(this);
        }

        protected virtual void Die()
        {
            isDead = true;
        }

        public void KillEntity()
        {
            if (!isDead)
                Die();
        }

        public void MakeInvincible(bool _invincible) => isInvincible = _invincible;


        #region Stat calculations
        protected int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
        {
            if (_targetStats.isChilled)
                totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * .8f);
            else
                totalDamage -= _targetStats.armor.GetValue();


            totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
            return totalDamage;
        }


        private int CheckTargetResistance(CharacterStats _targetStats, int totalMagicalDamage)
        {
            totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
            totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
            return totalMagicalDamage;
        }

        public virtual void OnEvasion()
        {

        }

        protected bool TargetCanAvoidAttack(CharacterStats _targetStats)
        {
            int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

            if (isShocked)
                totalEvasion += 20;

            if (Random.Range(0, 100) < totalEvasion)
            {
                _targetStats.OnEvasion();
                return true;
            }

            return false;
        }

        protected bool CanCrit()
        {
            int totalCriticalChance = critChance.GetValue() + agility.GetValue();

            if (Random.Range(0, 100) <= totalCriticalChance)
            {
                return true;
            }


            return false;
        }

        protected int CalculateCriticalDamage(int _damage)
        {
            float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;
            float critDamage = _damage * totalCritPower;

            return Mathf.RoundToInt(critDamage);
        }

        /// <summary>
        /// 获取角色的最大生命值
        /// </summary>
        /// <returns></returns>
        public int GetMaxHealthValue()
        {
            return maxHealth.GetValue() + vitality.GetValue() * 5;
        }

        #endregion

        public Stat GetStat(EStatType _statType)
        {
            if (_statType == EStatType.strength) return strength;
            else if (_statType == EStatType.agility) return agility;
            else if (_statType == EStatType.intelegence) return intelligence;
            else if (_statType == EStatType.vitality) return vitality;
            else if (_statType == EStatType.damage) return damage;
            else if (_statType == EStatType.critChance) return critChance;
            else if (_statType == EStatType.critPower) return critPower;
            else if (_statType == EStatType.health) return maxHealth;
            else if (_statType == EStatType.armor) return armor;
            else if (_statType == EStatType.evasion) return evasion;
            else if (_statType == EStatType.magicRes) return magicResistance;
            else if (_statType == EStatType.fireDamage) return fireDamage;
            else if (_statType == EStatType.iceDamage) return iceDamage;
            else if (_statType == EStatType.lightingDamage) return lightingDamage;

            return null;
        }
    }
}

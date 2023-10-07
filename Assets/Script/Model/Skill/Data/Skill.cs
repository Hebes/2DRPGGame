using Core;
using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    技能基类

-----------------------*/

namespace RPGGame
{
    public class Skill
    {
        public float cooldown;      //冷却
        public float cooldownTimer; //冷却计时器
        //public bool isUnLook;       //是否解锁

        /// <summary>
        /// 第一次
        /// </summary>
        public virtual void Awake()
        {

        }

        /// <summary>
        /// 循环更新
        /// </summary>
        public virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }

        /// <summary>
        /// 检查解锁
        /// </summary>
        public virtual void CheckUnlock() { }

        /// <summary>
        /// 解锁技能
        /// </summary>
        /// <param name="skillName"></param>
        public virtual void UnLockSkill(string skillName) { }

        /// <summary>
        /// 是否可以使用技能
        /// </summary>
        /// <returns></returns>
        public virtual bool CanUseSkill()
        {
            if (cooldownTimer < 0)
            {
                UseSkill();
                cooldownTimer = cooldown;
                return true;
            }
            ConfigEvent.EventEffectPopUpText.EventTrigger("冷却中...", Player.Instance.transform.position);
            return false;
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        public virtual void UseSkill()
        {
            //做一些特定技能的事情
        }

        /// <summary>
        /// 找到最近的敌人
        /// </summary>
        /// <param name="_checkTransform"></param>
        /// <returns></returns>
        protected virtual Transform FindClosestEnemy(Transform _checkTransform)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);

            float closestDistance = Mathf.Infinity;
            Transform closestEnemy = null;

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = hit.transform;
                    }
                }
            }
            return closestEnemy;
        }
    }
}
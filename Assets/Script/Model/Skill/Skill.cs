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
    /// <summary>
    /// 技能基类
    /// </summary>
    public class Skill : MonoBehaviour
    {
        public float cooldown;      //冷却
        public float cooldownTimer; //冷却计时器
        protected Player player;    //玩家



        //生命周期
        protected virtual void Start()
        {
            player = PlayerManager.Instance.player;
            CheckUnlock();
        }
        protected virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }



        //重写方法
        protected virtual void CheckUnlock()
        {

        }
        public virtual bool CanUseSkill()
        {
            if (cooldownTimer < 0)
            {
                UseSkill();
                cooldownTimer = cooldown;
                return true;
            }

            player.fx.CreatePopUpText("Cooldown");
            return false;
        }
        public virtual void UseSkill()
        {
            //做一些特定技能的事情
        }
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
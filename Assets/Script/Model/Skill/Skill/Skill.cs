using UnityEngine;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ���ܻ���

-----------------------*/

namespace RPGGame
{
    /// <summary>
    /// ���ܻ���
    /// </summary>
    public class Skill : MonoBehaviour
    {
        public float cooldown;      //��ȴ
        public float cooldownTimer; //��ȴ��ʱ��
        protected Player player;    //���



        //��������
        protected virtual void Start()
        {
            player = PlayerManager.Instance.player;
            CheckUnlock();
        }
        protected virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }



        //��д����
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
            //��һЩ�ض����ܵ�����
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
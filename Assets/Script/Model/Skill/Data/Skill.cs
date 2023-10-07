using Core;
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
    public class Skill
    {
        public float cooldown;      //��ȴ
        public float cooldownTimer; //��ȴ��ʱ��
        //public bool isUnLook;       //�Ƿ����

        /// <summary>
        /// ��һ��
        /// </summary>
        public virtual void Awake()
        {

        }

        /// <summary>
        /// ѭ������
        /// </summary>
        public virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }

        /// <summary>
        /// ������
        /// </summary>
        public virtual void CheckUnlock() { }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="skillName"></param>
        public virtual void UnLockSkill(string skillName) { }

        /// <summary>
        /// �Ƿ����ʹ�ü���
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
            ConfigEvent.EventEffectPopUpText.EventTrigger("��ȴ��...", Player.Instance.transform.position);
            return false;
        }

        /// <summary>
        /// ʹ�ü���
        /// </summary>
        public virtual void UseSkill()
        {
            //��һЩ�ض����ܵ�����
        }

        /// <summary>
        /// �ҵ�����ĵ���
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
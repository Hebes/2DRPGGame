using Core;
using UnityEngine;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    �ڶ�

-----------------------*/

namespace RPGGame
{
    public class Blackhole_Skill : Skill
    {
        public bool blackholeUnlocked;          //�ڶ��Ƿ����
        private int amountOfAttacks;            //��������
        private float cloneCooldown;            //��¡����ȴʱ��
        private float blackholeDuration;        //�ڶ�����ʱ��

        private GameObject blackHolePrefab;     //�ڶ�Ԥ����
        private float maxSize;                  //���ߴ�
        private float growSpeed;                //�����ٶ�
        private float shrinkSpeed;              //���ٵ��ٶ�


        private Blackhole_Skill_Controller currentBlackhole;    //��ǰ�ĺڶ�

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            cooldown = 60f;
            amountOfAttacks = 15;
            cloneCooldown = 0.35f;
            blackholeDuration = 3f;
            blackHolePrefab = LoadResExtension.Load<GameObject>(ConfigPrefab.prefabBlackhole);
            maxSize = 15f;
            growSpeed = 3f;
            shrinkSpeed = 6f;
        }

        public override void CheckUnlock()
        {
            base.CheckUnlock();
            //TODO ��鼼�ܽ��� ����ͼ�����
            //UnLockSkill(string.Empty);
        }

        public override void UnLockSkill(string skillName)
        {
            base.UnLockSkill(skillName);
            blackholeUnlocked = true;
        }

        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }

        public override void UseSkill()
        {
            base.UseSkill();

            GameObject newBlackHole = GameObject.Instantiate(blackHolePrefab, Player.Instance.transform.position, Quaternion.identity);
            currentBlackhole = newBlackHole.GetComponent<Blackhole_Skill_Controller>();
            currentBlackhole.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneCooldown, blackholeDuration);

            //������Ч
            ModelAudioManager.Instance.PlaySFX(18, Player.Instance.transform);
            ModelAudioManager.Instance.PlaySFX(19, Player.Instance.transform);
        }




        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public bool SkillCompleted()
        {
            if (!currentBlackhole)
                return false;

            if (currentBlackhole.playerCanExitState)
            {
                currentBlackhole = null;
                return true;
            }


            return false;
        }

        /// <summary>
        /// ��ȡ�ڶ��ߴ�
        /// </summary>
        /// <returns></returns>
        public float GetBlackholeRadius()
        {
            return maxSize / 2;
        }
    }
}
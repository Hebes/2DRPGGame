using Core;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = Core.Debug;


/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    �������ܵ�

-----------------------*/

namespace RPGGame
{
    public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISaveManager
    {
        private Image skillImage;

        public int skillID;


        [Header("�������ݽ��������ۿ�")]
        [SerializeField] private Color lockedSkillColor = new Color(0, 0, 0, 0);//δ��������ɫ
        public bool unlocked;
        [SerializeField] private int skillCost;     //��������
        [SerializeField] private string skillName;  //��������
        [TextArea]
        [SerializeField] private string skillDescription;
        [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
        [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;

        private void Awake()
        {
            //��ȡ���
            skillImage = GetComponent<Image>();
            //��������
            SkillInfo skillInfoData = ModelSkill.Instance.GetSkillInfoOne(skillID);
            skillCost = skillInfoData.skillCost;
            skillName = skillInfoData.name;
            skillDescription = skillInfoData.skillDescription;
            //���ó�ʼ��״̬
            skillImage.color = lockedSkillColor;
            if (unlocked)
                skillImage.color = Color.white;

            transform.AddEventTriggerListener(EventTriggerType.PointerClick, UnlockSkillSlot);
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="data"></param>
        private void UnlockSkillSlot(PointerEventData data)
        {
            //�ж�Ǯ�Ƿ��㹻
            if (ModelData.Instance.HaveEnoughMoney(skillCost) == false)
            {
                Debug.Log("Ǯ����,���ܽ�������");
                return;
            }

            //�ж�ǰ��ļ����Ƿ����

            for (int i = 0; i < shouldBeUnlocked.Length; i++)
            {
                if (shouldBeUnlocked[i].unlocked == false)
                {
                    Debug.Log("�޷���������");
                    return;
                }
            }

            //�ж��Ƿ�ֻ�ܽ���һ������һ��������������ܾͲ��ܽ��������ܶ�ѡһ��
            for (int i = 0; i < shouldBeLocked.Length; i++)
            {
                if (shouldBeLocked[i].unlocked == true)
                {
                    Debug.Log("�޷���������");
                    return;
                }
            }

            unlocked = true;
            skillImage.color = Color.white;
        }


        //����¼�
        /// <summary>
        /// �������¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            ConfigEvent.EventSkillToolTipShow.EventTrigger(skillDescription, skillName, skillCost);
        }

        /// <summary>
        /// ����˳��¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            ConfigEvent.EventSkillToolTipClose.EventTrigger();
        }

        public void LoadData(GameData _data)
        {
            if (_data.skillTree.TryGetValue(skillName, out bool value))
            {
                unlocked = value;
            }
        }

        public void SaveData(ref GameData _data)
        {
            if (_data.skillTree.TryGetValue(skillName, out bool value))
            {
                _data.skillTree.Remove(skillName);
                _data.skillTree.Add(skillName, unlocked);
            }
            else
                _data.skillTree.Add(skillName, unlocked);
        }
    }
}
using Core;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = Core.Debug;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    单个技能的

-----------------------*/

namespace RPGGame
{
    public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISaveManager
    {
        private Image skillImage;

        public int skillID;


        [Header("下面数据仅仅用来观看")]
        [SerializeField] private Color lockedSkillColor = new Color(0, 0, 0, 0);//未解锁的颜色
        public bool unlocked;
        [SerializeField] private int skillCost;     //技能消耗
        [SerializeField] private string skillName;  //技能名称
        [TextArea]
        [SerializeField] private string skillDescription;
        [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
        [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;

        private void Awake()
        {
            //获取组件
            skillImage = GetComponent<Image>();
            //设置数据
            SkillInfo skillInfoData = ModelSkill.Instance.GetSkillInfoOne(skillID);
            skillCost = skillInfoData.skillCost;
            skillName = skillInfoData.name;
            skillDescription = skillInfoData.skillDescription;
            //设置初始化状态
            skillImage.color = lockedSkillColor;
            if (unlocked)
                skillImage.color = Color.white;

            transform.AddEventTriggerListener(EventTriggerType.PointerClick, UnlockSkillSlot);
        }


        /// <summary>
        /// 解锁技能
        /// </summary>
        /// <param name="data"></param>
        private void UnlockSkillSlot(PointerEventData data)
        {
            //判断钱是否足够
            if (ModelData.Instance.HaveEnoughMoney(skillCost) == false)
            {
                Debug.Log("钱不够,不能解锁技能");
                return;
            }

            //判断前面的技能是否解锁

            for (int i = 0; i < shouldBeUnlocked.Length; i++)
            {
                if (shouldBeUnlocked[i].unlocked == false)
                {
                    Debug.Log("无法解锁技能");
                    return;
                }
            }

            //判断是否只能解锁一个（另一个解锁，这个技能就不能解锁，技能二选一）
            for (int i = 0; i < shouldBeLocked.Length; i++)
            {
                if (shouldBeLocked[i].unlocked == true)
                {
                    Debug.Log("无法解锁技能");
                    return;
                }
            }

            unlocked = true;
            skillImage.color = Color.white;
        }


        //鼠标事件
        /// <summary>
        /// 鼠标进入事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            ConfigEvent.EventSkillToolTipShow.EventTrigger(skillDescription, skillName, skillCost);
        }

        /// <summary>
        /// 鼠标退出事件
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
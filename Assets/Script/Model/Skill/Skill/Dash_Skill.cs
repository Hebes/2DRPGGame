using UnityEngine;
using UnityEngine.UI;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    冲刺技能

-----------------------*/

namespace RPGGame
{
    /// <summary>
    /// 冲刺技能
    /// </summary>
    public class Dash_Skill : Skill
    {
        [Header("冲刺")]
        [SerializeField] private UI_SkillTreeSlot dashUnlockButton;
        public bool dashUnlocked { get; private set; }

        [Header("冲刺的预制体")]
        [SerializeField] private UI_SkillTreeSlot cloneOnDashUnlockButton;
        public bool cloneOnDashUnlocked { get; private set; }

        [Header("到达的预制体")]
        [SerializeField] private UI_SkillTreeSlot cloneOnArrivalUnlockButton;
        public bool cloneOnArrivalUnlocked { get; private set; }


        protected override void Start()
        {
            base.Start();

            dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
            cloneOnDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
            cloneOnArrivalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        public override void UseSkill()
        {
            base.UseSkill();
        }

        /// <summary>
        /// 检查解锁
        /// </summary>
        protected override void CheckUnlock()
        {
            UnlockDash();
            UnlockCloneOnDash();
            UnlockCloneOnArrival();
        }

        /// <summary>
        /// 解锁冲刺
        /// </summary>
        private void UnlockDash()
        {
            if (dashUnlockButton.unlocked)
                dashUnlocked = true;
        }

        /// <summary>
        /// 在冲刺上解锁克隆
        /// </summary>
        private void UnlockCloneOnDash()
        {
            if (cloneOnDashUnlockButton.unlocked)
                cloneOnDashUnlocked = true;
        }

        /// <summary>
        /// 到达后解锁克隆
        /// </summary>
        private void UnlockCloneOnArrival()
        {
            if (cloneOnArrivalUnlockButton.unlocked)
                cloneOnArrivalUnlocked = true;
        }

        /// <summary>
        /// 冲刺克隆
        /// </summary>
        public void CloneOnDash()
        {
            if (cloneOnDashUnlocked)
                SkillManager.Instance.clone.CreateClone(player.transform, Vector3.zero);
        }

        /// <summary>
        /// 到达后克隆
        /// </summary>
        public void CloneOnArrival()
        {
            if (cloneOnArrivalUnlocked)
                SkillManager.Instance.clone.CreateClone(player.transform, Vector3.zero);
        }
    }
}
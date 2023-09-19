using UnityEngine;
using UnityEngine.UI;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ��̼���

-----------------------*/

namespace RPGGame
{
    /// <summary>
    /// ��̼���
    /// </summary>
    public class Dash_Skill : Skill
    {
        [Header("���")]
        [SerializeField] private UI_SkillTreeSlot dashUnlockButton;
        public bool dashUnlocked { get; private set; }

        [Header("��̵�Ԥ����")]
        [SerializeField] private UI_SkillTreeSlot cloneOnDashUnlockButton;
        public bool cloneOnDashUnlocked { get; private set; }

        [Header("�����Ԥ����")]
        [SerializeField] private UI_SkillTreeSlot cloneOnArrivalUnlockButton;
        public bool cloneOnArrivalUnlocked { get; private set; }


        protected override void Start()
        {
            base.Start();

            //dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
            //cloneOnDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
            //cloneOnArrivalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
        }

        /// <summary>
        /// ʹ�ü���
        /// </summary>
        public override void UseSkill()
        {
            base.UseSkill();
        }


        protected override void CheckUnlock()
        {
            UnlockDash();
            UnlockCloneOnDash();
            UnlockCloneOnArrival();
        }

        private void UnlockDash()
        {
            if (dashUnlockButton.unlocked)
                dashUnlocked = true;
        }

        private void UnlockCloneOnDash()
        {
            if (cloneOnDashUnlockButton.unlocked)
                cloneOnDashUnlocked = true;
        }

        private void UnlockCloneOnArrival()
        {
            if (cloneOnArrivalUnlockButton.unlocked)
                cloneOnArrivalUnlocked = true;
        }


        public void CloneOnDash()
        {
            if (cloneOnDashUnlocked)
                SkillManager.Instance.clone.CreateClone(player.transform, Vector3.zero);
        }

        public void CloneOnArrival()
        {
            if (cloneOnArrivalUnlocked)
                SkillManager.Instance.clone.CreateClone(player.transform, Vector3.zero);
        }
    }
}
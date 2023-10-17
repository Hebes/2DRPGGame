using Core;
using TMPro;
using UnityEngine;


/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ���ܽ������

-----------------------*/


namespace RPGGame
{
    public class UI_SkillToolTip : UI_ToolTip
    {
        [SerializeField] private TextMeshProUGUI skillText; //��������
        [SerializeField] private TextMeshProUGUI skillName; //��������
        [SerializeField] private TextMeshProUGUI skillCost; //���ܻ���
        [SerializeField] private float defaultNameFontSize; //Ĭ�����������С

        private void Awake()
        {
            ConfigEvent.EventSkillToolTipShow.EventAdd<string, string, int>(ShowToolTip);
            ConfigEvent.EventSkillToolTipClose.EventAdd(HideToolTip);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// ��ʾ
        /// </summary>
        /// <param name="_skillDescprtion"></param>
        /// <param name="_skillName"></param>
        /// <param name="_price"></param>
        public void ShowToolTip(string _skillDescprtion, string _skillName, int _price)
        {
            gameObject.SetActive(true);

            skillName.text = _skillName;            
            skillText.text = _skillDescprtion;
            skillCost.text = $"���� : {_price}";

            AdjustPosition();
            AdjustFontSize(skillName);
        } 

        /// <summary>
        /// �ر�
        /// </summary>
        public void HideToolTip()
        {
            skillName.fontSize = (int)defaultNameFontSize;
            gameObject.SetActive(false);
        }
    }
}

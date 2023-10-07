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
            ConfigEvent.EventSkillToolTipShow.AddEventListener<string, string, int>(ShowToolTip);
            ConfigEvent.EventSkillToolTipClose.AddEventListener(HideToolTip);
            gameObject.SetActive(false);
        }


        public void ShowToolTip(string _skillDescprtion, string _skillName, int _price)
        {
            gameObject.SetActive(true);

            skillName.text = _skillName;            
            skillText.text = _skillDescprtion;
            skillCost.text = $"���� : {_price}";

            AdjustPosition();
            AdjustFontSize(skillName);
        }

        public void HideToolTip()
        {
            skillName.fontSize = (int)defaultNameFontSize;
            gameObject.SetActive(false);
        }
    }
}

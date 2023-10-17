using Core;
using TMPro;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    技能介绍面板

-----------------------*/


namespace RPGGame
{
    public class UI_SkillToolTip : UI_ToolTip
    {
        [SerializeField] private TextMeshProUGUI skillText; //技能描述
        [SerializeField] private TextMeshProUGUI skillName; //技能名称
        [SerializeField] private TextMeshProUGUI skillCost; //技能花费
        [SerializeField] private float defaultNameFontSize; //默认名称字体大小

        private void Awake()
        {
            ConfigEvent.EventSkillToolTipShow.EventAdd<string, string, int>(ShowToolTip);
            ConfigEvent.EventSkillToolTipClose.EventAdd(HideToolTip);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="_skillDescprtion"></param>
        /// <param name="_skillName"></param>
        /// <param name="_price"></param>
        public void ShowToolTip(string _skillDescprtion, string _skillName, int _price)
        {
            gameObject.SetActive(true);

            skillName.text = _skillName;            
            skillText.text = _skillDescprtion;
            skillCost.text = $"花费 : {_price}";

            AdjustPosition();
            AdjustFontSize(skillName);
        } 

        /// <summary>
        /// 关闭
        /// </summary>
        public void HideToolTip()
        {
            skillName.fontSize = (int)defaultNameFontSize;
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private Text skillText;
    [SerializeField] private Text skillName;
    [SerializeField] private Text skillCost;
    [SerializeField] private float defaultNameFontSize;
    
    public void ShowToolTip(string _skillDescprtion,string _skillName,int _price)
    {
        skillName.text = _skillName;
        skillText.text = _skillDescprtion;
        skillCost.text = "Cost: " + _price;

        AdjustPosition();

        AdjustFontSize(skillName);

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        skillName.fontSize = (int)defaultNameFontSize;
        gameObject.SetActive(false);
    }
 
}

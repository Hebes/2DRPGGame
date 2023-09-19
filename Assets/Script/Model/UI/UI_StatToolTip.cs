using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;
using UnityEngine.UI;

public class UI_StatToolTip : UI_ToolTip
{
    [SerializeField] private Text description;
    
    public void ShowStatToolTip( string _text)
    {
        description.text = _text;
        AdjustPosition();

        gameObject.SetActive(true);
    }

    public void HideStatToolTip()
    {
        description.text = "";
        gameObject.SetActive(false);
    }
}
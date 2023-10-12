using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    Iͳ�ƹ�����ʾ

-----------------------*/

namespace RPGGame
{
    public class UI_StatToolTip : UI_ToolTip
    {
        [SerializeField] private TextMeshProUGUI description;

        private void Awake()
        {
            ConfigEvent.EventStatToolTipShow.EventAdd<string>(ShowStatToolTip);
            ConfigEvent.EventStatToolTipClose.EventAdd(HideStatToolTip);
            gameObject.SetActive(false);
        }
        public void ShowStatToolTip(string _text)
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
}

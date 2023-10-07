using Core;
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
        [SerializeField] private Text description;

        private void Awake()
        {
            ConfigEvent.EventStatToolTipShow.AddEventListener<string>(ShowStatToolTip);
            ConfigEvent.EventStatToolTipClose.AddEventListener(HideStatToolTip);
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
